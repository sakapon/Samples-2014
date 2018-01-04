using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SslRedirect
{
    public class RequireHttps2Attribute : FilterAttribute, IAuthorizationFilter
    {
        // true: 301, false: 302
        public bool IsPermanent { get; private set; }

        public RequireHttps2Attribute(bool isPermanent = false)
        {
            IsPermanent = isPermanent;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");

            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                // Uri.OriginalString プロパティを使用すると、:80 が追加されてしまうことがあります。
                var url = filterContext.HttpContext.Request.Url.AbsoluteUri;
                var secureUrl = Regex.Replace(url, @"^\w+(?=://)", Uri.UriSchemeHttps);

                filterContext.Result = new RedirectResult(secureUrl, IsPermanent);
            }
        }
    }
}
