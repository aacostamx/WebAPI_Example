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
    public class TutorialController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ITutorialRepository _tutorialRepository;

        public TutorialController(
            IMapper mapper,
            ILoggerManager logger,
            ITutorialRepository tutorialRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _tutorialRepository = tutorialRepository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<TutorialDto>>> GetAll()
        {
            var response = new ApiResponse<List<TutorialDto>>();

            try
            {
                response.Result = _mapper.Map<List<TutorialDto>>(_tutorialRepository.GetAll());
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