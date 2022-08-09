using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using KAMS.Models;
using System.Web.Security;

namespace KAMS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public DbApartmantManagamentSystemsEntities2 db = 
            new DbApartmantManagamentSystemsEntities2();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(USERS users, AMU amu)
        {
            var results = db.USERS.FirstOrDefault(x=> x.USERNAME == users.USERNAME 
            && x.C_PASSWORD == users.C_PASSWORD);
           
            if (results != null)
            {
                if (results.YETKIID == 1)
                {
                    FormsAuthentication.SetAuthCookie(results.USERNAME, false);
                    Session["username"] = results.USERNAME;
                    Session["name"] = results.C_NAME;
                    Session["id"] = results.ID.ToString();
                    return RedirectToAction("Index", "Admin");
                }
                else if (results.YETKIID == 2)
                {
                    FormsAuthentication.SetAuthCookie(results.USERNAME, false);
                    Session["id"] = results.ID;
                    Session["username"] = results.USERNAME;
                    return RedirectToAction("Member", "ApartmantManagement");
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.error = "Kullanıcı Adını veya şifrenizi hatalı girdiniz.    Lütfen Tekrar Deneyiniz!";
                return View();
            }
        }
    }
}
