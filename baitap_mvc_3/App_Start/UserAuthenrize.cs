using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap_mvc_3.App_Start
{
    public class UserAuthenrize: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Customer customerSession = (Customer)HttpContext.Current.Session["customer-login"];
            string email = CookieHelper.Get("customer-email");
            string password = CookieHelper.Get("customer-password");
            Customer customerCookie = db.Customers.SingleOrDefault(m => m.Email == email & m.Password == password);
            if(customerCookie != null)
            {
                customerSession = customerCookie;
                HttpContext.Current.Session["customer-login"] = customerCookie;
            }
            if(customerSession != null)
            {
                return;
            }
            else
            {
                var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            controller = "User",
                            action = "Login",
                            returnUrl = returnUrl.ToString()
                        }));
            }
        }
    }
}