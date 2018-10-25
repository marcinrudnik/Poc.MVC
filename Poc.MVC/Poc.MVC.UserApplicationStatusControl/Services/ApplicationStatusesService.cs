using System.Net.Http;
using System.Threading.Tasks;
using GP.Applications.ApplicationsStatus.Contract;
using Newtonsoft.Json;

namespace Poc.MVC.UserApplicationStatusControl.Services
{
    public class ApplicationStatusesService : IApplicationStatusesService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApplicationStatusesService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApplicationsInfo> GetFakeApplications(int userId)
        {
            var applicationStatusesClient = _httpClientFactory.CreateClient("applicationStatuses");

            var response = await applicationStatusesClient.GetAsync("user-applications/fake?limit=500").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                 string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                 return JsonConvert.DeserializeObject<ApplicationsInfo>(json);
            }

            return new ApplicationsInfo();
        }
    }
}
