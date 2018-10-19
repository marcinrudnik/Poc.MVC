using Microsoft.AspNetCore.Mvc;

namespace Poc.MVC.ViewComponents.UserApplicationStatusesControl
{
    public class UserApplicationStatusesControlViewComponent : ViewComponent
    {
        public UserApplicationStatusesControlViewComponent()
        {
            
        }

        public IViewComponentResult Invoke(int userId)
        {
            //Thread.Sleep(TimeSpan.FromSeconds(5));

            //if (userId == 100)
            //    throw  new ArgumentOutOfRangeException();
            return View("Default", userId);
        }


    }
}
