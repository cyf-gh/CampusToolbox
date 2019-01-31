using Microsoft.AspNetCore.Mvc;

namespace CTB.Web.Controllers {
    public class AccountController : Controller {
        public AccountController() {

        }
        public IActionResult Login() {
            return View();
        }
        public IActionResult Register() {
            return View();
        }
    }
}
