using baitap_mvc_3.App_Start;
using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap_mvc_3.Areas.Admin.Controllers
{
    public class NhanvienController : Controller
    {
        // GET: Admin/Nhanvien
        [AdminAuthenrize(idChucnang = 1)]
        public ActionResult Danhsach()
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            List<Nhanvien> nhanvienList= db.Nhanviens.ToList();

            return View(nhanvienList);
        }
        [AdminAuthenrize(idChucnang = 2)]
        public ActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Them(Nhanvien model)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            db.Nhanviens.Add(model);
            db.SaveChanges();
            return RedirectToAction("Danhsach", "Nhanvien");
        }
        [AdminAuthenrize(idChucnang = 3)]
        public ActionResult Capnhat(int id)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            return View(nhanvien);
        }
        [HttpPost]
        public JsonResult PhanQuyenJSON(int idNhanvien, int idChucnang)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var phanQuyen = db.Phanquyens.SingleOrDefault(m => m.idNhanvien == idNhanvien & m.idChucnang == idChucnang);
            if(phanQuyen != null)
            {
                db.Phanquyens.Remove(phanQuyen);
                db.SaveChanges();
            }
            else
            {
                phanQuyen = new Phanquyen();
                phanQuyen.idChucnang = idChucnang;
                phanQuyen.idNhanvien = idNhanvien;
                db.Phanquyens.Add(phanQuyen);
                db.SaveChanges();
            }

            return Json(new {status = "đã phân quyền"});
        }
        [AdminAuthenrize(idChucnang = 4)]
        public ActionResult Xoa(int id)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Nhanvien nvDelete = db.Nhanviens.Find(id);
            var nvPermission = db.Phanquyens.Where(m => m.idNhanvien == nvDelete.ID).ToList();
            foreach(var item in nvPermission)
            {
                db.Phanquyens.Remove(item);
            }
            db.Nhanviens.Remove(nvDelete);
            db.SaveChanges();
            return RedirectToAction("Danhsach", "Nhanvien");
        }
    }
}