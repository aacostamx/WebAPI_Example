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
    public class CompanyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(
            IMapper mapper,
            ILoggerManager logger,
            ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<CompanyDto>>> GetAll()
        {
            var response = new ApiResponse<List<CompanyDto>>();

            try
            {
                response.Result = _mapper.Map<List<CompanyDto>>(_companyRepository.GetAll());
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