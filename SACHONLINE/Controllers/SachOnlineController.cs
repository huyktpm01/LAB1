﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SACHONLINE.Models;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        //Tao 1 doi tuong chua toan bo CSDL tu bdSachOnline

        dbSachOnlineDataContext data = new dbSachOnlineDataContext("Data Source=LAPTOP-4PHTMN7E;Initial Catalog=SachOnline;Integrated Security=True");
        /// <summary>
        /// LaySachMoi
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>List</returns>
        private List<SACH> LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        // GET: SachOnline
        public ActionResult Index()
        {
            //Lay 6 quyen sach moi
            var listSachMoi = LaySachMoi(6);
            return View(listSachMoi);
        }
        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
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
    }
}