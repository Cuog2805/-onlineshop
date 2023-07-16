using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace baitap_mvc_3.App_Start
{
    public static class CookieHelper
    {
        public static void Create(string name, string value, DateTime expire)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = expire;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string Get(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if(cookie != null)
            {
                return cookie.Value;
            }
            else
            {
                return "";
            }
        }
        public static void Remove(string name)
        {
            if (HttpContext.Current.Request.Cookies[name] != null)
            {
                var cookie = new HttpCookie(name)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}