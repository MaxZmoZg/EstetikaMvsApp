using Estetika.Models;
using Estetika.Models.Entities;
using Estetika.Models.Exceptions;
using System;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace Estetika.Controllers
{
    public class VoprosController : Controller
    {
        private readonly SalonEntities db = new SalonEntities();

        // GET: Vopros
        public ActionResult Index()
        {
            return View(db.Vopros.ToList());
        }


        [HttpPost]
        public ActionResult RedirectUserDependingOnChoices()
        {
            NameValueCollection choicesForm = Request.Form;

            TrySaveChoices(choicesForm);

            string hairProblemsChoice = choicesForm["Какие проблемы у вас с волосами?"];

            if (hairProblemsChoice == null)
            {
                return RedirectToAction("Index", "Sredstva");
            }
            else
            {
                return RedirectDependingOnChoice(hairProblemsChoice);
            }
        }

        /// <summary>
        /// Tries to save the selected choices to the database or throws Exception. 
        /// The saved choices are not used on the website.
        /// </summary>
        /// <param name="choicesCollection">Collection with the selected choices.</param>
        private void TrySaveChoices(NameValueCollection choicesCollection)
        {
            try
            {
                SaveChoices(choicesCollection);
            }
            catch (DbException ex)
            {
                throw new Exception("Cannot save the choices to the db", ex);
            }
        }

        private void SaveChoices(NameValueCollection choicesCollection)
        {
            foreach (string answerKey in choicesCollection.Keys)
            {
                string choiceText = choicesCollection[answerKey];
                Variant choice = db.Variant.First(v => v.Tekst == choiceText);
                Otvet answer = new Otvet
                {
                    ID_Polzovatel = Me.Get().ID_Polzovatel,
                    ID_Variant = choice.ID_Variant
                };
                db.Otvet.Add(answer);
                db.SaveChanges();
            }
        }

        private ActionResult RedirectDependingOnChoice(string hairProblemsChoice)
        {
            if (hairProblemsChoice == "ломаются, секутся")
            {
                TempData[nameof(Tovar.ID_Tip_Tovar)] = GoodsTypeIds.Shampoo;
                return RedirectToAction("Index", "Sredstva");
            }
            else if (hairProblemsChoice == "пушатся")
            {
                TempData[nameof(Tovar.ID_Tip_Tovar)] = GoodsTypeIds.Elastic;
                return RedirectToAction("Index", "Sredstva");
            }
            else if (hairProblemsChoice == "невозможно расчесать")
            {
                TempData[nameof(Tovar.ID_Tip_Tovar)] = GoodsTypeIds.Comb;
                return RedirectToAction("Index", "Sredstva");
            }
            else
            {
                throw new ChoiceActionNotFoundException("The hairs problem needs more choices");
            }
        }

        // GET: Vopros/Details/5
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

        // GET: Vopros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vopros/Create
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

        // GET: Vopros/Edit/5
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

        // POST: Vopros/Edit/5
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

        // GET: Vopros/Delete/5
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

        // POST: Vopros/Delete/5
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
