using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mxm.api.ActionFilter;
using mxm.api.Models;
using mxm.biz.Repository;
using mxm.biz.Servicies;
using System;
using System.Collections.Generic;

namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ICourseRepository _courseRepository;

        public CourseController(
            IMapper mapper,
            ILoggerManager logger,
            ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<CourseDto>>> GetAll()
        {
            var response = new ApiResponse<List<CourseDto>>();

            try
            {
                response.Result = _mapper.Map<List<CourseDto>>(_courseRepository.GetAll());
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

        [HttpGet("{projectId}", Name = "GetCourses")]
        public ActionResult<ApiResponse<List<CourseDto>>> GetById(int projectId)
        {
            var response = new ApiResponse<List<CourseDto>>();

            try
            {
                response.Result = _mapper.Map<List<CourseDto>>(_courseRepository.FindBy(c => c.ProjectId == projectId));
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