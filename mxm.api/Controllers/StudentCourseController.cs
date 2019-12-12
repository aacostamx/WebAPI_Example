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
    public class StudentCourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IEvaluationStudentCourseRepository _evaluationStudentCourseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public StudentCourseController(
            IMapper mapper,
            ILoggerManager logger,
            IStudentCourseRepository studentCourseRepository,
            IEvaluationStudentCourseRepository evaluationStudentCourseRepository,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _studentCourseRepository = studentCourseRepository;
            _categoryRepository = categoryRepository;
            _evaluationStudentCourseRepository = evaluationStudentCourseRepository;
        }

        [HttpGet("{studentId}")]
        public ActionResult<ApiResponse<List<StudentCourseDto>>> GetbyStudentId(int studentId)
        {
            var response = new ApiResponse<List<StudentCourseDto>>();
            try
            {
                List<StudentCourseDto> dtos = _mapper.Map<List<StudentCourseDto>>(_studentCourseRepository.FindStudent(studentId));
                if (dtos.Count >0 )
                {
                    foreach (StudentCourseDto item in dtos)
                    {
                        item.EvaluationStudentCourse = _mapper.Map<EvaluationStudentCourseDto>
                            (_evaluationStudentCourseRepository.FindBy(esc => esc.StudentId == studentId 
                            && esc.CourseId == item.CourseId).LastOrDefault());
                        //if (item.EvaluationStudentCourse != null)
                        //{
                        //    item.EvaluationStudentCourse.Category = _mapper.Map<CategoryDto>
                        //        (_categoryRepository.Find(c => c.Id == item.EvaluationStudentCourse.CategoryId));
                        //}
                    }
                    response.Result = dtos;
                }
                else
                {
                    response.Success = false;
                    response.Message = "El estudiante no tiene cursos asociados";
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