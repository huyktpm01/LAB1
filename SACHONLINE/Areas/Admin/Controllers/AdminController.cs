using SACHONLINE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SACHONLINE.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        
        dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP-4PHTMN7E;Initial Catalog=SachOnline;Integrated Security=True");
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            //Gán các giá trị người dùng nhập liệu cho các biến
            var sTenDN = f["UserName"];
            var sMatKhau = f["Password"];
            //Gán giá trị cho đối tượng được tạo mới (ad)
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau
            == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Admin","Admin");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}