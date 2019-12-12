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
    public class MatterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IMatterRepository _matterRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IEvaluationStudentMatterRepository _evaluationStudentMatterRepository;
        private readonly IStudentMatterRepository _studentMatterRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MatterController(
            IMapper mapper,
            ILoggerManager logger,
            IMatterRepository matterRepository,
            IStudentCourseRepository studentCourseRepository,
            IEvaluationStudentMatterRepository evaluationStudentMatterRepository,
            IStudentMatterRepository studentMatterRepository,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _studentCourseRepository = studentCourseRepository;
            _matterRepository = matterRepository;
            _evaluationStudentMatterRepository = evaluationStudentMatterRepository;
            _studentMatterRepository = studentMatterRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("{studentCourseId}")]
        public ActionResult<ApiResponse<List<MatterEvaluationDto>>> GetBy(int studentCourseId)
        {
            var response = new ApiResponse<List<MatterEvaluationDto>>();
            try
            {
                var studentCourse = _studentCourseRepository.Find(sc => sc.Id == studentCourseId);
                if (studentCourse != null)
                {
                    List<MatterEvaluationDto> dtos = _mapper.Map<List<MatterEvaluationDto>>(_matterRepository.FindBy(m => m.CourseId == studentCourse.CourseId));
                    if (dtos.Count > 0)
                    {
                        foreach (MatterEvaluationDto item in dtos)
                        {
                            item.Evaluation = _mapper.Map<EvaluationMatterStudentMatterDto>(
                                (_evaluationStudentMatterRepository.
                                FindBy(esm => esm.MatterId == item.Id
                                && esm.StudentId == studentCourse.StudentId)).LastOrDefault());
                            //if (item.Evaluation != null)
                            //{
                            //    item.Evaluation.Category = _mapper.Map<CategoryEvaluationDto>(_categoryRepository.Find(c => c.Id == item.Evaluation.CategoryId));
                            //}
                            var resProgress = (_studentMatterRepository.FindBy(sm => sm.MatterId == item.Id
                             && sm.StudentCourseId == studentCourseId)).LastOrDefault();
                            if (resProgress != null)
                            {
                                item.Progress = resProgress.Progress;
                            }
                        }
                        response.Result = dtos;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "No hay materias para mostrar, se debe avisar al administrador";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "El estudiante no tiene cursos asignados";
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