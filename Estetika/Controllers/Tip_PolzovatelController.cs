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
    public class Tip_PolzovatelController : Controller
    {
        private SalonEntities db = new SalonEntities();

        // GET: Tip_Polzovatel
        public ActionResult Index()
        {
            return View(db.Tip_Polzovatel.ToList());
        }

        // GET: Tip_Polzovatel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip_Polzovatel tip_Polzovatel = db.Tip_Polzovatel.Find(id);
            if (tip_Polzovatel == null)
            {
                return HttpNotFound();
            }
            return View(tip_Polzovatel);
        }

        // GET: Tip_Polzovatel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tip_Polzovatel/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Tip_Polzovatel,Imya_Tip_Polzovatel")] Tip_Polzovatel tip_Polzovatel)
        {
            if (ModelState.IsValid)
            {
                db.Tip_Polzovatel.Add(tip_Polzovatel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tip_Polzovatel);
        }

        // GET: Tip_Polzovatel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip_Polzovatel tip_Polzovatel = db.Tip_Polzovatel.Find(id);
            if (tip_Polzovatel == null)
            {
                return HttpNotFound();
            }
            return View(tip_Polzovatel);
        }

        // POST: Tip_Polzovatel/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Tip_Polzovatel,Imya_Tip_Polzovatel")] Tip_Polzovatel tip_Polzovatel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tip_Polzovatel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tip_Polzovatel);
        }

        // GET: Tip_Polzovatel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip_Polzovatel tip_Polzovatel = db.Tip_Polzovatel.Find(id);
            if (tip_Polzovatel == null)
            {
                return HttpNotFound();
            }
            return View(tip_Polzovatel);
        }

        // POST: Tip_Polzovatel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tip_Polzovatel tip_Polzovatel = db.Tip_Polzovatel.Find(id);
            db.Tip_Polzovatel.Remove(tip_Polzovatel);
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
