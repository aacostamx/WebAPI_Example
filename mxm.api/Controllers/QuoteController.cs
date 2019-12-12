using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mxm.api.ActionFilter;
using mxm.api.Models;
using mxm.biz.Entities;
using mxm.biz.Repository;
using mxm.biz.Servicies;

namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IQuoteRepository _quoteRepository;

        public QuoteController(
            IMapper mapper,
            ILoggerManager logger,
            IQuoteRepository quoteRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _quoteRepository = quoteRepository;
        }

        [HttpPost]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public ActionResult<ApiResponse<QuoteDto>> Create(QuoteCreateDto item)
        {
            var response = new ApiResponse<QuoteDto>();
            try
            {
                Quote quote = _quoteRepository.Add(_mapper.Map<Quote>(item));
                response.Result = _mapper.Map<QuoteDto>(quote);
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

        [HttpGet("{studentId}")]
        public ActionResult<ApiResponse<QuoteDto>> GetBy(int studentId)
        {
            var response = new ApiResponse<QuoteDto>();
            try
            {
                var res = _mapper.Map<QuoteDto>(_quoteRepository.FindBy(qu => qu.StudentId == studentId).LastOrDefault());
                response.Result = res;
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