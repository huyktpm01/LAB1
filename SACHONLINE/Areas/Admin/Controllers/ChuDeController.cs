﻿using System;
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
    public class ChuDeController : Controller
    {
       // dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP010502\\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
        dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP-4PHTMN7E;Initial Catalog=SachOnline;Integrated Security=True");
        // GET: Admin/Sach
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.CHUDEs.ToList().OrderBy(n => n.MaCD).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CHUDE chude, FormCollection f)
        {
                    chude.TenChuDe = f["sTenChuDe"];
                    db.CHUDEs.InsertOnSubmit(chude);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
            
        }
        public ActionResult Details(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);

            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = db.CHUDEs.Where(ct => ct.MaCD == id);
            if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "chủ đề này đang có sách  <br>" + " Nếu muốn xóa thì phải xóa hết mã sách này trong bảng sách";
                return View(chude);
            }
            var sach = db.SACHes.Where(s => s.MaCD == id);
            if (sach != null)
            {
                db.SACHes.DeleteAllOnSubmit(sach);
                db.SubmitChanges();
            }
            db.CHUDEs.DeleteOnSubmit(chude);
            db.SubmitChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == int.Parse(f["iMaCD"]));
        
            if(ModelState.IsValid)
            {
                chude.TenChuDe = f["sTenChuDe"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(chude);
        }
    }
}