using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace HandlersMvc
{
    // メモ: IIS6 または IIS7 のクラシック モードの詳細については、
    // http://go.microsoft.com/?LinkId=9394801 を参照してください
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection && !string.Equals(Request.Url.Host, "localhost", StringComparison.InvariantCultureIgnoreCase))
            {
                // Uri.OriginalString プロパティを使用すると、:80 が追加されてしまうことがあります。
                var secureUrl = Regex.Replace(Request.Url.AbsoluteUri, @"^\w+(?=://)", Uri.UriSchemeHttps);

                if (PermanentHttps)
                {
                    Response.RedirectPermanent(secureUrl, true);
                }
                else
                {
                    Response.Redirect(secureUrl, true);
                }
            }
        }

        static bool PermanentHttps
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["app:PermanentHttps"]); }
        }
    }
}