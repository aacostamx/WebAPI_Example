using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using api_mxm.Models;
using api_mxm.Utiles.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_mxm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CambiarPasswordController : ControllerBase
    {
        private readonly MXMContext _context;
        public CambiarPasswordController(MXMContext context)
        {
            _context = context;
        }

        // GET: api/CambiarPassword
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return _context.Usuarios.ToList();
        }

        // GET: api/CambiarPassword/email
        [HttpGet("{Mail}", Name = "Get")]
        public IActionResult Get(string email)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(t => t.Mail == email);
            Registro registro = _context.Registros.FirstOrDefault(r => r.UsuarioId == usuario.UsuarioId);
            var result = new Models.Response();

            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                var guid = Guid.NewGuid().ToString().Substring(0, 5);
                //var path = Path.GetFullPath("TemplateMail/Email.html");
                StreamReader reader = new StreamReader(Path.GetFullPath("TemplateMail/Email.html"));
                string body = string.Empty;
                body = reader.ReadToEnd();
                body = body.Replace("{username}", "Hola," + " " + registro.Nombre + " " + registro.Paterno + " " + registro.Materno);
                body = body.Replace("{pass}", "Tu nueva contraseña es:  $mxmex" + guid);
                try
                {
                    new SmtpClient
                    {
                        Host = "Smtp.Gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        Timeout = 10000,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("rodrigo.stps@gmail.com", "bostones01")
                    }.Send(new MailMessage
                    {
                        From = new MailAddress("no-reply@techo.org", "Miele"),
                        To = { email },
                        Subject = "Recuperar contraseña",
                        IsBodyHtml = true,
                        Body = body
                        //Body = "Nueva contraseña: " + "$miele" + guid, BodyEncoding = Encoding.UTF8 
                    });

                    usuario.Password = HashHelper.MD5("$mxmex" + guid);
                    _context.SaveChanges();
                    result = new Models.Response
                    {
                        response = "La contraseña se cambio correctamente"
                    };
                }
                catch (Exception ex)
                {
                    result = new Models.Response
                    {
                        response = ex.ToString()
                    };

                }
            }
            return new ObjectResult(result);
        }

        // POST: api/CambiarPassword
        [HttpPost]
        public void Post([FromBody]int id, string pass)
        {

        }

        // PUT: api/CambiarPassword/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] password item)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(t => t.UsuarioId == id);
            Registro registro = _context.Registros.FirstOrDefault(r => r.UsuarioId == usuario.UsuarioId);
            var result = new Models.Response();
            var guid = Guid.NewGuid().ToString().Substring(0, 4);
            var path = Path.GetFullPath("TemplateMail/Email.html");
            StreamReader reader = new StreamReader(Path.GetFullPath("TemplateMail/Email.html"));
            string body = string.Empty;
            body = reader.ReadToEnd();
            body = body.Replace("{username}", "Hola," + " " + registro.Nombre + " " + registro.Paterno + " " + registro.Materno);
            body = body.Replace("{pass}", "Tu contraseña ha sido modificada correctamente.");

            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    if (usuario.Password == item.OldPassword)
                    {
                        usuario.Password = item.NewPassword;
                        _context.SaveChanges();

                        new SmtpClient
                        {
                            Host = "Smtp.Gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            Timeout = 10000,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("rodrigo.stps@gmail.com", "bostones01")
                        }.Send(new MailMessage
                        {
                            From = new MailAddress("no-reply@techo.org", "Miele"),
                            To = { usuario.Mail },
                            Subject = "Cambio de contraseña",
                            IsBodyHtml = true,
                            Body = body
                            //Body = "Nueva contraseña: " + "$miele" + guid, BodyEncoding = Encoding.UTF8 
                        });

                        result = new Models.Response
                        {
                            response = "La contraseña se cambio correctamente"
                        };
                    }
                    else
                    {
                        result = new Models.Response
                        {
                            response = "Contraseña incorrecta"
                        };
                    }

                }
                catch (Exception ex)
                {
                    result = new Models.Response
                    {
                        response = ex.ToString()
                    };

                }
            }
            return new ObjectResult(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public class password
        {
            public string NewPassword { get; set; }
            public string OldPassword { get; set; }
        }
    }
}