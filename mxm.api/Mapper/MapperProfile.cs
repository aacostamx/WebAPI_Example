using AutoMapper;
using mxm.api.Models;
using mxm.biz.Entities;

namespace mxm.api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            #region Schedule
            CreateMap<Schedule, ScheduleDto>().ReverseMap(); 
            #endregion

            #region Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryEvaluationDto>().ReverseMap();
            #endregion

            CreateMap<City, CityDto>().ReverseMap();
            #region Company
            CreateMap<Company, CompanyDto>().ReverseMap();
            #endregion


            #region Connection
            CreateMap<Connection, ConnectionDto>().ReverseMap();
            CreateMap<Connection, ConnectionCreateDto>().ReverseMap(); 
            #endregion


            #region Content
            CreateMap<Content, ContentDto>().ReverseMap();
            #endregion

            #region ContentDetail
            CreateMap<ContentDetail, ContentDetailDto>().ReverseMap();
            #endregion

            #region ContentType
            CreateMap<ContentType, ContentTypeDto>().ReverseMap();
            #endregion

            #region ContentText
            CreateMap<ContentText, ContentTextDto>().ReverseMap();
            #endregion

            #region Course
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            #endregion

            #region District
            CreateMap<District, DistrictDto>().ReverseMap();
            #endregion

            #region Document
            CreateMap<Document, DocumentDto>().ReverseMap();
            #endregion

            #region DocumentType
            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
            #endregion

            #region EvaluationStudentCourse
            CreateMap<EvaluationStudentCourse, EvaluationStudentCourseDto>().ReverseMap();
            #endregion

            #region EvaluationStudentMatter
            CreateMap<EvaluationStudentMatter, EvaluationStudentMatterDto>().ReverseMap();
            CreateMap<EvaluationStudentMatter, EvaluationMatterStudentMatterDto>().ReverseMap();
            CreateMap<EvaluationStudentMatterDto, EvaluationMatterStudentMatterDto>().ReverseMap();
            #endregion


            #region Hierarchy
            CreateMap<Hierarchy, HierarchyDto>().ReverseMap();
            CreateMap<Hierarchy, HierarchyCreateDto>().ReverseMap();
            CreateMap<Hierarchy, HierarchyUpdateDto>().ReverseMap();
            #endregion

            #region Location
            CreateMap<Location, LocationDto>().ReverseMap(); 
            #endregion

            #region Matter
            CreateMap<Matter, MatterDto>().ReverseMap();
            CreateMap<Matter, MatterEvaluationDto>().ReverseMap();
            #endregion

            #region Notification
            CreateMap<Notification, NotificationDto>().ReverseMap();
            #endregion

            #region Permission
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<Permission, PermissionUpdateDto>().ReverseMap();
            #endregion

            #region Project
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            #endregion

            #region Quote
            CreateMap<Quote, QuoteDto>().ReverseMap();
            CreateMap<Quote, QuoteCreateDto>().ReverseMap();
            CreateMap<Quote, QuoteUpdateDto>().ReverseMap();
            #endregion

            #region Reason
            CreateMap<Reason, ReasonDto>().ReverseMap(); 
            #endregion

            #region Screen
            CreateMap<Screen, ScreenDto>().ReverseMap();
            #endregion

            CreateMap<State, StateDto>().ReverseMap();

            #region Student
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Student, StudentUpdateDto>().ReverseMap();
            CreateMap<Student, StudentCreateDto>().ReverseMap();
            CreateMap<Student, StudentProfileDto>().ReverseMap();
            CreateMap<StudentDto, StudentUpdateDto>().ReverseMap();
            CreateMap<StudentProfileDto, StudentUpdateDto>().ReverseMap();
            #endregion

            #region StudentContent
            CreateMap<StudentContent, StudentContentDto>().ReverseMap();
            #endregion

            #region StudentContentDetail
            CreateMap<StudentContentDetail, StudentContentDetailDto>().ReverseMap();
            CreateMap<StudentContentDetail, StudentContentDetailCreateDto>().ReverseMap();
            #endregion


            #region StudentCourse
            CreateMap<StudentCourse, StudentCourseDto>().ReverseMap();
            #endregion

            #region StudentMatter
            CreateMap<StudentMatter, StudentMatterDto>().ReverseMap();
            
            #endregion

            #region StudentSubTopic
            CreateMap<StudentSubTopic, StudentSubTopicDto>().ReverseMap(); 
            #endregion

            #region StudentTopic
            CreateMap<StudentTopic, StudentTopicDto>().ReverseMap();
            #endregion

            #region SubTopic
            CreateMap<SubTopic, SubTopicDto>().ReverseMap();
            CreateMap<SubTopic, SubTopicProgressDto>().ReverseMap();
            #endregion

            #region Topic
            CreateMap<Topic, TopicDto>().ReverseMap();
            CreateMap<Topic, TopicProgressDto>().ReverseMap();
            #endregion



            #region User
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserAdminDto>().ReverseMap();
            CreateMap<User, UserStudentDto>().ReverseMap();
            CreateMap<User, UserProfileDto>().ReverseMap();
            CreateMap<UserDto, UserStudentDto>().ReverseMap();
            #endregion

            #region Token
            CreateMap<Token, TokenDto>().ReverseMap();
            CreateMap<Token, TokenCreateDto>().ReverseMap();
            #endregion




        }
    }
}
