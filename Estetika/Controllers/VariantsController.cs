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
    public class VariantsController : Controller
    {
        private readonly SalonEntities db = new SalonEntities();

        // GET: Variants
        public ActionResult Index()
        {
            var variant = db.Variant.Include(v => v.Vopros);
            return View(variant.ToList());
        }

        // GET: Variants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variant variant = db.Variant.Find(id);
            if (variant == null)
            {
                return HttpNotFound();
            }
            return View(variant);
        }

        // GET: Variants/Create
        public ActionResult Create()
        {
            ViewBag.ID_Vopros = new SelectList(db.Vopros, "ID_Vopros", "Zagolovok");
            return View();
        }

        // POST: Variants/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Variant,ID_Vopros,Tekst")] Variant variant)
        {
            if (ModelState.IsValid)
            {
                db.Variant.Add(variant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Vopros = new SelectList(db.Vopros, "ID_Vopros", "Zagolovok", variant.ID_Vopros);
            return View(variant);
        }

        // GET: Variants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variant variant = db.Variant.Find(id);
            if (variant == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Vopros = new SelectList(db.Vopros, "ID_Vopros", "Zagolovok", variant.ID_Vopros);
            return View(variant);
        }

        // POST: Variants/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Variant,ID_Vopros,Tekst")] Variant variant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(variant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Vopros = new SelectList(db.Vopros, "ID_Vopros", "Zagolovok", variant.ID_Vopros);
            return View(variant);
        }

        // GET: Variants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Variant variant = db.Variant.Find(id);
            if (variant == null)
            {
                return HttpNotFound();
            }
            return View(variant);
        }

        // POST: Variants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Variant variant = db.Variant.Find(id);
            db.Variant.Remove(variant);
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
