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
    public class KhachHangController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP010502\\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
        //dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP-4PHTMN7E;Initial Catalog=SachOnline;Integrated Security=True");
        // GET: Admin/NhaXuatBAN
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG khachhang , FormCollection f)
        {
            khachhang.HoTen = f["sHoTen"];
            khachhang.TaiKhoan = f["sTaiKhoan"];
            khachhang.MatKhau = f["sMatKhau"];
            khachhang.Email = f["sEmail"];
            khachhang.DiaChi= f["sDiaChi"];
            khachhang.DienThoai = f["sDienThoai"];
            khachhang.NgaySinh = DateTime.Parse(f["sNgaySinh"]);
            db.KHACHHANGs.InsertOnSubmit(khachhang);
            db.SubmitChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Details(int id)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);

            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = db.DONDATHANGs.Where(ct => ct.MaKH == id);
            if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Khách hàng đã đặt đơn hàng này  <br>" + " Nếu muốn xóa thì phải xóa đơn hàng này trước";
                return View(khachhang);
            }
            var dh = db.DONDATHANGs.Where(s => s.MaKH == id);
            if (dh != null)
            {
                foreach(var i in dh)
                {
                   
                    var ddd = db.CHITIETDATHANGs.Where(s => s.MaDonHang == i.MaDonHang);
                    if (ddd != null)
                    {
                        db.CHITIETDATHANGs.DeleteAllOnSubmit(ddd);
                        db.SubmitChanges();
                    }
                    db.DONDATHANGs.DeleteOnSubmit(i);
                    db.SubmitChanges();
                }
            }
            db.DONDATHANGs.DeleteAllOnSubmit(dh);
            db.SubmitChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == int.Parse(f["iMaKH"]));

            if (ModelState.IsValid)
            {
                khachhang.HoTen = f["sHoTen"];
                khachhang.Email = f["sEmail"];
                khachhang.DiaChi = f["sDiaChi"];
                khachhang.DienThoai = f["sSoDienThoai"];
                khachhang.NgaySinh = DateTime.Parse(f["dNgaySinh"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(khachhang);
        }
    }
}