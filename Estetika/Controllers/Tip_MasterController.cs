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
    public class Tip_MasterController : Controller
    {
        private readonly SalonEntities db = new SalonEntities();

        // GET: Tip_Master
        public ActionResult Index()
        {
            return View(db.Tip_Master.ToList());
        }

        // GET: Tip_Master/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip_Master tip_Master = db.Tip_Master.Find(id);
            if (tip_Master == null)
            {
                return HttpNotFound();
            }
            return View(tip_Master);
        }

        // GET: Tip_Master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tip_Master/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Tip_Master,Imya_Tip_Master")] Tip_Master tip_Master)
        {
            if (ModelState.IsValid)
            {
                db.Tip_Master.Add(tip_Master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tip_Master);
        }

        // GET: Tip_Master/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip_Master tip_Master = db.Tip_Master.Find(id);
            if (tip_Master == null)
            {
                return HttpNotFound();
            }
            return View(tip_Master);
        }

        // POST: Tip_Master/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Tip_Master,Imya_Tip_Master")] Tip_Master tip_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tip_Master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tip_Master);
        }

        // GET: Tip_Master/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip_Master tip_Master = db.Tip_Master.Find(id);
            if (tip_Master == null)
            {
                return HttpNotFound();
            }
            return View(tip_Master);
        }

        // POST: Tip_Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tip_Master tip_Master = db.Tip_Master.Find(id);
            db.Tip_Master.Remove(tip_Master);
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
