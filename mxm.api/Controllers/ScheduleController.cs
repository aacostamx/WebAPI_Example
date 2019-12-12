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
    public class ScheduleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleController(
            IMapper mapper,
            ILoggerManager logger,
            IScheduleRepository scheduleRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _scheduleRepository = scheduleRepository;
        }

        [HttpGet("{locationId},{courseId}")]
        public ActionResult<ApiResponse<List<ScheduleDto>>> GetBy(int locationId, int courseId)
        {
            var response = new ApiResponse<List<ScheduleDto>>();

            try
            {
                var schedules = _scheduleRepository.FindBy(c => c.LocationId == locationId && c.CourseId == courseId);
                List<ScheduleDto> dtos = new List<ScheduleDto>();
                ScheduleDto dto = new ScheduleDto();
                foreach (Schedule item in schedules)
                {
                    _mapper.Map(item, dto);
                    dto.Date = dto.Date.AddHours(item.Time.Hours).AddMinutes(item.Time.Minutes);
                    dto.Available = dto.Limit - item.Quotes.Count();
                    dtos.Add(dto);
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