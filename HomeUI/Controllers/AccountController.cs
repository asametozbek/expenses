using Data;
using Entity;
using HomeUI.Exception;
using HomeUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;

namespace HomeUI.Controllers
{
    public class AccountController : Controller
    {
        private IRepository<Household> _reposHousehold;
        public AccountController(IRepository<Household> reposHousehold)
        {
            _reposHousehold = reposHousehold;
        }

        public IActionResult Date()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Date(Deneme model)
        {
            var a = model.date;
            if (a!=null)
            {
                model.date1 = a.Value.ToString("dd MMMM yyyy", new CultureInfo("tr-TR"));
            }
            
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var e = HttpContext.Request.Headers["sec-ch-ua"].ToString();
                var user = _reposHousehold.GetAll().Where(x => x.UserName == username & x.Password == password).FirstOrDefault();
                if (user != null)
                {

                    if (e!=user.SessionLogin)
                    {
                        user.SessionLogin = e;
                        _reposHousehold.Update(user);
                        _reposHousehold.Save();
                    }

                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("user", user.Id.ToString(), cookie);
                    HttpContext.Session.SetObjectAsJson("User", user);


                    return Json(new { Success = true, data = user });
                }

                return Json(new { Success = false, Mesaj = "Kullanıcı adı veya şifre yanlış" });
            }
            catch (System.Exception ex)
            {
                ViewBag.ms = ex.Message;
                return View();
                throw;
            }


        }
        [UserAuthorize]
        public IActionResult MyProfile()
        {
            var model = HttpContext.Session.GetObjectFromJson<Household>("User");

            return View(model);
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("User");
            Response.Cookies.Delete("user");

            return RedirectToAction("Login", "Account");
        }
    }
}
