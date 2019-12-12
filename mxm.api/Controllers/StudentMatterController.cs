using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mxm.api.Models;
using mxm.biz.Repository;
using mxm.biz.Servicies;

namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMatterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IStudentMatterRepository _studentMatterRepository;

        public StudentMatterController(
            IMapper mapper,
            ILoggerManager logger,
            IStudentMatterRepository studentMatterRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _studentMatterRepository = studentMatterRepository;
        }

        [HttpGet("{studentCourseId}")]
        public ActionResult<ApiResponse<List<StudentMatterDto>>> GetByStudentCourseId(int studentCourseId)
        {
            var response = new ApiResponse<List<StudentMatterDto>>();
            try
            {
                List<StudentMatterDto> dto = _mapper.Map<List<StudentMatterDto>>(_studentMatterRepository.FindStudentCourse(studentCourseId));
                if (dto != null)
                {
                    response.Result = dto;
                }
                else
                {
                    response.Success = false;
                    response.Message = "El estudiante no tiene materias asociados";
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
    }
}