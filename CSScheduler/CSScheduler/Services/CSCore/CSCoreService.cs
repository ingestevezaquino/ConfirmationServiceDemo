namespace CSScheduler.Services.CSCore
{
    public class CSCoreService : ICSCoreService
    {
        private readonly HttpClient _client;

        public CSCoreService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("CSCoreRestClient");
            _client.BaseAddress = new Uri("http://cscore-micro/");
        }
        public async Task LoadTickets()
        {
            await _client.GetStringAsync("api/job");
        }
    }
}
