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
namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IProjectRepository _projectRepository;

        public ProjectController(
            IMapper mapper,
            ILoggerManager logger,
            IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<ProjectDto>>> GetAll()
        {
            var response = new ApiResponse<List<ProjectDto>>();

            try
            {
                response.Result = _mapper.Map<List<ProjectDto>>(_projectRepository.GetAll());
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

        [HttpGet("{companyId}", Name = "GetProjects")]
        public ActionResult<ApiResponse<List<ProjectDto>>> GetById(int companyId)
        {
            var response = new ApiResponse<List<ProjectDto>>();

            try
            {
                response.Result = _mapper.Map<List<ProjectDto>>(_projectRepository.FindBy(c => c.CompanyId == companyId));
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