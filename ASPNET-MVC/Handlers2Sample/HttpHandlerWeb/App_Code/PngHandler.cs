using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

// MEMO: 名前空間を定義しても利用できます。
public class PngHandler : IHttpHandler
{
    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        var w = context.Request.QueryString["w"] ?? "300";
        var h = context.Request.QueryString["h"] ?? "200";
        var width = int.Parse(w);
        var height = int.Parse(h);

        var fileName = Path.GetFileNameWithoutExtension(context.Request.Path);
        var color = Regex.IsMatch(fileName, "^[0-9A-Fa-f]{6}$") ? ToColor(fileName) : Color.FromName(fileName);
        if (color.A == 0)
        {
            context.Response.StatusCode = 404;
            return;
        }

        context.Response.ContentType = "image/png";
        var bitmap = CreateBitmap(width, height, color);
        bitmap.Save(context.Response.OutputStream, ImageFormat.Png);
    }

    static Color ToColor(string rgb)
    {
        var opaque = 0xFF000000;
        var argb = (int)opaque | Convert.ToInt32(rgb, 16);
        return Color.FromArgb(argb);
    }

    static Bitmap CreateBitmap(int width, int height, Color color)
    {
        var bitmap = new Bitmap(width, height);

        for (int i = 0; i < bitmap.Width; i++)
            for (int j = 0; j < bitmap.Height; j++)
                bitmap.SetPixel(i, j, color);

        return bitmap;
    }
}
