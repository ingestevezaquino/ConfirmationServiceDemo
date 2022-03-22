namespace CSScheduler.Services.DiagADSL
{
    public class DiagADSLService : IDiagADSLService
    {
        private readonly HttpClient _client;

        public DiagADSLService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("DiagADSLRestClient");
            _client.BaseAddress = new Uri("http://diag-adsl-micro/");
        }

        public async Task ExecProcessDiagADSL() 
        {
            await _client.GetStringAsync("api/job");
        }
    }
}
