using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Poc.MVC.UserApplicationStatusControl.Services;

namespace Poc.MVC.UserApplicationStatusControl
{
    public class UserApplicationStatusesControlViewComponent : ViewComponent
    {
        private readonly IApplicationStatusesService _applicationStatusesService;

        public UserApplicationStatusesControlViewComponent(IApplicationStatusesService applicationStatusesService)
        {
            _applicationStatusesService = applicationStatusesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            var applicationStatuses = await _applicationStatusesService.GetFakeApplications(userId).ConfigureAwait(false);
            return View("Default", applicationStatuses);
        }
    }
}
