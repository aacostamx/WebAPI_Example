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
using mxm.biz.Paged;

namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentDetailController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ISubTopicRepository _subTopicRepository;
        private readonly IContentDetailRepository _contentDetailRepository;
        //private readonly IStudentSubTopicRepository _studentSubTopicRepository;

        public ContentDetailController(
            IMapper mapper,
            ILoggerManager logger,
            ISubTopicRepository subTopicRepository,
            IContentDetailRepository contentDetailRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _subTopicRepository = subTopicRepository;
            _contentDetailRepository = contentDetailRepository;
            //_studentSubTopicRepository = studentSubTopicRepository;
        }


        [HttpGet("{contentId}/{pageNumber},{pageSize}")]
        public ActionResult<ApiResponse<PagedList<ContentDetailDto>>> GetPaged(int pageNumber, int pageSize, int contentId)
        {
            var response = new ApiResponse<PagedList<ContentDetailDto>>();
            try
            {
                //var filtro = _contentDetailRepository.FindBy(cd => cd.ContentId == contentId);
                var res = _mapper.Map<PagedList<ContentDetailDto>>(_contentDetailRepository.GetAllPagedFilter(pageNumber, pageSize, contentId));
                if (res.TotalRows > 0)
                {
                    
                    response.Result = res;
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

        [HttpGet("{studentCourseId},{contentId}")]
        public ActionResult<ApiResponse<List<ContentDetailDto>>> GetBy(int studentCourseId, int contentId)
        {
            var response = new ApiResponse<List<ContentDetailDto>>();
            try
            {
                List<ContentDetailDto> dto = _mapper.Map<List<ContentDetailDto>>(_contentDetailRepository.FindBy(t => t.ContentId == contentId));
                if (dto != null)
                {
                    response.Result = dto;
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