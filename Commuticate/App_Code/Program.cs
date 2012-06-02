using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Settings
/// </summary>
public static class Program
{
    public static bool UserLoggedIn
    {
        get { return HttpContext.Current.Session["UserId"] != null && UserId > 0; }
    }

    public  static int UserId
    {
        get { return (int) HttpContext.Current.Session["UserId"]; }
        set { HttpContext.Current.Session["UserId"] = value; }
    }
}