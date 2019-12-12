using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mxm.api.ActionFilter;
using mxm.api.Models;
using mxm.biz.Entities;
using mxm.biz.Repository;
using mxm.biz.Servicies;

namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IEmailService _emailService;
        private readonly IReasonRepository _reasonRepository;
        //TODO add IDistrictRepository

        public StudentController(
            IMapper mapper,
            ILoggerManager logger,
            IUserRepository userRepository,
            IStudentRepository studentRepository,
            IDocumentRepository documentRepository,
            IReasonRepository reasonRepository,
            IEmailService emailService)
        {
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _documentRepository = documentRepository;
            _reasonRepository = reasonRepository;
            _emailService = emailService;
        }


        [HttpPost("Login")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public ActionResult<ApiResponse<StudentDto>> Login(UserLoginDto item)
        {
            var response = new ApiResponse<StudentDto>();
            try
            {

                //UserDto usr = _mapper.Map<UserDto>(_userRepository.Find(c => c.Email == item.Email && c.Password == item.Password));
                UserDto usr = _mapper.Map<UserDto>(_userRepository.Find(u => u.Email == item.Email));
                if (usr != null)
                {
                    if (_userRepository.VerifyPassword(usr.Password, item.Password) && usr.Active)
                    {
                        //UserDto userDto = _mapper.Map<UserDto>(usr);
                        if (usr.RolId == 1)
                        {
                            StudentDto respuesta = _mapper.Map<StudentDto>(_studentRepository.Find(st => st.UserId == usr.Id));
                            if (respuesta.Active)
                            {
                                respuesta.token = "ABC";
                                response.Result = respuesta;
                            }
                            else
                            {
                                response.Success = false;
                                response.Message = "Tu acceso fue deshabilitado, comunicate con el administrador de la plataforma";
                            }
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "No cuentas con acceso a este recurso";
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Usuario y/o password incorrectos, favor de verificar";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Aun no estás registrado en la plataforma, te sugerimos comunicarte con el administrador";
                }
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return StatusCode(201, response);
        }

        [HttpPost("Logout/{studentId}")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public ActionResult<ApiResponse<bool>> Logout(int studentId)
        {
            var response = new ApiResponse<bool>();
            try
            {

                //UserDto usr = _mapper.Map<UserDto>(_userRepository.Find(c => c.Email == item.Email && c.Password == item.Password));
                StudentDto usr = _mapper.Map<StudentDto>(_studentRepository.Find(u => u.Id == studentId));
                if (usr != null)
                {
                    response.Success = true;
                    response.Message = "Logout correcto";
                    response.Result = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Usuario no existe";
                    response.Result = false;
                }
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("ActivateAccount")]
        public ActionResult<ApiResponse<StudentDto>> ActivateAccount(StudentActivateAccountDto studentDto)
        {
            var response = new ApiResponse<StudentDto>();
            Student stu = _studentRepository.Find(u => u.Code == studentDto.code);
            try
            {
                if (stu != null)
                {
                    if (!stu.Active)
                    {
                        User usr = _userRepository.Find(us => us.Id == stu.UserId);
                        var guid = Guid.NewGuid().ToString().Substring(0, 8);
                        usr.Password = _userRepository.HashPassword(guid);
                        stu.Activated = DateTime.Now;
                        stu.Active = true;
                        //stu.UserDto = _mapper.Map<UserDto>(usr);
                        //Student student = _mapper.Map<Student>(stu);
                        //student.User = usr;
                        stu.User = usr;
                        //_studentRepository.Update(student, stu.Id);
                        _studentRepository.Update(stu, usr.Student.Id);
                        response.Message = "Tu cuenta esta activa, revisa tu correo e ingresa la informacion proporcionada";
                        response.Result = _mapper.Map<StudentDto>(stu);
                        response.Result.token = "ABC";

                        StreamReader reader = new StreamReader(Path.GetFullPath("Templates/Email.html"));
                        string body = string.Empty;
                        body = reader.ReadToEnd();
                        //body = body.Replace("{username}", "Bienvenido," + " " + registro.Nombre + " " + registro.Paterno + " " + registro.Materno);
                        body = body.Replace("{username}", "Bienvenido, " + usr.Name + " " + usr.LastName + " " + usr.MotherName);
                        body = body.Replace("{pass}", "Tu nueva contraseña es " + guid);

                        Email email = new Email();
                        email.To = usr.Email;
                        email.Subject = "Activacion de cuenta";
                        email.Body = body;
                        email.IsBodyHtml = true;
                        //_emailService.SendEmail(email);

                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "El Codigo ya fue activado previamente";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "El Codigo ingresado no existe, favor de verificar";
                }
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}", Name ="GetStudent")]
        public ActionResult<ApiResponse<StudentProfileDto>> Profile(int id)
        {
            var response = new ApiResponse<StudentProfileDto>();

            try
            {
                response.Result = _mapper.Map<StudentProfileDto>(_studentRepository.Profile(id));
                //TimeSpan ts = DateTime.Today - response.Result.DateofBirth;
                //response.Result.age = DateTime.Today.AddTicks(-response.Result.DateofBirth.Ticks).Year - 1;
                //long edad = DateTime.Today.Ticks;
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }


        [HttpPut("{id}")]
        public ActionResult<ApiResponse<StudentProfileDto>> UpdateProfileDocuments(int id, StudentProfileDto profileDto)
        {
            var response = new ApiResponse<StudentProfileDto>();
            StudentUpdateDto updateDto = _mapper.Map<StudentUpdateDto>(profileDto);
            try
            {
                var stu = _studentRepository.Find(st => st.Id == id);
                updateDto.Activated = stu.Activated;
                updateDto.Active = stu.Active;
                if (stu != null)
                {
                    foreach (DocumentDto item in profileDto.Documents)
                    {
                        var doc = _documentRepository.Find(p => p.Id == item.Id);
                        if (doc == null)
                        {
                            //insert
                            Document document = _documentRepository.Add(_mapper.Map<Document>(item));
                            //response.Result = _mapper.Map<UserDto>(user);
                        }
                        else
                        {
                            //update
                            _mapper.Map(item, doc);
                            _documentRepository.Update(doc, item.Id);

                        }
                    }
                    var usr = _userRepository.Find(us => us.Id == updateDto.UserId);
                    _mapper.Map(profileDto.User, usr);
                    _userRepository.Update(usr, updateDto.UserId);
                    _mapper.Map(updateDto, stu);
                    _studentRepository.Update(stu, id);
                    response.Message = "Los cambios se guardaron correctamente";
                    response.Result = _mapper.Map<StudentProfileDto>(_studentRepository.Find(u => u.Id == id));
                }
                else
                {
                    response.Success = false;
                    response.Message = "El estudiante no existe, favor de verificar";
                }
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }
            return Ok(response);
        }


        //[HttpPost("Profile/{id}")]
        //public async Task<ActionResult<ApiResponse<StudentProfileDto>>> UpdateProfile(int id, StudentProfileDto profileDto, IFormFile certificado, IFormFile foto, IFormFile acta, IFormFile domicilio)
        //{
        //    var response = new ApiResponse<StudentProfileDto>();
        //    StudentUpdateDto updateDto = _mapper.Map<StudentUpdateDto>(profileDto);
        //    try
        //    {
        //        var stu = _studentRepository.Find(st => st.Id == id);
        //        updateDto.Activated = stu.Activated;
        //        updateDto.Active = stu.Active;

        //        if (stu != null)
        //        {
        //            //List<IFormFile> fotos = new List<IFormFile>();
        //            if (certificado != null) await GuardarImagen(certificado, 1, id);
        //            if (foto != null) await GuardarImagen(foto, 2, id);
        //            if (acta != null) await GuardarImagen(acta, 3, id);
        //            if (domicilio != null) await GuardarImagen(domicilio, 4, id);
                    
        //            var usr = _userRepository.Find(us => us.Id == updateDto.UserId);
        //            profileDto.User.Email = usr.Email;
        //            _mapper.Map(profileDto.User, usr);
        //            _userRepository.Update(usr, updateDto.UserId);
        //            _mapper.Map(updateDto, stu);
        //            _studentRepository.Update(stu, id);
        //            response.Message = "Los cambios se guardaron correctamente";
        //            response.Result = _mapper.Map<StudentProfileDto>(_studentRepository.Profile(id));
        //        }
        //        else
        //        {
        //            response.Success = false;
        //            response.Message = "El estudiante no existe, favor de verificar";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Result = null;
        //        response.Success = false;
        //        response.Message = "Internal server error";
        //        _logger.LogError($"Something went wrong: { ex.ToString() }");
        //        return StatusCode(500, response);
        //    }
        //    return Ok(response);
        //}

        

        [HttpPost("Send/{id}")]
        public ActionResult<ApiResponse<int>> SendMail(int id, StudentMailDto emailDto)
        {

            var response = new ApiResponse<int>();
            try
            {
                StudentProfileDto student = _mapper.Map<StudentProfileDto>(_studentRepository.Profile(id));
                ReasonDto reasonDto = _mapper.Map<ReasonDto>(_reasonRepository.Find(r => r.Id == emailDto.ReasonId));
                if (student != null && reasonDto != null)
                {
                    StreamReader reader = new StreamReader(Path.GetFullPath("Templates/Email.html"));
                    string body = string.Empty;
                    body = reader.ReadToEnd();
                    //body = body.Replace("{username}", emailDto.Body);
                    body = body.Replace("{username}", "Enviado por, " + student.User.Name + " " + student.User.LastName + " " + student.User.MotherName);
                    body = body.Replace("{pass}", emailDto.Body);

                    Email email = new Email();
                    email.To = reasonDto.Target;
                    email.Subject = reasonDto.Title;
                    email.Body = body;
                    email.IsBodyHtml = true;
                    _emailService.SendEmail(email);

                    response.Message = "El correo se ha enviado correctamente";
                    response.Result = id;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Hubo un error, intenta mas tarde";
                }
            }
            catch (Exception ex)
            {
                response.Result = 0;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("Comunication")]
        public ActionResult<ApiResponse<ComunicationDto>> Comunication()
        {
            var response = new ApiResponse<ComunicationDto>();
            try
            {
                ComunicationDto dto = new ComunicationDto();
                dto.Whatsapp = "5583274711";

                dto.Phones.Add("018004074310");
                dto.Phones.Add("018008347024");
                response.Result = dto;
                response.Message = "Informacion correcta";
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }
            return Ok(response);
        }


        [HttpPost("UploadFiles")]
        public async Task<ActionResult<ApiResponse<List<string[]>>>> UploadFiles(IFormFile certificado, IFormFile foto, IFormFile acta, IFormFile domicilio)
        {
            var response = new ApiResponse<List<string[]>>();
            List<string[]> lstRutas = new List<string[]>();
            try
            {
                var filePath = Environment.CurrentDirectory;
                //string servePath = Request.Host.Value();
                List<IFormFile> fotos = new List<IFormFile>();
                if(certificado != null) fotos.Add(certificado);
                if (foto != null) fotos.Add(foto);
                if (acta != null) fotos.Add(acta);
                if (domicilio != null) fotos.Add(domicilio);
                string servePath = Request.IsHttps ? "https://" : "http://" + Request.Host.ToUriComponent();

                foreach (var file in fotos)
                {
                    var extencion = file.FileName.Split(".");
                    var _guid = Guid.NewGuid();
                    var path = "/Imagenes/Documentos/" + _guid + "." + extencion[1];
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(filePath + path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        path = path.Replace("/Imagenes", "/mxm");
                        string[] doc = new string[2];
                        doc[0] = extencion[0];
                        doc[1] = servePath + path;
                        lstRutas.Add(doc);
                    }
                }
                
                response.Result = lstRutas;
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                //response.Message = "Internal server error";
                response.Message = ex.Message;
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        private async Task<string> GuardarImagen(IFormFile file)
        {
            try
            {
                var filePath = Environment.CurrentDirectory;
                string servePath = Request.IsHttps ? "https://" : "http://" + Request.Host.ToUriComponent();
                var extencion = file.FileName.Split(".");
                var _guid = Guid.NewGuid();
                var path = "/Imagenes/Documentos/" + _guid + "." + extencion[1];
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath + path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    path = path.Replace("/Imagenes", "/mxm");
                    return servePath + path;
                    //Document document = _documentRepository.Find(d => d.DocumentTypeId == type && d.StudentId == studenId);
                    //if (document != null)
                    //{
                    //    document.Resource = servePath + path;
                    //    //_mapper.Map(item, document);
                    //    _documentRepository.Update(document, document.Id);
                    //}
                    //else
                    //{
                    //    document = new Document();
                    //    document.Resource = servePath + path;
                    //    document.StudentId = studenId;
                    //    document.DocumentTypeId = type;
                    //    _documentRepository.Add(document);
                    //}

                }
                else
                    return "error";

            }
            catch (Exception ex)
            {
                return "error inesperado:" + ex.Message;
            }
        }

        [HttpPost("Upload")]
        public async Task<ActionResult<ApiResponse<List<string[]>>>> Upload(List<IFormFile> files)
        {
            var response = new ApiResponse<List<string[]>>();
            List<string[]> lstRutas = new List<string[]>();
            try
            {
                var filePath = Environment.CurrentDirectory;
                //string servePath = Request.Host.Value();

                string servePath = Request.IsHttps ? "https://" : "http://" + Request.Host.ToUriComponent();
                foreach (IFormFile file in files)
                {
                    var extencion = file.FileName.Split(".");
                    var _guid = Guid.NewGuid();
                    var path = "/Imagenes/Documentos/" + _guid + "." + extencion[1];
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(filePath + path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        path = path.Replace("/Imagenes", "/mxm");
                        string[] doc = new string[2];
                        doc[0] = extencion[0];
                        doc[1] = servePath  + path;
                        lstRutas.Add(doc);
                    }
                }
                response.Result = lstRutas;
                //foreach (var file in file)
                //{
                //    if (file.Length >0)
                //    {
                //        using (var stream = new FileStream(filePath + path, FileMode.Create)) ;
                //    }
                //}
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                //response.Message = "Internal server error";
                response.Message = ex.Message;
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }
            return Ok(response);
        }
    }
}