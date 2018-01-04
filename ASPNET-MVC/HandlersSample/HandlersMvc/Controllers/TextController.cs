using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HandlersMvc.Controllers
{
    [RoutePrefix("Text")]
    [Route("{action=Index}")]
    public class TextController : Controller
    {
        // GET: /Text/
        public ActionResult Index()
        {
            return RedirectToAction("", "");
        }

        [Route("Echo/{text}")]
        public ActionResult Echo(string text)
        {
            Response.Headers["Expires"] = "-1";

            return Content(text, "text/plain");
        }

        [Route("{name}.txt")]
        public ActionResult GetFile(string name)
        {
            Response.Headers["Expires"] = "-1";

            var text = string.Format("The file name is {0}.", name);
            return Content(text, "text/plain");
        }
    }
}
