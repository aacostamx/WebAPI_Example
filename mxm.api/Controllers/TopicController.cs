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
    public class TopicController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ITopicRepository _topicRepository;
        private readonly IStudentTopicRepository _studentTopicRepository;

        public TopicController(
            IMapper mapper,
            ILoggerManager logger,
            ITopicRepository topicRepository,
            IStudentTopicRepository studentTopicRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _topicRepository = topicRepository;
            _studentTopicRepository = studentTopicRepository;
        }

        [HttpGet("{studentCourseId},{matterId}")]
        public ActionResult<ApiResponse<List<TopicProgressDto>>> GetBy(int studentCourseId, int matterId)
        {
            var response = new ApiResponse<List<TopicProgressDto>>();
            try
            {
                List<TopicProgressDto> dtos = _mapper.Map<List<TopicProgressDto>>(_topicRepository.FindBy(t => t.MatterId == matterId));
                if (dtos.Count > 0)
                {
                    foreach (TopicProgressDto item in dtos)
                    {
                        var tp = _mapper.Map<StudentTopicDto>(_studentTopicRepository
                            .FindBy(st => st.StudentCourseId == studentCourseId && st.TopicId == item.Id)
                            .LastOrDefault());
                        if (tp != null)
                        {
                            item.Progress = tp.Progress;
                        }
                    }
                    foreach (TopicProgressDto item in dtos)
                    {
                        if (item.Blocker > 0)
                        {
                            var parent = _topicRepository.Find(p => p.Id == item.Blocker);
                            if (parent != null)
                            {
                                var std = _mapper.Map<StudentTopicDto>(_studentTopicRepository
                                            .FindBy(st => st.StudentCourseId == studentCourseId && st.TopicId == parent.Id)
                                            .LastOrDefault());
                                if (std != null)
                                {
                                    if (std.Progress < 100)
                                        item.Enabled = false;
                                    else
                                        item.Enabled = true;
                                }
                                else
                                {
                                    item.Enabled = false;
                                }
                            }
                            else
                            {
                                item.Enabled = true;
                            }
                        }
                        else
                            item.Enabled = true;
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