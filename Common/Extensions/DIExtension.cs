using API.Entities.Context;
using API.Repository;
using API.Repository.Impl;
using API.Services;
using API.Services.Impl;

namespace API.Common.Extensions
{
    public static class DIExtension
    {
        public static void AddAllServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContextFactory, ApplicationDbContextFactory>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped<ITaskService, TaskService>();
        }
    }
}
