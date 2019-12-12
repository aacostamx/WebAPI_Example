using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mxm.api.ActionFilter;
using mxm.api.Models;
using mxm.biz.Entities;
using mxm.biz.Paged;
using mxm.biz.Repository;
using mxm.biz.Servicies;
using System;
using System.Collections.Generic;
using System.IO;

namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IUserRepository _userRepository;
        private readonly IHierarchyRepository _hierarchyRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEmailService _emailService;
        //TODO add IDistrictRepository

        public UserController(
            IMapper mapper,
            ILoggerManager logger,
            IUserRepository userRepository,
            IHierarchyRepository hierarchyRepository,
            IPermissionRepository permissionRepository,
            ICompanyRepository companyRepository,
            IProjectRepository projectRepository,
            ICourseRepository courseRepository,
            IEmailService emailService)
        {
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
            _hierarchyRepository = hierarchyRepository;
            _permissionRepository = permissionRepository;
            _companyRepository = companyRepository;
            _projectRepository = projectRepository;
            _courseRepository = courseRepository;
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<UserDto>>> GetAll()
        {
            var response = new ApiResponse<List<UserDto>>();

            try
            {
                response.Result = _mapper.Map<List<UserDto>>(_userRepository.GetAll());
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

        [HttpPost("UsersAdmin")]
        public ActionResult<ApiResponse<List<UserAdminDto>>> UsersAdmin(HierarchyDto hierarchy)
        {
            var response = new ApiResponse<List<UserAdminDto>>();

            try
            {
                var res1 = _userRepository.FindByHierarchy(hierarchy.CompanyId, hierarchy.ProjectId, hierarchy.CourseId);
                List<UserAdminDto> dtos = _mapper.Map<List<UserAdminDto>>(res1);
                
                foreach (var item in dtos)
                {
                    switch (item.Hierarchies[0].CourseId)
                    {
                        case 0:
                            item.Level = "Proyecto";
                            item.CourseName = "Todos";
                            break;
                        default:
                            item.Level = "Curso";
                            item.CourseName = (_courseRepository.Find(c => c.Id == item.Hierarchies[0].CourseId).Name);
                            break;
                    }
                    switch (item.Hierarchies[0].ProjectId)
                    {
                        case 0:
                            item.Level = "Entidad";
                            item.ProjectName = "Todos";
                            break;
                        default:
                            item.Level = "Proyecto";
                            item.ProjectName = (_projectRepository.Find(c => c.Id == item.Hierarchies[0].ProjectId).Name);
                            break;
                    }
                    switch (item.Hierarchies[0].CompanyId)
                    {
                        case 0:
                            item.Level = "Global";
                            item.CompanyName = "Todos";
                            break;
                        default:
                            item.Level = "Entidad";
                            item.CompanyName = (_companyRepository.Find(c => c.Id == item.Hierarchies[0].CompanyId).Name);
                            break;
                    }

                }
                response.Result = dtos;
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


        [HttpGet("{pageNumber}/{pageSize}")]
        public ActionResult<ApiResponse<PagedList<UserDto>>> GetPaged(int pageNumber, int pageSize)
        {
            var response = new ApiResponse<PagedList<UserDto>>();

            try
            {
                response.Result = _mapper.Map<PagedList<UserDto>>
                    (_userRepository.GetAllPaged(pageNumber, pageSize));
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

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<ApiResponse<UserDto>> GetById(int id)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                response.Result = _mapper.Map<UserDto>(_userRepository.Find(c => c.Id == id));
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



        //[HttpGet("{email}/{password}")]
        //public ActionResult<ApiResponse<UserDto>> GetByEmail(string email, string password)
        //{
        //    var response = new ApiResponse<UserDto>();

        //    try
        //    {
        //        response.Result = _mapper.Map<UserDto>(_userRepository.Find(c => c.Email == email && c.Password == password));
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

        [HttpPost("Login")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public ActionResult<ApiResponse<UserDto>> Login(UserLoginDto item)
        {
            var response = new ApiResponse<UserDto>();
            try
            {

                //UserDto usr = _mapper.Map<UserDto>(_userRepository.Find(c => c.Email == item.Email && c.Password == item.Password));
                User usr = _userRepository.Find(u => u.Email == item.Email);
                if (usr != null)
                {
                    if(_userRepository.VerifyPassword(usr.Password, item.Password))
                    {
                        if (usr.Active == true)
                        {
                            if (usr.RolId == 1 || usr.RolId == 2)
                            {
                                response.Result = _mapper.Map<UserDto>(usr);
                            }
                            else
                            {
                                response.Success = false;
                                response.Message = "Usuario no permitido";
                            }

                        }
                        else
                        {
                            response.Success = false;
                            response.Message = "Cuenta inactiva";
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "credenciales erroneas";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Usuario no existe";
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

        [HttpPost("Change/{id}")]
        public ActionResult<ApiResponse<bool>> ChangePassword(int id, UserChangePasswordDto user)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var dto = _userRepository.Find(us => us.Id == id);
                if (dto != null)
                {
                    if (_userRepository.VerifyPassword(dto.Password, user.oldPassword))
                    {
                        dto.Password = _userRepository.HashPassword(user.newPassword);

                        StreamReader reader = new StreamReader(Path.GetFullPath("Templates/Email.html"));
                        //_hierarchyRepository.Update(hie, item.Hierarchies[0].Id);
                        _userRepository.Update(dto, id);
                        response.Success = true;
                        response.Message = "La contraseña se actualizó correctamente";
                        response.Result = true;

                        string body = string.Empty;
                        body = reader.ReadToEnd();
                        body = body.Replace("{username}", "Saludos, " + dto.Name + " " + dto.LastName + " " + dto.MotherName);
                        body = body.Replace("{pass}", "Tu nueva contraseña es " + user.newPassword);

                        Email mail = new Email();
                        mail.To = dto.Email;
                        mail.Subject = "Cambio de contraseña";
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        _emailService.SendEmail(mail);
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "La contraseña no coincide, favor de verificar";
                        response.Result = false;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "El usuario no existe, favor de verificar";
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

        [HttpPost("Restore")]
        public ActionResult<ApiResponse<bool>> RestorePassword(UserLoginDto userRestore)
        {
            var response = new ApiResponse<bool>();
            try
            {
                UserDto usr = _mapper.Map<UserDto>(_userRepository.Find(u => u.Email == userRestore.Email));
                if (usr != null)
                {
                    if (usr.Active == true)
                    {
                        StreamReader reader = new StreamReader(Path.GetFullPath("Templates/Email.html"));
                        var guid = Guid.NewGuid().ToString().Substring(0, 8);
                        usr.Password = _userRepository.HashPassword(guid);
                        
                        _userRepository.Update(_mapper.Map<User>(usr), usr.Id);

                        response.Message = "Tu password se ha restablecido correctamente";
                        response.Result = true;
                        string body = string.Empty;
                        body = reader.ReadToEnd();
                        body = body.Replace("{username}", "Saludos, " + usr.Name + " " + usr.LastName + " " + usr.MotherName);
                        body = body.Replace("{pass}", "Tu nueva contraseña es " + guid);

                        Email mail = new Email();
                        mail.To = usr.Email;
                        mail.Subject = "Restablecer contraseña";
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        _emailService.SendEmail(mail);
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Usuario desactivado";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "usuario no existe";
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

        [HttpPost]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public ActionResult<ApiResponse<UserDto>> Create(UserCreateDto item)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                //if (_userRepository.Exists(c => c.Curp == item.Curp))
                //{
                //    response.Success = false;
                //    response.Message = $"CURP: { item.Curp } Already Exists";
                //    return BadRequest(response);
                //}

                //if (_userRepository.Exists(c => c.Email == item.Email))
                //{
                //    response.Success = false;
                //    response.Message = $"Email: { item.Email } Already Exists";
                //    return BadRequest(response);
                //}
                //if (item.sDateOfBirth != null && item.sDateOfBirth != "")
                //{
                //    item.DateOfBirth = Convert.ToDateTime(item.sDateOfBirth);
                //}
                User user = _userRepository.Add(_mapper.Map<User>(item));
                response.Result = _mapper.Map<UserDto>(user);
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

        [HttpPut("{id}")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public ActionResult<ApiResponse<UserDto>> Update(int id, UserDto item)
        {
            var response = new ApiResponse<UserDto>();
            UserUpdateDto userDto = _mapper.Map<UserUpdateDto>(item);
            HierarchyUpdateDto hierarchyUpdateDto = _mapper.Map<HierarchyUpdateDto>(item.Hierarchies[0]);
            var permissions = _mapper.Map<List<PermissionUpdateDto>>(item.Hierarchies[0].Permissions);
            try
            {
                var user = _userRepository.Find(c => c.Id == id);

                //TODO Pending validate District Id

                if (user == null)
                {
                    response.Message = $"User id { id } Not Found";
                    return NotFound(response);
                }
                var hie = _hierarchyRepository.Find(h => h.Id == item.Hierarchies[0].Id);
                if (hie == null)
                {
                    response.Message = $"User id { id } Not Found";
                    return NotFound(response);
                }
                //var per = _permissionRepository.FindBy(p => p.HierarchyId == item.Hierarchies[0].Id);
                //if (hie == null)
                //{
                //    response.Message = $"User id { id } Not Found";
                //    return NotFound(response);
                //}
                int i = 0;
                foreach (PermissionUpdateDto permission in permissions)
                {
                    var per = _permissionRepository.Find(p => p.Id == item.Hierarchies[0].Permissions[i].Id);
                    if (per == null)
                    {
                        //insert
                        Permission permissioncreate = _permissionRepository.Add(_mapper.Map<Permission>(permission));
                        //response.Result = _mapper.Map<UserDto>(user);
                    }
                    else
                    {
                        //update
                        _mapper.Map(permission, per);
                        _permissionRepository.Update(per, item.Hierarchies[0].Permissions[i].Id);

                    }
                    i += 1;
                }

                _mapper.Map(hierarchyUpdateDto, hie);
                _hierarchyRepository.Update(hie, item.Hierarchies[0].Id);

                _mapper.Map(userDto, user);
                _userRepository.Update(user, id);
                response.Result = _mapper.Map<UserDto>(_userRepository.Find(u => u.Id == id));
                //Hierarchy hierarchy = _mapper.Map<Hierarchy>(item.Hierarchies[0]);
                //_hierarchyRepository.Update(hierarchy, hierarchy.Id);
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

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<UserDto>> Delete(int id)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                var user = _userRepository.Find(c => c.Id == id);

                if (user == null)
                {
                    response.Message = $"User id { id } Not Found";
                    return NotFound(response);
                }

                _userRepository.Delete(user);
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
    }
}