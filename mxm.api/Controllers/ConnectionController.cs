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
    public class ConnectionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IConnectionRepository _connectionRepository;
        
        public ConnectionController(
            IMapper mapper,
            ILoggerManager logger,
            IConnectionRepository connectionRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _connectionRepository = connectionRepository;
        }

        [HttpPost]
        //[ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public ActionResult<ApiResponse<ConnectionDto>> Create(ConnectionCreateDto item)
        {
            var response = new ApiResponse<ConnectionDto>();

            try
            {

                Connection user = _connectionRepository.Add(_mapper.Map<Connection>(item));
                response.Result = _mapper.Map<ConnectionDto>(user);
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