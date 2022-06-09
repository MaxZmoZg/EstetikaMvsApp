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
    public class OtzivsController : Controller
    {
        private SalonEntities db = new SalonEntities();

        // GET: Otzivs
        public ActionResult Index()
        {
            var otziv = db.Otziv.Include(o => o.Polzovatel);
            return View(otziv.ToList());
        }

        // GET: Otzivs/Details/5
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

        // GET: Otzivs/Create
        public ActionResult Create()
        {
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya");
            return View();
        }

        // POST: Otzivs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Otziv,Otziv1,ID_Polzovatel")] Otziv otziv)
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

        // GET: Otzivs/Edit/5
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

        // POST: Otzivs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Otziv,Otziv1,ID_Polzovatel")] Otziv otziv)
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

        // GET: Otzivs/Delete/5
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

        // POST: Otzivs/Delete/5
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

        [Authorize]
        public ActionResult PostComment(string comment)
        {
            if ( comment == null || comment == "")
            {
                ModelState.AddModelError(nameof(comment), "Введите комментарий");
            }
            if (ModelState.IsValid)
            {

                Otziv newComment = new Otziv
            {
              Napisanny_Otziv = comment,
               ID_Polzovatel = db.Polzovatel.First(u => u.Login == HttpContext.User.Identity.Name).ID_Polzovatel
              
           };
           db.Otziv.Add(newComment);
           db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
