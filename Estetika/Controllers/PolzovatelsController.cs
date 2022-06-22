using Estetika.Models.Entities;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace Estetika.Controllers
{
    public class PolzovatelsController : Controller
    {
        private readonly SalonEntities db = new SalonEntities();

        // GET: Polzovatels
        public ActionResult Index()
        {
            var polzovatel = db.Polzovatel
                .Include(p => p.Tip_Polzovatel)
                .Where(u => !u.IsDeleted);
            return View(polzovatel.ToList());
        }


        // GET: Polzovatels/Details/5
        [Authorize]
        public ActionResult Details(string login)
        {
            if (login == null)
            {
                return View(
                    db.Polzovatel.First(u => u.Login == HttpContext.User.Identity.Name));
            }
            return View(
                db.Polzovatel.First(u => u.Login == login));
        }

        // GET: Polzovatels/Create
        public ActionResult Create()
        {
            ViewBag.ID_Tip_Polzovatel = new SelectList(db.Tip_Polzovatel, "ID_Tip_Polzovatel", "Imya_Tip_Polzovatel");
            return View();
        }

        // POST: Polzovatels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Polzovatel,Imya,Phamilia,Electronnya_Pochta,Telephon,Login,Parol,ID_Tip_Polzovatel")] Polzovatel polzovatel)
        {
            if (ModelState.IsValid)
            {
                db.Polzovatel.Add(polzovatel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Tip_Polzovatel = new SelectList(db.Tip_Polzovatel, "ID_Tip_Polzovatel", "Imya_Tip_Polzovatel", polzovatel.ID_Tip_Polzovatel);
            return View(polzovatel);
        }

        // GET: Polzovatels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Polzovatel polzovatel = db.Polzovatel.Find(id);
            if (polzovatel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Tip_Polzovatel = new SelectList(db.Tip_Polzovatel, "ID_Tip_Polzovatel", "Imya_Tip_Polzovatel", polzovatel.ID_Tip_Polzovatel);
            return View(polzovatel);
        }

        // POST: Polzovatels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Polzovatel,Imya,Phamilia,Electronnya_Pochta,Telephon,Login,Parol,ID_Tip_Polzovatel")] Polzovatel polzovatel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(polzovatel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Tip_Polzovatel = new SelectList(db.Tip_Polzovatel, "ID_Tip_Polzovatel", "Imya_Tip_Polzovatel", polzovatel.ID_Tip_Polzovatel);
            return View(polzovatel);
        }

        // GET: Polzovatels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Polzovatel polzovatel = db.Polzovatel.Find(id);
            if (polzovatel == null)
            {
                return HttpNotFound();
            }
            return View(polzovatel);
        }

        // POST: Polzovatels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Polzovatel user = db.Polzovatel.Find(id);
            user.IsDeleted = true;
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
