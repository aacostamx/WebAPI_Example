using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mxm.api.Models;
using mxm.biz.Paged;
using mxm.biz.Repository;
using mxm.biz.Servicies;


namespace mxm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IContentRepository _contentRepository;

        public ContentController(
            IMapper mapper,
            ILoggerManager logger,
            IContentRepository contentRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _contentRepository = contentRepository;
        }

        [HttpGet("{subTopicId}/{pageNumber},{pageSize}")]
        public ActionResult<ApiResponse<PagedList<ContentDto>>> GetPaged(int pageNumber, int pageSize, int subTopicId)
        {
            var response = new ApiResponse<PagedList<ContentDto>>();
            try
            {
                //var filtro = _contentDetailRepository.FindBy(cd => cd.ContentId == contentId);
                var res = _mapper.Map<PagedList<ContentDto>>(_contentRepository.GetAllPagedFilter(pageNumber, pageSize, subTopicId));
                if (res.TotalRows >0)
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

        [HttpGet("{studentCourseId},{subTopicId}")]
        public ActionResult<ApiResponse<List<ContentDto>>> GetBy(int studentCourseId, int subTopicId)
        {
            var response = new ApiResponse<List<ContentDto>>();
            try
            {
                List<ContentDto> dto = _mapper.Map<List<ContentDto>>(_contentRepository.FindBy(t => t.SubTopicId  == subTopicId));
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