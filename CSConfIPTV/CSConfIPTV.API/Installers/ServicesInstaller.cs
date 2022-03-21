using CSConfIPTV.Services.Job;

namespace CSConfIPTV.Installers
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
