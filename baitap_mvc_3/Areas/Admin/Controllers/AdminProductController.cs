﻿using baitap_mvc_3.App_Start;
using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap_mvc_3.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        //Menu
        [AdminAuthenrize(idChucnang = 1)]
        public ActionResult ProductMenu()
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            List<Product> productList = db.Products.ToList();
            return View(productList);
        }
        //Upload Image
        void UploadImage(Product model, List<HttpPostedFileBase> fileImages)
        {
            var path = "";
            foreach (var item in fileImages)
            {
                if (item != null)
                {
                    if (Path.GetExtension(item.FileName).ToLower() == ".jpg" || Path.GetExtension(item.FileName).ToLower() == ".png")
                    {
                        //save image in Data folder
                        path = Path.Combine(Server.MapPath("/Data/")) + item.FileName;
                        item.SaveAs(path);
                        //save image from Data in model
                        Image img = new Image();
                        img.Url = "/Data/" + item.FileName;
                        img.ProductID = model.ID;
                        model.Images.Add(img);
                    }
                }
            }
        }
        //Create
        [AdminAuthenrize(idChucnang = 2)]
        public ActionResult ProductCreate()
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            ViewBag.sizeList = db.Sizes.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ProductCreate(Product model, int[] selectedSize, List<HttpPostedFileBase> fileImages)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            //add attributes
            model.Quantity = 1;
            if(selectedSize != null)
            {
                model.Sizes = db.Sizes.Where(m => selectedSize.Contains(m.ID)).ToList();
            }
            db.Products.Add(model);
            //add image
            UploadImage(model, fileImages);

            db.SaveChanges();
            return RedirectToAction("ProductMenu");
        }
        //Edit
        [AdminAuthenrize(idChucnang = 3)]
        public ActionResult ProductEdit(int? id)
        {
            if(id == null)
            {
                return View("ProductMenu");
            }
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Product model = db.Products.Find(id);
            ViewBag.sizeList = db.Sizes.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult ProductEdit(Product model, int[] selectedSize, List<HttpPostedFileBase> fileImages)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var productUpdate = db.Products.Find(model.ID);

            //gán productUpdate = giá trị từ Form
            productUpdate.Name = model.Name.Trim();
            productUpdate.Price = model.Price;
            productUpdate.Color = model.Color.Trim();
            productUpdate.Decription = model.Decription.Trim();
            productUpdate.BrandID = model.BrandID;
            productUpdate.CategoryID = model.CategoryID;
            //xóa các quan hệ N-N của model cũ với bảng Size
            productUpdate.Sizes.Clear();
            if (selectedSize != null)
            {
                productUpdate.Sizes = db.Sizes.Where(m => selectedSize.Contains(m.ID)).ToList();
            }
            //img
            UploadImage(productUpdate, fileImages);

            db.SaveChanges();
            return RedirectToAction("ProductMenu");
        }
        public JsonResult ImageDelete(int imageID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities(); 
            var image = db.Images.Find(imageID);
            db.Images.Remove(image);
            db.SaveChanges();
            return Json(new {status = "success"});
        }
        public JsonResult ImageAdd(int imageID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var image = db.Images.Find(imageID);
            db.Images.Remove(image);
            db.SaveChanges();
            return Json(new { status = "success" });
        }
        //Delete
        [AdminAuthenrize(idChucnang = 4)]
        public ActionResult ProductDelete(int id)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Product productDel = db.Products.Find(id);
            db.Products.Remove(productDel);
            db.SaveChanges();
            return RedirectToAction("ProductMenu");
        }
    }
}