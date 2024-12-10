using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Data
{
    public static class ServiceExtensions
    {
        public static void AddDBContextDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StudentManagementDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
