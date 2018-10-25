using System.Threading.Tasks;
using GP.Applications.ApplicationsStatus.Contract;

namespace Poc.MVC.UserApplicationStatusControl.Services
{
    public interface IApplicationStatusesService
    {
        Task<ApplicationsInfo> GetFakeApplications(int userId);
    }
}
