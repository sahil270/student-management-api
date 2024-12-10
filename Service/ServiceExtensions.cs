using Microsoft.Extensions.DependencyInjection;

namespace Service
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<IStudentService, StudentService>();
        }
    }
}
