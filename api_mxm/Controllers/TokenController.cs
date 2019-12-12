using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api_mxm.Models;
using api_mxm.Utiles.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api_mxm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        private readonly MXMContext _context;

        public TokenController(IConfiguration config, MXMContext context)
        {
            _config = config;
            _context = context;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public IEnumerable<Usuario> GetAll()
        {

            return _context.Usuarios.Include(usr => usr.RolesUsuarios)
                    .ThenInclude(ru => ru.Jerarquias).ThenInclude(je => je.Permisos)
                    .Include(usr=> usr.Documentos).ThenInclude(doc=> doc.TipoDocumento).ToList();
                
                    

        }

        [HttpGet("{id}", Name = "GetToken")]
        public IActionResult GetById(long id)
        {
            var item = _context.Tokens.FirstOrDefault(t => t.TokenId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [Route("token")]
        [HttpPost("{token}")]
        public IActionResult Create([FromBody]Token item)
        {
            IActionResult response;

            var item_token = _context.Tokens.FirstOrDefault(t => t.Cadena == item.Cadena);
            if (item_token == null)
            {
                response = Ok(new { token = "" });
            }
            else
            {
                response = Ok(new { token = item.Cadena });
            }

            return new ObjectResult(response);
        }

        [AllowAnonymous]
        [Route("Salir")]
        [HttpPost("{Salir}")]
        public IActionResult DeleteToken([FromBody] tokenbyid item)
        {
            var todo = _context.Tokens.FirstOrDefault(t => t.TokenId == item.id && t.Cadena == item.token);
            var result = new Models.Response();
            if (todo == null)
            {
                return NotFound();
            }

            try
            {
                _context.Tokens.Remove(todo);
                _context.SaveChanges();
                result = new Models.Response
                {
                    response = "Sesion cerrada correctamente"
                };
            }
            catch (Exception ex)
            {
                result = new Models.Response
                {
                    response = ex.ToString()
                };
            }
            return new ObjectResult(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            Debug.WriteLine("ingresado");
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user.id >0)
            {
                Debug.WriteLine("logueado");
                var tokenString = BuildToken(user);
                Token item = new Token
                {
                    Cadena = tokenString,
                    UsuarioId = user.id,
                    Fecha = DateTime.Now
                };

                _context.Tokens.Add(item);
                _context.SaveChanges();
                Usuario usuario = _context.Usuarios.Include(usr => usr.RolesUsuarios)
                    .ThenInclude(ru => ru.Jerarquias).ThenInclude(je => je.Permisos)
                    .Include(usr => usr.Registro).Where(usr=> usr.UsuarioId == user.id).SingleOrDefault();
                response = Ok(new { token = tokenString, usuario });

            }
            else
            {
                if (user.username == "none")
                {
                    response = Ok(new { token = "usuarios no existe" });
                }

                if (user.email == "none")
                {
                    response = Ok(new { token = "password incorrecto" });
                }
            }

            return response;
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Tokens.Count(t => t.TokenId == id);
            var result = new Models.Response();
            if (todo == 0)
            {
                return NotFound();
            }

            try
            {
                for (var i = 0; i < todo; i++)
                {
                    var query = _context.Tokens.FirstOrDefault(t => t.TokenId == id);
                    _context.Tokens.Remove(query);
                    _context.SaveChanges();
                }
                result = new Models.Response
                {
                    response = "Sesiones cerradas correctamente"
                };
            }
            catch (Exception ex)
            {
                result = new Models.Response
                {
                    response = ex.ToString()
                };
            }
            return new ObjectResult(result);
        }

        private string BuildToken(UserModel user)
        {

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.username),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginModel login)
        {
            UserModel user = null;
            //login.Password = HashHelper.MD5(login.Password);
            Usuario item = _context.Usuarios.Where(usr => usr.Username == login.Username).FirstOrDefault();
                //(from a in _context.Usuarios
                //        where a.Mail == login.Username
                //        select new
                //        {
                //            id = a.UsuarioId,
                //            username = a.Username,
                //            password = a.Password,
                //            email = a.Mail
                //        }).ToList();

            //var item = _context.Users.FirstOrDefault(t => t.email == login.Username);esc_actividad

            if (item == null)
            {
                user = new UserModel { id = 0, email = "none" };
            }
            else
            {
                if (login.Username == item.Mail && ConfirmPassword(item, login))
                {
                    user = new UserModel
                    {
                        id = item.UsuarioId,
                        username = item.Username,
                        //name = item[0].name,
                        //paterno = item[0].paterno,
                        //materno = item[0].materno,
                        password = item.Password,
                        email = item.Mail
                        //telefono = item[0].telefono,
                        //movil = item[0].movil,
                        //tipo_tecnico = item[0].tipo,
                        //noalmacen = item[0].noalmacen
                    };
                }
                else
                {
                    user = new UserModel { email = "none", };
                    //if (!ConfirmPassword(item, login))
                    //{
                    //    user = new UserModel { email = "none", };
                    //}
                }
            }

            return user;

        }

        
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private bool ConfirmPassword(Usuario usuario, LoginModel login)
        {
            string passHash = HashHelper.CreateHash(login.Password, usuario.Codigo);
            return usuario.Password.SequenceEqual(passHash);
        }

        public class ActividadModel
        {
            public long id { get; set; }
            public string actividad { get; set; }
        }


        private class UserModel
        {
            public long id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string email { get; set; }
        }

        public class tokenbyid
        {
            public long id { get; set; }
            public string token { get; set; }
        }
    }
    
}
