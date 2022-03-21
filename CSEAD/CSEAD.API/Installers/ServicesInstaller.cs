using CSEAD.Persistence;
using CSEAD.Persistence.Interfaces;
using CSEAD.Services.Diagnostics;
using CSEAD.Services.Facilities;

namespace CSEAD.API.Installers
{
    public static class ServicesInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IDiagnosticService, DiagnosticService>();
            return services;
        }
    }
}
