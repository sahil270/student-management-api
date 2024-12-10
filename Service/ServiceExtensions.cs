using Microsoft.Extensions.DependencyInjection;

namespace Service
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IClassesService, ClassesService>();
        }
    }
}
