using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

public class StaticFileHandler2 : IHttpHandler
{
    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        // 任意の処理。

        ProcessRequestOriginal(context);

        // 任意の処理。
    }

    void ProcessRequestOriginal(HttpContext context)
    {
        var systemWeb = Assembly.GetAssembly(typeof(IHttpHandler));
        var orgType = systemWeb.GetType("System.Web.StaticFileHandler");

        var orgHandler = Activator.CreateInstance(orgType, true);
        orgType.GetMethod("ProcessRequest").Invoke(orgHandler, new[] { context });
    }
}
