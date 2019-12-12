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
    public class LocationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ILocationRepository _locationRepository;

        public LocationController(
            IMapper mapper,
            ILoggerManager logger,
            ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _locationRepository = locationRepository;
        }

        [HttpGet("{cityId}")]
        public ActionResult<ApiResponse<List<LocationDto>>> GetBy(int cityId)
        {
            var response = new ApiResponse<List<LocationDto>>();

            try
            {
                response.Result = _mapper.Map<List<LocationDto>>(_locationRepository.FindBy(l => l.District.CityId == cityId));
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