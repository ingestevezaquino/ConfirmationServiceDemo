using CSCore.Services.Diagnostics;
using CSCore.Services.Job;
using CSCore.Services.Tickets;

namespace CSCore.API.Installers
{
    public static class ServicesInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IDiagnosticService, DiagnosticService>();
            return services;
        }
    }
}
