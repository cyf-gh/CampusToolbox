using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTB.DomainModel.HappyHandingIn;
using CTB.Factory.HappyHandingIn;
using Microsoft.AspNetCore.Mvc;

namespace CTB.Web.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}