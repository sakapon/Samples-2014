using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CorsMvc.Controllers
{
    public class HomeController : Controller
    {
        // GET /
        public ActionResult Index()
        {
            return View();
        }

        // GET /Home/FormTest
        public ActionResult FormTest()
        {
            return View();
        }
    }
}
