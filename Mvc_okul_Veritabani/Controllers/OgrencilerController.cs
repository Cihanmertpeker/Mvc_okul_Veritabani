using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Mvc_okul_Veritabani.Models;
namespace Mvc_okul_Veritabani.Controllers
{
    public class OgrencilerController : Controller
    {
        okul2Entities db = new okul2Entities();
        public async Task<ActionResult> tum_ogrenciler()
        {
            var ogrenci_listem = await db.Ogrenciler.ToListAsync();
            return View(ogrenci_listem);
        }

        public ActionResult ogrenci_kaydet()
        {
            ViewBag.sinif_adi = new SelectList(db.siniflar, "sinif_adi", "sinif_adi");
            return View();
        }
        [HttpPost]
        public ActionResult ogrenci_kaydet(Ogrenciler yeni_ogr)
        {
            ViewBag.sinif_adi = new SelectList(db.siniflar, "sinif_adi", "sinif_adi");
            try
            {
                if (ModelState.IsValid == true)
                {
                    db.Ogrenciler.Add(yeni_ogr);
                    db.SaveChanges();
                    ViewBag.sonuc = "Kayıt Başarılı ile yapıldı";
                }
            }
            catch(Exception)
            {
                ViewBag.sonuc = "AYnı Tc veya email bilgisinde öğrenci var kontrol edip değiştirin";
            }
            return View();
        }

        public ActionResult ogrenci_sil(int id)
        {
            var silinecek_ogrenci = db.Ogrenciler.Find(id);
            db.Ogrenciler.Remove(silinecek_ogrenci);
            db.SaveChanges();
            return RedirectToAction("tum_ogrenciler");
        }

        public ActionResult ogrenci_guncelle(int id)
        {
           
            var ogrencimiz = db.Ogrenciler.Find(id);
            ViewBag.sinif_adi = new SelectList(db.siniflar, "sinif_adi", "sinif_adi",ogrencimiz.sinif_adi);
            return View(ogrencimiz);
        }
       [HttpPost]
        public ActionResult ogrenci_guncelle(Ogrenciler yeni_ogr)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    db.Entry(yeni_ogr).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.sonuc = "Kayıt Başarılı ile Değiştirildi";
                }
            }
            catch (Exception)
            {
                ViewBag.sonuc = "AYnı Tc veya email bilgisinde öğrenci var kontrol edip değiştirin";
            }
            ViewBag.sinif_adi = new SelectList(db.siniflar, "sinif_adi", "sinif_adi", yeni_ogr.sinif_adi);

            return View(yeni_ogr);
        }

        public ActionResult sinif_bazinda_rapor()
        {
            ViewBag.sinif_adi = new SelectList(db.siniflar, "sinif_adi", "sinif_adi");

           var ogrenciler = db.Ogrenciler.ToList();
            return View(ogrenciler);
        }

        [HttpPost]
        public ActionResult sinif_bazinda_rapor(string sinif_adi)
        {
            ViewBag.sinif_adi = new SelectList(db.siniflar, "sinif_adi", "sinif_adi");
            List<Ogrenciler> ogrenciler;
            if (sinif_adi=="")
            {
                ogrenciler = db.Ogrenciler.ToList();
            }
            else
            { 
                ogrenciler = db.Ogrenciler.Where(x => x.sinif_adi == sinif_adi).ToList();
            }
          

            return View(ogrenciler);
          
          
        }

    }
    }
