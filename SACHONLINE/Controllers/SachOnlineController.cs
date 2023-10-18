using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using SACHONLINE.Models;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        //Tao 1 doi tuong chua toan bo CSDL tu bdSachOnline

        dbSachOnlineDataContext data = new dbSachOnlineDataContext("Data Source=LAPTOP010502\\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
        /// <summary>
        /// LaySachMoi
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>List</returns>
        private List<SACH> LaySachMoi(int c)
        {
            return data.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(c).ToList();
        }
        // GET: SachOnline
        public ActionResult Index(int ? page)
        {
            int iSize = 6;
            int iPageNum = (page ?? 1);
            var sach = data.SACHes.OrderByDescending(a => a.NgayCapNhat).ToList();
            return View(sach.ToPagedList(iPageNum, iSize));
       
        }
        public ActionResult ChiTietSach(int id)

        {

            var sach = from s in data.SACHes

                       where s.MaSach == id
                       select s;
            return View(sach.Single());
        }
        public ActionResult SachTheoChuDe(int iMaCD, int? page)
        {
            ViewBag.MaCD = iMaCD;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = from s in data.SACHes where s.MaCD == iMaCD select s;
            return View(sach.ToPagedList(iPageNum, iSize));
        }

        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
        }
        public ActionResult SachTheoNhaXuatBan(int id, int? page)
        {
            ViewBag.MaCD = id;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach.ToPagedList(iPageNum, iSize));
 
        }
        public ActionResult NXBPartial()
        {
            var listNhaXuatBan = from cd in data.NHAXUATBANs select cd;
            return PartialView(listNhaXuatBan);
        }
        public ActionResult SachBanNhieuPartial()
        {
            var listSachMoi = LaySachMoi(6);
            return PartialView(listSachMoi);
        }
       
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogout");
        }
    }
}