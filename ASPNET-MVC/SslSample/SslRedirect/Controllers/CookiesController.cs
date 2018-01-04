using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SslRedirect.Controllers
{
    public class CookiesController : Controller
    {
        public ActionResult Index(string v, string vs)
        {
            if (!string.IsNullOrWhiteSpace(v))
            {
                Response.SetCookie(new HttpCookie("v", v) { HttpOnly = false, Secure = false });
            }
            if (!string.IsNullOrWhiteSpace(vs))
            {
                Response.SetCookie(new HttpCookie("vs", vs));
            }

            return View();
        }
    }
}
