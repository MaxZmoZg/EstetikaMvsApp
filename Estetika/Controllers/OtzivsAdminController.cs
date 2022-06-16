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
    public class OtzivsAdminController : Controller
    {
        private SalonEntities db = new SalonEntities();

        // GET: OtzivsAdmin
        public ActionResult Index()
        {
            var otziv = db.Otziv.Include(o => o.Polzovatel);
            return View(otziv.ToList());
        }

        // GET: OtzivsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Otziv otziv = db.Otziv.Find(id);
            if (otziv == null)
            {
                return HttpNotFound();
            }
            return View(otziv);
        }

        // GET: OtzivsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya");
            return View();
        }

        // POST: OtzivsAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Otziv,Napisanny_Otziv,ID_Polzovatel")] Otziv otziv)
        {
            if (ModelState.IsValid)
            {
                db.Otziv.Add(otziv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", otziv.ID_Polzovatel);
            return View(otziv);
        }

        // GET: OtzivsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Otziv otziv = db.Otziv.Find(id);
            if (otziv == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", otziv.ID_Polzovatel);
            return View(otziv);
        }

        // POST: OtzivsAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Otziv,Napisanny_Otziv,ID_Polzovatel")] Otziv otziv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otziv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", otziv.ID_Polzovatel);
            return View(otziv);
        }

        // GET: OtzivsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Otziv otziv = db.Otziv.Find(id);
            if (otziv == null)
            {
                return HttpNotFound();
            }
            return View(otziv);
        }

        // POST: OtzivsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Otziv otziv = db.Otziv.Find(id);
            db.Otziv.Remove(otziv);
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
