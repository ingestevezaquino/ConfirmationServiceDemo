using CSDiagADSL.Services.Job;

namespace CSDiagADSL.Installers
{
    public static class ServicesInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJobService, JobService>();
            return services;
        }
    }
}
