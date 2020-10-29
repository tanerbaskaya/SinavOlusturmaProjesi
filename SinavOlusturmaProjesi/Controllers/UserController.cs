using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SinavOlusturmaProjesi.DAL;

namespace SinavOlusturmaProjesi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Models.User userModel)
        {
            LoginDal loginControl = new LoginDal();
            if(loginControl.UserControl(userModel.Username, userModel.Password))
            {
                HttpContext.Session.SetString("UserName", userModel.Username.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error="Kullanıcı Adı veya Şifre Hatalı";
                return View();
            }

        }

    }
}
