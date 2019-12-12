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
    public class SubTopicController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ISubTopicRepository _subTopicRepository;
        private readonly IStudentSubTopicRepository _studentSubTopicRepository;

        public SubTopicController(
            IMapper mapper,
            ILoggerManager logger,
            ISubTopicRepository subTopicRepository,
            IStudentSubTopicRepository studentSubTopicRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _subTopicRepository = subTopicRepository;
            _studentSubTopicRepository = studentSubTopicRepository;
        }


        [HttpGet("{studentCourseId},{topicId}")]
        public ActionResult<ApiResponse<List<SubTopicProgressDto>>> GetBy(int studentCourseId, int topicId)
        {
            var response = new ApiResponse<List<SubTopicProgressDto>>();
            try
            {
                List<SubTopicProgressDto> dtos = _mapper.Map<List<SubTopicProgressDto>>(_subTopicRepository.FindBy(t => t.TopicId == topicId));
                if (dtos.Count >0)
                {
                    foreach (SubTopicProgressDto item in dtos)
                    {
                        var sp = (_studentSubTopicRepository
                            .FindBy(st => st.StudentCourseId == studentCourseId && st.SubTopicId == item.Id)
                            .LastOrDefault());
                        if (sp != null)
                        {
                            item.Progress = sp.Progress;
                        }

                    }
                    response.Result = dtos;
                }
                else
                {
                    response.Success = false;
                    response.Message = "No hay resultados para mostrar";
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