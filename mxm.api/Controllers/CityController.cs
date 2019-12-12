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
    public class CityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ICityRepository _cityRepository;

        public CityController(
            IMapper mapper,
            ILoggerManager logger,
            ICityRepository cityRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _cityRepository = cityRepository;
        }

        [HttpGet("{stateId}")]
        public ActionResult<ApiResponse<List<CityDto>>> GetBy(int stateId)
        {
            var response = new ApiResponse<List<CityDto>>();

            try
            {
                response.Result = _mapper.Map<List<CityDto>>(_cityRepository.FindBy(c=> c.StateId == stateId));
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