using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Estetika.Models.Entities;

namespace Estetika.Controllers
{
    public class ZapisAdminController : Controller
    {
        private SalonEntities db = new SalonEntities();

        // GET: ZapisAdmin
        public ActionResult Index()
        {
            var zapis = db.Zapis.Include(z => z.Master).Include(z => z.Polzovatel);
            return View(zapis.ToList());
        }

        // GET: ZapisAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zapis zapis = db.Zapis.Find(id);
            if (zapis == null)
            {
                return HttpNotFound();
            }
            return View(zapis);
        }

        // GET: ZapisAdmin/Create
        public ActionResult Create(int id = 0)
        {

            if (id > 0)
            {
                ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master", db.Master.First(m => m.ID_Master == id).ID_Master);
            }
            else
            {
                ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master", db.Master.First().ID_Master);
            }

            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya");
            return View();
        }

        // POST: ZapisAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID_Zapis,Data,Vremya,ID_Polzovatel,ID_Master,Activien")] Zapis zapis)
        {
            if (ModelState.IsValid)
            {
                Zapis newRequest = new Zapis
                {
                    Data = zapis.Data.Value,
                    Vremya = zapis.Vremya.Value,
                    ID_Polzovatel = db.Polzovatel.First(u => u.Login == HttpContext.User.Identity.Name).ID_Polzovatel,
                    ID_Master = zapis.ID_Master,
                    Activien = true
                };
                db.Zapis.Add(newRequest);
                db.SaveChanges();
                return RedirectToAction("Index", "Zapis");
            }

            ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master", zapis.ID_Master);
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", zapis.ID_Polzovatel);
            return View(zapis);
        }

        // GET: ZapisAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zapis zapis = db.Zapis.Find(id);
            if (zapis == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master", zapis.ID_Master);
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", zapis.ID_Polzovatel);
            return View(zapis);
        }

        // POST: ZapisAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Zapis,Data,Vremya,ID_Polzovatel,ID_Master,Activien")] Zapis zapis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zapis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master", zapis.ID_Master);
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", zapis.ID_Polzovatel);
            return View(zapis);
        }

        // GET: ZapisAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zapis zapis = db.Zapis.Find(id);
            if (zapis == null)
            {
                return HttpNotFound();
            }
            return View(zapis);
        }

        // POST: ZapisAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zapis zapis = db.Zapis.Find(id);
            db.Zapis.Remove(zapis);
            db.SaveChanges();
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
