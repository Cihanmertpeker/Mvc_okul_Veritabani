using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc_okul_Veritabani.Models;

namespace Mvc_okul_Veritabani.Controllers
{
    public class NotlarsController : Controller
    {
        private okul2Entities db = new okul2Entities();

        // GET: Notlars
        public async Task<ActionResult> Index()
        {
            var notlar = db.Notlar.Include(n => n.Dersler).Include(n => n.Ogrenciler);
            return View(await notlar.ToListAsync());
        }


        // GET: Notlars/Create
        public ActionResult Create()
        {
            ViewBag.ders_no = new SelectList(db.Dersler, "ders_no", "ders_adi");
            ViewBag.og_no = new SelectList(db.Ogrenciler, "og_no", "og_ad_soyad");
            return View();
        }

        // POST: Notlars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "kayit_no,og_no,ders_no,yaz1,yaz2,soz")] Notlar notlar)
        {
            if (ModelState.IsValid)
            {
                db.Notlar.Add(notlar);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ders_no = new SelectList(db.Dersler, "ders_no", "ders_adi", notlar.ders_no);
            ViewBag.og_no = new SelectList(db.Ogrenciler, "og_no", "og_ad_soyad", notlar.og_no);
            return View(notlar);
        }

        // GET: Notlars/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notlar notlar = await db.Notlar.FindAsync(id);
            if (notlar == null)
            {
                return HttpNotFound();
            }
            ViewBag.ders_no = new SelectList(db.Dersler, "ders_no", "ders_adi", notlar.ders_no);
            ViewBag.og_no = new SelectList(db.Ogrenciler, "og_no", "og_ad_soyad", notlar.og_no);
            return View(notlar);
        }

        // POST: Notlars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "kayit_no,og_no,ders_no,yaz1,yaz2,soz")] Notlar notlar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notlar).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ders_no = new SelectList(db.Dersler, "ders_no", "ders_adi", notlar.ders_no);
            ViewBag.og_no = new SelectList(db.Ogrenciler, "og_no", "og_ad_soyad", notlar.og_no);
            return View(notlar);
        }

        // GET: Notlars/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notlar notlar = await db.Notlar.FindAsync(id);
            if (notlar == null)
            {
                return HttpNotFound();
            }
            return View(notlar);
        }

        // POST: Notlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Notlar notlar = await db.Notlar.FindAsync(id);
            db.Notlar.Remove(notlar);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
