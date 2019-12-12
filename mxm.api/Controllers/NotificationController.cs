using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mxm.biz.Repository;
using mxm.biz.Servicies;
using AutoMapper;
using mxm.api.Models;
using System.Globalization;

namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(
            IMapper mapper,
            ILoggerManager logger,
            INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _notificationRepository = notificationRepository;
        }

        [HttpGet("{studentId}")]
        public ActionResult<ApiResponse<List<NotificationDto>>> GetbyStudentId(int studentId)
        {
            var response = new ApiResponse<List<NotificationDto>>();
            try
            {
                List<NotificationDto> dtos = _mapper.Map<List<NotificationDto>>(_notificationRepository.FindBy(n => n.StudentId == studentId));
                //foreach (NotificationDto dto in dtos)
                //{
                //    dto.DateTime = dto.Date.ToString(new CultureInfo("es-MX"));
                //}
                if (dtos.Count > 0)
                {
                    response.Result = dtos;
                }
                else
                {
                    response.Success = false;
                    response.Message = "El estudiante no tiene notificaciones para mostrar";
                }
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