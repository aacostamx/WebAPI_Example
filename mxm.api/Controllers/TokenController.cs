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
    public class TokenController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;

        public TokenController( IMapper mapper, ILoggerManager logger, IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<TokenDto>>> GetAll()
        {
            var response = new ApiResponse<List<TokenDto>>();

            try
            {
                response.Result = _mapper.Map<List<TokenDto>>(_tokenRepository.GetAll());
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