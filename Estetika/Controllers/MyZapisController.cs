using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Estetika;

using Estetika.Models.Entities; namespace Estetika.Controllers
{
    public class MyZapisController : Controller
    {
        private readonly SalonEntities db = new SalonEntities();

        // GET: MyZapis
        public ActionResult Index()
        {
            var zapis = db.Zapis.Include(z => z.Master).Include(z => z.Polzovatel);
            return View(zapis.ToList());
        }

        // GET: MyZapis/Details/5
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

        // GET: MyZapis/Create
        public ActionResult Create()
        {
            ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master");
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya");
            return View();
        }

        // POST: MyZapis/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Zapis,Data,Vremya,ID_Polzovatel,ID_Master,Activien")] Zapis zapis)
        {
            if (ModelState.IsValid)
            {
                db.Zapis.Add(zapis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master", zapis.ID_Master);
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", zapis.ID_Polzovatel);
            return View(zapis);
        }

        // GET: MyZapis/Edit/5
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

        // POST: MyZapis/Edit/5
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

        // GET: MyZapis/Delete/5
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

        // POST: MyZapis/Delete/5
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
