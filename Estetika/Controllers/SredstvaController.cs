using Estetika.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace Estetika.Controllers
{
    public class SredstvaController : Controller
    {
        private readonly SalonEntities db = new SalonEntities();

        // GET: Sredstva
        public ActionResult Index()
        {
            return GetFilteredItemsActionResult();
        }

        private void LoadDropDownLists()
        {
            List<Tip_Tovar> tip_Tovars = db.Tip_Tovar.ToList();
            tip_Tovars.Insert(0, new Tip_Tovar { Imya_Tip_Tovar = "-----" });
            ViewBag.ID_Tip_Tovar = new SelectList(tip_Tovars, "ID_Tip_Tovar", "Imya_Tip_Tovar");
        }

        private void LoadDropDownListsWithPredefinedId(int goodsId)
        {
            List<Tip_Tovar> tip_Tovars = db.Tip_Tovar.ToList();
            tip_Tovars.Insert(0, new Tip_Tovar { Imya_Tip_Tovar = "-----" });
            ViewBag.ID_Tip_Tovar = new SelectList(tip_Tovars, "ID_Tip_Tovar", "Imya_Tip_Tovar", goodsId);
        }

        [HttpPost]
        public ActionResult GetFilteredItemsActionResult()
        {
            List<Tovar> filteredTovar = db.Tovar
                .Include(m => m.Tip_Tovar)
                .ToList();
            if (!string.IsNullOrWhiteSpace(Request["Name"]))
            {
                filteredTovar = filteredTovar.Where(m =>
                {
                    return m.Imya_Tovar.IndexOf(Request["Name"],
                                                StringComparison.OrdinalIgnoreCase) != -1;
                })
                    .ToList();
            }

            if (Request.Form[nameof(Tovar.ID_Tip_Tovar)] is string tipTovaraIdAsString && tipTovaraIdAsString != "0")
            {
                filteredTovar = filteredTovar.Where(m => m.ID_Tip_Tovar == int.Parse(tipTovaraIdAsString))
                    .ToList();
            }

            if (TempData[nameof(Tovar.ID_Tip_Tovar)] is int tipTovaraIdAsInteger && tipTovaraIdAsInteger > 0)
            {
                filteredTovar = filteredTovar.Where(m => m.ID_Tip_Tovar == tipTovaraIdAsInteger)
                    .ToList();
            }

            if (Request.Form.Keys.OfType<string>().Contains("Алфавиту"))
            {
                filteredTovar = filteredTovar
                    .OrderByDescending(m => m.Imya_Tovar)
                    .ToList();
            }

            LoadDropDownLists();

            return View("Index", filteredTovar);
        }




        // GET: Sredstva/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tovar tovar = db.Tovar.Find(id);
            if (tovar == null)
            {
                return HttpNotFound();
            }
            return View(tovar);
        }

        // GET: Sredstva/Create
        public ActionResult Create()
        {
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya");
            ViewBag.ID_Tip_Tovar = new SelectList(db.Tip_Tovar, "ID_Tip_Tovar", "Imya_Tip_Tovar");
            return View();
        }

        // POST: Sredstva/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Tovar,Imya_Tovar,Photo_Tovar,ID_Tip_Tovar,Prais,Opisanie_Tovar,ID_Polzovatel,Srok_hranen,Markirofka,Uslovia_hranenya")] Tovar tovar)
        {
            if (ModelState.IsValid)
            {
                db.Tovar.Add(tovar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", tovar.ID_Polzovatel);
            ViewBag.ID_Tip_Tovar = new SelectList(db.Tip_Tovar, "ID_Tip_Tovar", "Imya_Tip_Tovar", tovar.ID_Tip_Tovar);
            return View(tovar);
        }

        // GET: Sredstva/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tovar tovar = db.Tovar.Find(id);
            if (tovar == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", tovar.ID_Polzovatel);
            ViewBag.ID_Tip_Tovar = new SelectList(db.Tip_Tovar, "ID_Tip_Tovar", "Imya_Tip_Tovar", tovar.ID_Tip_Tovar);
            return View(tovar);
        }

        // POST: Sredstva/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Tovar,Imya_Tovar,Photo_Tovar,ID_Tip_Tovar,Prais,Opisanie_Tovar,ID_Polzovatel,Srok_hranen,Markirofka,Uslovia_hranenya")] Tovar tovar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tovar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", tovar.ID_Polzovatel);
            ViewBag.ID_Tip_Tovar = new SelectList(db.Tip_Tovar, "ID_Tip_Tovar", "Imya_Tip_Tovar", tovar.ID_Tip_Tovar);
            return View(tovar);
        }

        // GET: Sredstva/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tovar tovar = db.Tovar.Find(id);
            if (tovar == null)
            {
                return HttpNotFound();
            }
            return View(tovar);
        }

        // POST: Sredstva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tovar tovar = db.Tovar.Find(id);
            db.Tovar.Remove(tovar);
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
