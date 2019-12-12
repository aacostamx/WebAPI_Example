using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using api_mxm.Models;
using api_mxm.Utiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;
using api_mxm.Utiles.Helper;

namespace api_mxm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosController : ControllerBase
    {
        private IConfiguration config;
        private readonly MXMContext context;

        public RegistrosController(IConfiguration config, MXMContext context)
        {
            this.config = config;
            this.context = context;
        }


        [Route("RegistroEstudiantes")]
        [HttpPost]
        public IActionResult RegistroEstudiantes([FromBody]registroUsuario registro)
        {
            IActionResult response;
            //JsonResultado jsonResultado = new JsonResultado();

            if (registro == null)
            {
                return BadRequest();
            }
            else
            {
                var resp = context.Usuarios.FirstOrDefault(t => t.Username == registro.Username);
                Usuario usr = new Usuario();
                StreamReader reader = new StreamReader(Path.GetFullPath("TemplateMail/Email.html"));
                string body = string.Empty;
                body = reader.ReadToEnd();
                body = body.Replace("{username}", "Bienvenido," + " " + registro.Nombre + " " + registro.Paterno + " " + registro.Materno);
                
                body = body.Replace("{pass}", "Ahora te encuentras registrado como Estudiante");
                if (resp == null)
                {
                    try
                    {
                        
                        usr.Username = registro.Username;
                        usr.Codigo = HashHelper.CreateSalt(10);
                        string secPass = HashHelper.GenerateSHA256Hash(registro.Password, usr.Codigo);
                        usr.Password = secPass;
                        usr.Mail = registro.Mail;
                        usr.Creado = DateTime.Now;
                        usr.Actualizado = DateTime.Now;
                        usr.Avatar = registro.Avatar;
                        Registro newReg = new Registro() {
                            Nombre = registro.Nombre,
                            Paterno = registro.Paterno,
                            Materno = registro.Materno,
                            CURP = registro.CURP,
                            Movil = registro.Movil,
                            Fijo = registro.Fijo,
                            CodigoPostal = registro.CodigoPostal,
                            Domicilio = registro.Domicilio,
                            Exterior = registro.Exterior,
                            Interior = registro.Interior,
                            FechaNacimiento = registro.FechaNacimiento
                        };
                        usr.Registro = newReg;
                        List<Documento> documentos = ValidarDocumentos(registro);
                        foreach (Documento item in documentos)
                        {
                            if (item.Contenido != "Documento no valido")
                            {
                                usr.Documentos.Add(item);
                            }
                        }
                        //usr.Documentos = ;
                        RolUsuario rolUsuario = new RolUsuario() { RolId = 1 };
                        usr.RolesUsuarios.Add(rolUsuario);
                        context.Usuarios.Add(usr);
                        context.SaveChanges();

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
                            From = new MailAddress("no-reply@techo.org", "Mas por Mexico"),
                            To = { registro.Mail },
                            Subject = "Registro Estudiante",
                            IsBodyHtml = true,
                            Body = body
                        });

                        response = Ok(new { resultado = "Success", detalle = "Usuario creado correctamente", usr });
                    }
                    catch (Exception ex)
                    {
                        response = Ok(new { resultado = "Error", detalle = ex.Message, usr });
                    }
                    
                }
                else
                {
                    response = Ok(new { resultado = "Success", detalle = "Usuario ya registrado", usr });

                }
            }
            

            return response;
        }

        private string GuardarDocumentos(string imagen, string nombre)
        {
            string path = Environment.CurrentDirectory + "/Imagenes/Documentos/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                var imgdoc = Image.FromStream(new MemoryStream(Convert.FromBase64String(imagen)));
                string pathimg = Path.Combine(path, nombre + ".jpg");
                imgdoc.Save(pathimg);
                return pathimg;
            }
            catch (Exception ex)
            {
                return "Documento no valido";
            }
        }

        private List<Documento> ValidarDocumentos(registroUsuario registro)
        {
            //path += "/Imagenes/Documentos/";
            List<Documento> lstDocumentos = new List<Documento>();
            Documento acta = new Documento();
            //string.is
            if (!string.IsNullOrEmpty(registro.acnf))
            {
                lstDocumentos.Add(new Documento()
                { CursoId = registro.CursoId, TipoDocumentoId = 1, Contenido = GuardarDocumentos(registro.acnf, Guid.NewGuid().ToString() ) });
            }
            if (!string.IsNullOrEmpty(registro.acnv))
            {
                lstDocumentos.Add(new Documento()
                { CursoId = registro.CursoId, TipoDocumentoId = 1, Contenido = GuardarDocumentos(registro.acnv, Guid.NewGuid().ToString()) });
            }
            if (!string.IsNullOrEmpty(registro.Frente))
            {
                lstDocumentos.Add(new Documento()
                { CursoId = registro.CursoId, TipoDocumentoId = registro.TipoDocumentoId, Contenido = GuardarDocumentos(registro.Frente, Guid.NewGuid().ToString()) });
            }
            if (!string.IsNullOrEmpty(registro.Vuelta))
            {
                lstDocumentos.Add(new Documento()
                { CursoId = registro.CursoId, TipoDocumentoId = registro.TipoDocumentoId, Contenido = GuardarDocumentos(registro.Vuelta, Guid.NewGuid().ToString()) });
            }


            //switch (registro.CursoId)
            //{
            //    //Si es primariano hay documentos adicionales
            //    case 1:
            //        break;
            //        //Si es secundaria toma el tipo de dodumento 
            //    case 2:
            //        if (!string.IsNullOrEmpty(registro.Frente))
            //        {

            //            lstDocumentos.Add(new Documento()
            //            { CursoId = registro.CursoId, TipoDocumentoId = 2, Contenido = GuardarDocumentos(registro.Frente, Guid.NewGuid().ToString()) });
            //        }
            //        if (!string.IsNullOrEmpty(registro.Vuelta))
            //        {
            //            lstDocumentos.Add(new Documento()
            //            { CursoId = registro.CursoId, TipoDocumentoId = 2, Contenido = GuardarDocumentos(registro.Vuelta, Guid.NewGuid().ToString()) });
            //        }
            //        break;
            //    case 3:
            //        if (!string.IsNullOrEmpty(registro.Frente))
            //        {
            //            lstDocumentos.Add(new Documento()
            //            { CursoId = registro.CursoId, TipoDocumentoId = 3, Contenido = GuardarDocumentos(registro.Frente, Guid.NewGuid().ToString()) });
            //        }
            //        if (!string.IsNullOrEmpty(registro.Vuelta))
            //        {
            //            lstDocumentos.Add(new Documento()
            //            { CursoId = registro.CursoId, TipoDocumentoId = 3, Contenido = GuardarDocumentos(registro.Vuelta, Guid.NewGuid().ToString()) });
            //        }
            //        break;
            //    default:
            //        break;
            //}

            return lstDocumentos;
        }

        public class registroUsuario
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Mail { get; set; }
            public string Avatar { get; set; }
            public string Nombre { get; set; }
            public string Paterno { get; set; }
            public string Materno { get; set; }
            public string CURP { get; set; }
            public string Movil { get; set; }
            public string Fijo { get; set; }
            public string CodigoPostal { get; set; }
            public string Domicilio { get; set; }
            public string Exterior { get; set; }
            public string Interior { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public int CursoId { get; set; }
            public int TipoDocumentoId { get; set; }
            public string acnf { get; set; }
            public string acnv { get; set; }
            public string Frente { get; set; }
            public string Vuelta { get; set; }

        }
    }   
}