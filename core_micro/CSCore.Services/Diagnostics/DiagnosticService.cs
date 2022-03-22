using CSCore.Persistence;
using CSCore.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCore.Services.Diagnostics
{
    public class DiagnosticService : IDiagnosticService
    {
        private readonly HttpClient _httpClient;

        public DiagnosticService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("EADRestClient");
            _httpClient.BaseAddress = new Uri("http://equipments-diagnostics-micro/");
        }

        public async Task<ClientResponse.Diagnostic> GetLastDiagnosticAndFacility(string subcriberNumber)
        {
            string responseJSON = await _httpClient.GetStringAsync($"api/diagnostics/last/{subcriberNumber}");
            var diagnosis = JsonConvert.DeserializeObject<ClientResponse.Diagnostic>(responseJSON);
            return diagnosis;
        }
    }
}
