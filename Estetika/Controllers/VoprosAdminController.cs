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
    public class VoprosAdminController : Controller
    {
        private SalonEntities db = new SalonEntities();

        // GET: VoprosAdmin
        public ActionResult Index()
        {
            return View(db.Vopros.ToList());
        }

        // GET: VoprosAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vopros vopros = db.Vopros.Find(id);
            if (vopros == null)
            {
                return HttpNotFound();
            }
            return View(vopros);
        }

        // GET: VoprosAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoprosAdmin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Vopros,Zagolovok")] Vopros vopros)
        {
            if (ModelState.IsValid)
            {
                db.Vopros.Add(vopros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vopros);
        }

        // GET: VoprosAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vopros vopros = db.Vopros.Find(id);
            if (vopros == null)
            {
                return HttpNotFound();
            }
            return View(vopros);
        }

        // POST: VoprosAdmin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Vopros,Zagolovok")] Vopros vopros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vopros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vopros);
        }

        // GET: VoprosAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vopros vopros = db.Vopros.Find(id);
            if (vopros == null)
            {
                return HttpNotFound();
            }
            return View(vopros);
        }

        // POST: VoprosAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vopros vopros = db.Vopros.Find(id);
            db.Vopros.Remove(vopros);
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
