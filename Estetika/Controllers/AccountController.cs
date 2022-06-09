using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Estetika;
using Estetika.Models;

namespace Estetika.Controllers
{
    public class AccountController : Controller
    {
        private const string EmailPattern = @"\w+@\w+\.\w{2,3}";

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Polzovatel user = SearchUserInDatabase(model);

                bool isUserFound = user != null;
                if (isUserFound)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(
                        nameof(model.Login), "Неверный логин или пароль");
                }
            }

            return View(model);
        }

        private static Polzovatel SearchUserInDatabase(LoginModel model)
        {
            Polzovatel user = null;
            using (SalonEntities db = new SalonEntities())
            {
                user = db.Polzovatel.FirstOrDefault(u => u.Login == model.Login);
                if (user == null)
                {
                    return null;
                }
               
                if (Enumerable.SequenceEqual(user.Parol,
                                             model.Password))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {

            if (model.Login == null)
            {
                ModelState.AddModelError(nameof(model.Login), "Введите логин");
            }
            else
            {
                using (SalonEntities db = new SalonEntities())
                {
                    if (db.Polzovatel.Any(u => u.Login == model.Login))
                    {
                        ModelState.AddModelError(nameof(model.Login), "Такой логин уже существует");
                    }
                }
            }

            if (model.Password == null || model.Password.Length < 5)
            {
                ModelState.AddModelError(nameof(model.Password), "Введите пароль от 5 символов");
            }

            if (ModelState.IsValid)
            {
                CreateNewUser(model);
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        private static void CreateNewUser(RegisterModel model)
        {
            
            using (SalonEntities db = new SalonEntities())
            {


                Polzovatel user = new Polzovatel
                {
                    Electronnya_Pochta = model.Email,
                    Parol = model.Password,
                    Login = model.Login,
                    Imya = model.Login,
                    ID_Tip_Polzovatel = UserTypes.User
                };

                db.Polzovatel.Add(user);
                db.SaveChanges();
            }
        }
    }
}
