using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mxm.api.Models;
using mxm.biz.Entities;
using mxm.biz.Repository;
using mxm.biz.Servicies;

namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HierarchyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IHierarchyRepository _hierarchyRepository;

        public HierarchyController(
            IMapper mapper,
            ILoggerManager logger,
            IHierarchyRepository hierarchyRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _hierarchyRepository = hierarchyRepository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<HierarchyDto>>> GetAll()
        {
            var response = new ApiResponse<List<HierarchyDto>>();
            try
            {
                response.Result = _mapper.Map<List<HierarchyDto>>(_hierarchyRepository.GetAll());
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
        [HttpGet("{userId}", Name = "GetHierarchyUser")]
        public ActionResult<ApiResponse<HierarchyDto>> GetByUserId(int userId)
        {
            var response = new ApiResponse<HierarchyDto>();
            try
            {
                response.Result = _mapper.Map<HierarchyDto>(_hierarchyRepository.Find(c => c.UserId == userId));
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