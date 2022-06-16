using Estetika.Models.Entities;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace Estetika.Controllers
{
    public class TovarsController : Controller
    {
        private readonly SalonEntities db = new SalonEntities();

        // GET: Tovars
        public ActionResult Index()
        {
            var tovar = db.Tovar.Include(t => t.Polzovatel).Include(t => t.Tip_Tovar);
            return View(tovar.ToList());
        }

        // GET: Tovars/Details/5
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

        // GET: Tovars/Create
        public ActionResult Create()
        {
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya");
            ViewBag.ID_Tip_Tovar = new SelectList(db.Tip_Tovar, "ID_Tip_Tovar", "Imya_Tip_Tovar");
            return View();
        }

        // POST: Tovars/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Tovar,Imya_Tovar,Photo_Tovar,ID_Tip_Tovar,Prais,Opisanie_Tovar,ID_Polzovatel,Srok_hranen,Markirofka,Uslovia_hranenya")] Tovar tovar, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    tovar.Photo_Tovar = imageData;
                }
                db.Tovar.Add(tovar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", tovar.ID_Polzovatel);
            ViewBag.ID_Tip_Tovar = new SelectList(db.Tip_Tovar, "ID_Tip_Tovar", "Imya_Tip_Tovar", tovar.ID_Tip_Tovar);
            return View(tovar);
        }

        // GET: Tovars/Edit/5
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

        // POST: Tovars/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Tovar,Imya_Tovar,Photo_Tovar,ID_Tip_Tovar,Prais,Opisanie_Tovar,ID_Polzovatel,Srok_hranen,Markirofka,Uslovia_hranenya")] Tovar tovar, HttpPostedFileBase goodsImage)
        {
            if (ModelState.IsValid)
            {
                if (goodsImage != null)
                {
                    tovar.Photo_Tovar = goodsImage.ToByteArray();
                }
                db.Entry(tovar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Polzovatel = new SelectList(db.Polzovatel, "ID_Polzovatel", "Imya", tovar.ID_Polzovatel);
            ViewBag.ID_Tip_Tovar = new SelectList(db.Tip_Tovar, "ID_Tip_Tovar", "Imya_Tip_Tovar", tovar.ID_Tip_Tovar);
            return View(tovar);
        }

        // GET: Tovars/Delete/5
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

        // POST: Tovars/Delete/5
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
