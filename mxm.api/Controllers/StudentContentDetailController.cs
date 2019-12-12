using System;
using System.Collections.Generic;
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
    public class StudentContentDetailController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IStudentContentDetailRepository _studentContentDetailRepository;

        public StudentContentDetailController(
            IMapper mapper,
            ILoggerManager logger,
            IStudentContentDetailRepository studentContentDetailRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _studentContentDetailRepository = studentContentDetailRepository;
        }



        [HttpPost]
        public ActionResult<ApiResponse<StudentContentDetailDto>> Create(StudentContentDetailCreateDto item)
        {
            var response = new ApiResponse<StudentContentDetailDto>();

            try
            {
                var std = _studentContentDetailRepository.Find(scd => scd.StudentCourseId == item.StudentCourseId && scd.ContentDetailId == item.ContentDetailId);
                if (std == null)
                {
                    StudentContentDetail user = _studentContentDetailRepository.Add(_mapper.Map<StudentContentDetail>(item));
                    response.Result = _mapper.Map<StudentContentDetailDto>(user);
                }
                else
                {
                    response.Success = true;
                    response.Message = "El contenido ya fue visto previamente";
                    response.Result = _mapper.Map<StudentContentDetailDto>(std);
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
    }
}