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
    public class ScreenController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IScreenRepository _screenRepository;

        public ScreenController(
            IMapper mapper,
            ILoggerManager logger,
            IScreenRepository screenRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _screenRepository = screenRepository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<ScreenDto>>> GetAll()
        {
            var response = new ApiResponse<List<ScreenDto>>();

            try
            {
                List<ScreenDto> dtos = _mapper.Map<List<ScreenDto>>(_screenRepository.GetAll());
                foreach (var item in dtos)
                {
                    item.ScreenId = item.Id;
                    item.Id = 0;
                }
                response.Result = dtos; 
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