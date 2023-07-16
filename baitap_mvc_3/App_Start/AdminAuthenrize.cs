using baitap_mvc_3.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap_mvc_3.App_Start
{
    public class AdminAuthenrize : AuthorizeAttribute
    {
        public int idChucnang { set; get; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Nhanvien nvSession = (Nhanvien)HttpContext.Current.Session["admin"];
            //nếu user chưa có trong session -> kiểm tra cookie
            string username = CookieHelper.Get("username-cookie");
            string pass = CookieHelper.Get("pass-cookie");
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Nhanvien taiKhoan = db.Nhanviens.SingleOrDefault(m => m.Username == username);
            if(taiKhoan != null)
            {
                if (taiKhoan.Password == pass)
                {
                    nvSession = taiKhoan;
                    HttpContext.Current.Session["admin"] = taiKhoan;
                }
            }
            //check session: đã đăng nhập -> cho thực hiện Filter
            //chưa đăng nhập -> trở về Login
           
            if (nvSession != null)
            {
                #region//check quyền: có quyền thì -> thực hiện Filter
                //không có quyền -> trở lại trang đăng nhập
                var count = db.Phanquyens.Count(m => m.idNhanvien == nvSession.ID & m.idChucnang == idChucnang);
                if (count != 0)
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
                                controller = "AdminHome",
                                action = "PermissionError",
                                area = "Admin",
                                returnUrl = returnUrl.ToString()
                            }));
                }
                #endregion
            }
            else
            {
                var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            controller = "AdminHome",
                            action = "Login",
                            area = "Admin",
                            returnUrl = returnUrl.ToString()
                        }));
            }
        }
    }
}