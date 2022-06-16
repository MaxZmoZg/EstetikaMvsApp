using Estetika.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace Estetika.Controllers
{
    public class ZapisController : Controller
    {
        private readonly SalonEntities db = new SalonEntities();

        // GET: Zapis
        public ActionResult Index()
        {
            var zapis = db.Zapis.Include(z => z.Master).Include(z => z.Polzovatel);
            return View(zapis.ToList());
        }

        // GET: Zapis/Details/5
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

        // GET: Zapis/Create
        public ActionResult Create(int id = 0)
        {
            if (id > 0)
            {
                ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master", db.Master.First(m => m.ID_Master == id).ID_Master);
            }
            else
            {
                ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master", db.Master.First().ID_Master);
            }

            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya");
            return View();
        }

        // POST: Zapis/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID_Zapis,Data,Vremya,ID_Polzovatel,ID_Master,Activien")] Zapis zapis)
        {
            if (ModelState.IsValid)

            {
                Zapis zapisfrobd = new Zapis
                {
                    Data = zapis.Data,
                    Vremya = zapis.Vremya,
                    ID_Polzovatel = db.Polzovatel.First(u => u.Login == HttpContext.User.Identity.Name).ID_Polzovatel,
                    ID_Master = zapis.ID_Master,
                    Activien = true
                };

                db.Zapis.Add(zapisfrobd);
                db.SaveChanges();
                return RedirectToAction("Details", "Polzovatels");
            }

            ViewBag.ID_Master = new SelectList(db.Master, "ID_Master", "Imya_Master", zapis.ID_Master);
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", zapis.ID_Polzovatel);
            return View(zapis);
        }

        // GET: Zapis/Edit/5
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

        // POST: Zapis/Edit/5
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

        // GET: Zapis/Delete/5
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

        // POST: Zapis/Delete/5
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

        public ActionResult CancelRequest(int requestId)
        {
            using (SalonEntities entities = new SalonEntities())
            {
                Zapis request = entities.Zapis.Find(requestId);
                request.Activien = false;
                entities.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
