using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Estetika;

namespace Estetika.Controllers
{
    public class Tip_TovarController : Controller
    {
        private SalonEntities db = new SalonEntities();

        // GET: Tip_Tovar
        public ActionResult Index()
        {
            return View(db.Tip_Tovar.ToList());
        }

        // GET: Tip_Tovar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip_Tovar tip_Tovar = db.Tip_Tovar.Find(id);
            if (tip_Tovar == null)
            {
                return HttpNotFound();
            }
            return View(tip_Tovar);
        }

        // GET: Tip_Tovar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tip_Tovar/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Tip_Tovar,Imya_Tip_Tovar")] Tip_Tovar tip_Tovar)
        {
            if (ModelState.IsValid)
            {
                db.Tip_Tovar.Add(tip_Tovar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tip_Tovar);
        }

        // GET: Tip_Tovar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip_Tovar tip_Tovar = db.Tip_Tovar.Find(id);
            if (tip_Tovar == null)
            {
                return HttpNotFound();
            }
            return View(tip_Tovar);
        }

        // POST: Tip_Tovar/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Tip_Tovar,Imya_Tip_Tovar")] Tip_Tovar tip_Tovar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tip_Tovar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tip_Tovar);
        }

        // GET: Tip_Tovar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip_Tovar tip_Tovar = db.Tip_Tovar.Find(id);
            if (tip_Tovar == null)
            {
                return HttpNotFound();
            }
            return View(tip_Tovar);
        }

        // POST: Tip_Tovar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tip_Tovar tip_Tovar = db.Tip_Tovar.Find(id);
            db.Tip_Tovar.Remove(tip_Tovar);
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
