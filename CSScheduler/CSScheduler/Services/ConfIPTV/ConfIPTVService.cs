namespace CSScheduler.Services.ConfIPTV
{
    public class ConfIPTVService : IConfIPTVService
    {
        private readonly HttpClient _client;

        public ConfIPTVService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("DiagADSLRestClient");
            _client.BaseAddress = new Uri("http://diag-adsl-micro/");
        }

        public async Task ExecProcessConfIPTV() 
        {
            await _client.GetStringAsync("api/job");
        }
    }
}
