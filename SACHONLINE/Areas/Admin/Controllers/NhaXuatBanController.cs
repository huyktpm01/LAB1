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
    public class NhaXuatBanController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP010502\\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
        //dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP-4PHTMN7E;Initial Catalog=SachOnline;Integrated Security=True");
        // GET: Admin/NhaXuatBAN
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.NHAXUATBANs.ToList().OrderBy(n => n.MaNXB).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NHAXUATBAN nhaxuatban, FormCollection f)
        {
            nhaxuatban.TenNXB = f["sNhaXuatBan"];
            nhaxuatban.DiaChi = f["sDiaChi"];
            nhaxuatban.DienThoai = f["sDienThoai"];
            db.NHAXUATBANs.InsertOnSubmit(nhaxuatban);
            db.SubmitChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Details(int id)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhaxuatban);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhaxuatban);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);

            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = db.NHAXUATBANs.Where(ct => ct.MaNXB == id);
            if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Nhà xuất bản đã xuất bản sách  <br>" + " Nếu muốn xóa thì phải xóa hết mã sách của nhà xuất bản này";
                return View(nhaxuatban);
            }
            var sach = db.SACHes.Where(s => s.MaNXB == id);
            if (sach != null)
            {
                db.SACHes.DeleteAllOnSubmit(sach);
                db.SubmitChanges();
            }
            db.NHAXUATBANs.DeleteOnSubmit(nhaxuatban);
            db.SubmitChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nhaxuatban == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhaxuatban);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == int.Parse(f["iMaNXB"]));

            if (ModelState.IsValid)
            {
                nhaxuatban.TenNXB = f["sTenNhaXuatBan"];
                nhaxuatban.DiaChi = f["sDiaChi"];
                nhaxuatban.DienThoai = f["sSoDienThoai"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(nhaxuatban);
        }
    }
}