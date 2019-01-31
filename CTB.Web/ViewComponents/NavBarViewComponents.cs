using CTB.Service;
using CTB.Service.NavBar;
using Microsoft.AspNetCore.Mvc;

namespace CTB.Web.ViewComponents {
    public class NavBarViewComponents : ViewComponent {
        private readonly INavBarService _NavBarService;

        public NavBarViewComponents(INavBarService navBarService) {
            this._NavBarService = navBarService;
        }

        public IViewComponentResult Invoke() { 
            return View("Default", _NavBarService);
        }
    }
}
