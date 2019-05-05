using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loners.DBModel;

namespace Loners.Controllers
{
    public class AccountController : Controller
    {
        LonersEntities lonersEntities = new LonersEntities();

        public ActionResult Registration()
        {
            Loners.Models.UserModel userModel = new Models.UserModel();
            return View(userModel);
        }



        [HttpPost]
        public ActionResult Registration(Loners.Models.UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                DateTime dateOfBirth = new DateTime(userModel.DateYear, userModel.DateMonth, userModel.DateDay);

                DBModel.User user = new DBModel.User();
                user.UserName = userModel.UserName;
                user.Password = Cryptography.Hash(userModel.Password);
                user.Age = (int)((DateTime.Today - dateOfBirth).Days / 365.25);
                user.Gender = userModel.Gender;
                user.Country = userModel.Country;
                user.DateOfRegistration = DateTime.Now.ToString("dd/MM/yyyy");
                user.Status = "Logged Out";

                var loginUser = lonersEntities.Users.Where(m => m.UserName == user.UserName).FirstOrDefault();

                if (loginUser != null)
                {
                    ViewBag.ErrorLogin = "User name already exist.";

                    return RedirectToAction("Registration", "Account");
                }
                else
                {
                    lonersEntities.Users.Add(user);
                    lonersEntities.SaveChanges();
                }
            }
            return RedirectToAction("Login", "Account");
        }


        public ActionResult Login()
        {
            Loners.Models.LoginModel loginModel = new Models.LoginModel();
            return View(loginModel);
        }


        [HttpPost]
        public ActionResult Login(Loners.Models.LoginModel loginModel)
        {
            var loginUser = lonersEntities.Users.Where(m => m.UserName == loginModel.UserName).FirstOrDefault();

            if (loginUser != null)
            {
                if (loginUser.UserName != loginModel.UserName)
                {
                    ViewBag.ErrorLogin = "Incorrect login.";
                }
                else
                {
                    if (string.Compare(Cryptography.Hash(loginModel.Password), loginUser.Password) == 0)
                    {
                        Session["UserName"] = loginModel.UserName;
                        loginUser.Status = "Logged In";
                        lonersEntities.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorLogin = "Incorrect password.";

                        return RedirectToAction("Login", "Account");
                    }
                }
            }

            return View();
        }

        
        public ActionResult Logout()
        {
            string userName = Session["UserName"].ToString();

            var loginUser = lonersEntities.Users.Where(m => m.UserName == userName).FirstOrDefault();
            loginUser.Status = "Logged Out";
            lonersEntities.SaveChanges();

            Session.Abandon();

            return RedirectToAction("Login", "Account");
        }

    }
}
