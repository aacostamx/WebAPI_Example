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
    public class FeatController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IEvaluationStudentCourseRepository _evaluationStudentCourseRepository;
        private readonly IEvaluationStudentMatterRepository _evaluationStudentMatterRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMatterRepository _matterRepository;


        public FeatController(
            IMapper mapper,
            ILoggerManager logger,
            IStudentCourseRepository studentCourseRepository,
            IEvaluationStudentCourseRepository evaluationStudentCourseRepository,
            IEvaluationStudentMatterRepository evaluationStudentMatterRepository,
            ICategoryRepository categoryRepository, 
            IMatterRepository matterRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _studentCourseRepository = studentCourseRepository;
            _categoryRepository = categoryRepository;
            _matterRepository = matterRepository; 
            _evaluationStudentCourseRepository = evaluationStudentCourseRepository;
            _evaluationStudentMatterRepository = evaluationStudentMatterRepository;
        }

        [HttpGet("{studentId},{courseId}")]
        public ActionResult<ApiResponse<StudentCourseFeatDto>> Courses (int studentId, int courseId)
        {
            ApiResponse<StudentCourseFeatDto> response = new ApiResponse<StudentCourseFeatDto>();
            StudentCourseFeatDto result = new StudentCourseFeatDto();
            try
            {
                var res = _mapper.Map<List<EvaluationStudentCourseDto>>(_evaluationStudentCourseRepository.FindBy(esc => esc.StudentId == studentId && esc.CourseId == courseId));

                if (res == null)
                {
                    response.Message = "No hay cursos para mostrar";
                }
                result.Courses = res;
                //var studentCourse = _studentCourseRepository.Find(sc => sc.Id == studentCourseId);
                List<MatterDto> dtos = _mapper.Map<List<MatterDto>>(_matterRepository.FindBy(m=> m.CourseId == courseId));
                if (dtos.Count > 0)
                {
                    foreach (MatterDto item in dtos)
                    {
                        item.Evaluation = _mapper.Map<EvaluationMatterStudentMatterDto>(
                            (_evaluationStudentMatterRepository
                            .FindBy(esm => esm.MatterId == item.Id&& esm.StudentId == studentId && esm.Qualification > 5))
                            .FirstOrDefault());
                    }
                    result.Matters = dtos;
                    //Logros de connexion
                    List<ConnectionTimeDto> timeDtos = new List<ConnectionTimeDto>();
                    timeDtos.Add(new ConnectionTimeDto { Duration = "1 HORA CONECTADO", Icon = "iconounahora", Enabled = true });
                    timeDtos.Add(new ConnectionTimeDto { Duration = "2 HORAS CONECTADO", Icon = "iconodoshoras", Enabled = false });
                    timeDtos.Add(new ConnectionTimeDto { Duration = "3 HORAS CONECTADO", Icon = "iconotreshoras", Enabled = false });
                    timeDtos.Add(new ConnectionTimeDto { Duration = "4 HORAS CONECTADO", Icon = "iconocuatrohoras", Enabled = false });

                    List<ConnectionDayDtos> dayDtos = new List<ConnectionDayDtos>();
                    dayDtos.Add(new ConnectionDayDtos { Duration = "1 DIA CONECTADO", Icon = "iconoundia", Enabled = true });
                    dayDtos.Add(new ConnectionDayDtos { Duration = "2 DIAS CONECTADO", Icon = "iconodosdias", Enabled = true });
                    dayDtos.Add(new ConnectionDayDtos { Duration = "3 DIAS CONECTADO", Icon = "iconotresdias", Enabled = false });
                    dayDtos.Add(new ConnectionDayDtos { Duration = "4 DIAS CONECTADO", Icon = "iconocuatrodias", Enabled = false });
                    result.ConnectionDurations = timeDtos;
                    result.ConnectionDays = dayDtos;
                }


                
                response.Result = result;
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