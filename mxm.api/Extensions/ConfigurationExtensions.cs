using Microsoft.Extensions.DependencyInjection;
using mxm.api.ActionFilter;
using mxm.biz.Repository;
using mxm.biz.Servicies;
using mxm.dal.Repository;

namespace mxm.api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IConnectionRepository, ConnectionRepository>();
            services.AddTransient<IContentRepository, ContentRepository>();
            services.AddTransient<IContentDetailRepository, ContentDetailRepository>();
            services.AddTransient<IContentTypeRepository, ContentTypeRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();

            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddTransient<IEvaluationStudentCourseRepository, EvaluationStudentCourseRepository>();
            services.AddTransient<IEvaluationStudentMatterRepository, EvaluationStudentMatterRepository>();
            services.AddTransient<IHierarchyRepository, HierarchyRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IMatterRepository, MatterRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IQuoteRepository, QuoteRepository>();
            services.AddTransient<IReasonRepository, ReasonRepository>();
            services.AddTransient<IScreenRepository, ScreenRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentContentRepository, StudentContentRepository>();
            services.AddTransient<IStudentCourseRepository, StudentCourseRepository>();
            services.AddTransient<IStudentContentDetailRepository, StudentContentDetailRepository>();
            services.AddTransient<IStudentMatterRepository, StudentMatterRepository>();
            services.AddTransient<IStudentSubTopicRepository, StudentSubTopicRepository>();
            services.AddTransient<IStudentTopicRepository, StudentTopicRepository>();
            services.AddTransient<ISubTopicRepository, SubTopicRepository>();
            services.AddTransient<ITopicRepository, TopicRepository>();
            services.AddTransient<ITutorialRepository, TutorialRepository>();


        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
        }
    }
}
