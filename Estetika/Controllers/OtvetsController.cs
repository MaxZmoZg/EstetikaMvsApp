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
    public class OtvetsController : Controller
    {
        private readonly SalonEntities db = new SalonEntities();

        // GET: Otvets
        public ActionResult Index()
        {
            var otvet = db.Otvet.Include(o => o.Polzovatel).Include(o => o.Variant);
            return View(otvet.ToList());
        }

        // GET: Otvets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Otvet otvet = db.Otvet.Find(id);
            if (otvet == null)
            {
                return HttpNotFound();
            }
            return View(otvet);
        }

        // GET: Otvets/Create
        public ActionResult Create()
        {
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya");
            ViewBag.ID_Variant = new SelectList(db.Variant, "ID_Variant", "Tekst");
            return View();
        }

        // POST: Otvets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Otvet,ID_Variant,ID_Polzovatel")] Otvet otvet)
        {
            if (ModelState.IsValid)
            {
                db.Otvet.Add(otvet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", otvet.ID_Polzovatel);
            ViewBag.ID_Variant = new SelectList(db.Variant, "ID_Variant", "Tekst", otvet.ID_Variant);
            return View(otvet);
        }

        // GET: Otvets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Otvet otvet = db.Otvet.Find(id);
            if (otvet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", otvet.ID_Polzovatel);
            ViewBag.ID_Variant = new SelectList(db.Variant, "ID_Variant", "Tekst", otvet.ID_Variant);
            return View(otvet);
        }

        // POST: Otvets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Otvet,ID_Variant,ID_Polzovatel")] Otvet otvet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otvet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", otvet.ID_Polzovatel);
            ViewBag.ID_Variant = new SelectList(db.Variant, "ID_Variant", "Tekst", otvet.ID_Variant);
            return View(otvet);
        }

        // GET: Otvets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Otvet otvet = db.Otvet.Find(id);
            if (otvet == null)
            {
                return HttpNotFound();
            }
            return View(otvet);
        }

        // POST: Otvets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Otvet otvet = db.Otvet.Find(id);
            db.Otvet.Remove(otvet);
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
