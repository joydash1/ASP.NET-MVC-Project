using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistration.Models;
using System.Web.Security;

namespace UserRegistration.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
                return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {

            using (var context = new OfficeStuffEntities())
            {
               bool isValid = context.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if (isValid)
                {  
                    FormsAuthentication.SetAuthCookie(model.UserName,false);
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid Username & Password");
                return View();
            }
           
        }

        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User model)
        {
            using (var context =new OfficeStuffEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();

            }
            return RedirectToAction("login");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}