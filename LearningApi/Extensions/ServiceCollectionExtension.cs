using DatabaseBroker.Repositories.Learning;
using LearningService.Service;

namespace LearningApi.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped <IArticleService, ArticleService>();
        services.AddScoped <ISeminarVideoService, SeminarVideoService>();
        services.AddScoped <IShortVideoService, ShortVideoService>();
        services.AddScoped <IAuthorService, AuthorService>();
        services.AddScoped <IHashtagService, HashtagService>();
        services.AddScoped <ICourseService, CourseService>();
        services.AddScoped <IVideoOfCourseService, VideoOfCourseService>();
        services.AddScoped <IQuizService, QuizService>();
        services.AddScoped <IExamService, ExamService>();
        
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped <IArticleRepository, ArticleRepository>();
        services.AddScoped <ISeminarVideoRepository, SeminarVideoRepository>();
        services.AddScoped <ICategoryRepository, CategoryRepository>();
        services.AddScoped <IHashtagRepository, HashtagRepository>();
        services.AddScoped <IAuthorRepository, AuthorRepository>();
        services.AddScoped <IShortVideoRepository, ShortVideoRepository>();
        services.AddScoped <ICourseRepository, CourseRepository>();
        services.AddScoped <IVideoOfCourseRepository, VideoOfCourseRepository>();
        services.AddScoped <IQuizRepository, QuizRepository>();
        services.AddScoped <IQuestionRepository, QuestionRepository>();
        services.AddScoped <IQuestionInExamRepository, QuestionInExamRepository>();
        services.AddScoped <IExamRepository, ExamRepository>();
        
        return services;
    }
}