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
    public class ReasonController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IReasonRepository _reasonRepository;

        public ReasonController(
            IMapper mapper,
            ILoggerManager logger,
            IReasonRepository reasonRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _reasonRepository = reasonRepository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<ReasonDto>>> GetAll()
        {
            var response = new ApiResponse<List<ReasonDto>>();

            try
            {
                response.Result = _mapper.Map<List<ReasonDto>>(_reasonRepository.GetAll());
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