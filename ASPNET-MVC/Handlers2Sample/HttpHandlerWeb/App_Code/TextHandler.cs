using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public class TextHandler : IHttpHandler
{
    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        var fileName = Path.GetFileNameWithoutExtension(context.Request.Path);
        var text = string.Format("The file name is {0}.", fileName);

        context.Response.ContentType = "text/plain";
        context.Response.Write(text);
    }
}
