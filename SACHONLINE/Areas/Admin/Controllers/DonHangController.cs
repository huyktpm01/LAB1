using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SACHONLINE.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace SACHONLINE.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP010502\\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
        //dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP-4PHTMN7E;Initial Catalog=SachOnline;Integrated Security=True");
        // GET: Admin/NhaXuatBAN
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.DONDATHANGs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DONDATHANG dondathang, FormCollection f)
        {
            dondathang.DaThanhToan = bool.Parse(f["bDaThanhToan"]);
            dondathang.TinhTrangGiaoHang = int.Parse(f["iTinhTrangGiaoHang"]);
            dondathang.NgayDat = DateTime.Parse(f["dNgayDat"]);
            dondathang.NgayGiao = DateTime.Parse(f["dNgayGiao"]);
            dondathang.MaKH = int.Parse(f["iMaKH"]);
            db.DONDATHANGs.InsertOnSubmit(dondathang);
            db.SubmitChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Details(int id)
        {
            var dondathang = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dondathang);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var dondathang = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dondathang);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var dondathang = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);

            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = db.CHITIETDATHANGs.Where(ct => ct.MaDonHang == id);        
            if (ctdh != null)
            {
                db.CHITIETDATHANGs.DeleteAllOnSubmit(ctdh);
                db.SubmitChanges();
            }
            db.DONDATHANGs.DeleteOnSubmit(dondathang);
            db.SubmitChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dondathang = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dondathang);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var dondathang  = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == int.Parse(f["iMaDonHang"]));

            if (ModelState.IsValid)
            {
                dondathang.DaThanhToan = bool.Parse(f["bDaThanhToan"]);
                dondathang.TinhTrangGiaoHang = int.Parse(f["iTinhTrangGiaoHang"]);
                dondathang.NgayDat = DateTime.Parse(f["dNgayDat"]);
                dondathang.NgayGiao = DateTime.Parse(f["dNgayGiao"]);
                dondathang.MaKH = int.Parse(f["iMaKH"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(dondathang);
        }
    }
}
