using KAMS.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;


namespace KAMS.Controllers
{
    [Authorize(Roles = "Admin , Kullanıcı")]
    public class ApartmantManagementController : Controller
    {
        // GET: ApartmantManagement

      

        public DbApartmantManagamentSystemsEntities2 db = new DbApartmantManagamentSystemsEntities2();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            int id = Convert.ToInt32(Session["id"]);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERS users = db.USERS.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.namesurmane = users.C_NAME + " " + users.C_SURNAME;
            ViewBag.Phone = users.TELEPHONE;
            ViewBag.CITY = new SelectList(db.CITY, "ID", "CITY1");
            ViewBag.COUNTRY = new SelectList(db.COUNTRY, "ID", "COUNTRY1");
            ViewBag.img = users.img.ToString();
            return View(users);
        }

        public JsonResult ilcegetir(int p)
        {
            var ileceler = (from x in db.TOWNS
                            join y in db.CITY on x.CITY.ID equals y.ID
                            where y.ID == p
                            select new
                            {
                                Text = x.TOWNS1,
                                Value = x.ID.ToString()
                            }).ToList();
            return Json(ileceler, JsonRequestBehavior.AllowGet);
        }
        public JsonResult semtgetir(int p)
        {
            var semtler = (from x in db.DISTRICTS
                           join y in db.TOWNS on x.TOWNS.ID equals y.ID
                           where y.ID == p
                           select new
                           {
                               Text = x.DISTRICT,
                               Value = x.ID.ToString()
                           }).ToList();
            return Json(semtler, JsonRequestBehavior.AllowGet);
        }


        public JsonResult blokgetir(int p)
        {

            var bloklar = (from x in db.ApartmantBlocs
                           join y in db.ApartmantDetails on x.APID equals y.ID
                           where y.ID == p
                           select new
                           {
                               Text = x.BLOCNAME,
                               Value = x.ID.ToString()
                           }).ToList();
            return Json(bloklar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult katgetir(int p)
        {

            var katlar = (from x in db.KATLAR
                          join y in db.ApartmantBlocs on x.ABID equals y.ID
                          where y.ID == p
                          select new
                          {
                              Text = x.KATNO,
                              Value = x.ID.ToString()
                          }).ToList();
            return Json(katlar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult dairegetir(int p)
        {

            var daireler = (from x in db.DAIRELER
                            join y in db.KATLAR on x.KATID equals y.ID
                            where y.ID == p
                            select new
                            {
                                Text = x.DAIRENO,
                                Value = x.ID.ToString()
                            }).ToList();
            return Json(daireler, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Member()
        {
            int id = Convert.ToInt32(Session["id"]);

            var result = db.RESIDENT.OrderByDescending(x => x.ID).ToList();
            return View(result);
        }

        public ActionResult MemberAdd()
        {
            ViewBag.COUNTRY = new SelectList(db.COUNTRY, "ID", "COUNTRY1");
            ViewBag.CITY = new SelectList(db.CITY, "ID", "CITY1");
            ViewBag.TOWNS = new SelectList(db.TOWNS, "ID", "TOWNS1");
            ViewBag.DISTRICTS = new SelectList(db.DISTRICTS, "ID", "DISTRICT");
            ViewBag.ApartmanID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            ViewBag.ApartmanBlocs = new SelectList(db.ApartmantBlocs, "ID", "BLOCNAME");
            ViewBag.KatID = new SelectList(db.KATLAR, "ID", "KATNO");
            ViewBag.DAIREID = new SelectList(db.DAIRELER, "ID", "DAIRENO");
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult MemberAdd(RESIDENT resident)
        {
            if (ModelState.IsValid)
            {
                db.RESIDENT.Add(resident);
                db.SaveChanges();
                return RedirectToAction("Member");
            }
            ViewBag.COUNTRY = new SelectList(db.COUNTRY, "ID", "COUNTRY1");
            ViewBag.CITY = new SelectList(db.CITY, "ID", "CITY1");
            ViewBag.TOWNS = new SelectList(db.TOWNS, "ID", "TOWNS1");
            ViewBag.DISTRICTS = new SelectList(db.DISTRICTS, "ID", "DISTRICT");
            ViewBag.ApartmanID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            ViewBag.ApartmanBlocs = new SelectList(db.ApartmantBlocs, "ID", "BLOCNAME");
            ViewBag.KatID = new SelectList(db.KATLAR, "ID", "KATNO");
            ViewBag.DAIREID = new SelectList(db.DAIRELER, "ID", "DAIRENO");
            return View(resident);
        }

        public ActionResult ApartmantRevenue()
        {
            var result = db.ApartmanRevenue.ToList();
            return View(result);
        }


        public ActionResult ApartmantRevenueDetail(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmanRevenue apartmanRevenue = db.ApartmanRevenue.Find(id);

            if (apartmanRevenue == null)
            {
                return HttpNotFound();
            }
            return View(apartmanRevenue);

        }
        [HttpGet]
        public ActionResult ApartmantRevenueDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmanRevenue apartmanRevenue = db.ApartmanRevenue.Find(id);
            if (apartmanRevenue == null)
            {
                return HttpNotFound();

            }
            db.ApartmanRevenue.Remove(apartmanRevenue);
            return RedirectToAction("ApartmantRevenue");
        }

        [HttpGet]
        public ActionResult ApartmantRevenueEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmanRevenue apartmanRevenue = db.ApartmanRevenue.Find(id);
            if (apartmanRevenue == null)
            {
                return HttpNotFound();

            }
            ViewBag.AID = new SelectList(db.ApartmantDetails, "ID", "NAME", apartmanRevenue.AID);
            ViewBag.REVUNUETYPE = new SelectList(db.REVUNUETYPE, "ID", "REVUNUE", apartmanRevenue.REVUNUETYPE);
            return View(apartmanRevenue);
        }

        [HttpPost]

        public ActionResult ApartmantRevenueEdit(ApartmanRevenue apartmanRevenue)
        {

            if (ModelState.IsValid)
            {
                db.Entry(apartmanRevenue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ApartmantRevenue");
            }
            ViewBag.AID = new SelectList(db.ApartmantDetails, "ID", "NAME", apartmanRevenue.AID);
            ViewBag.REVUNUETYPE = new SelectList(db.REVUNUETYPE, "ID", "REVUNUE", apartmanRevenue.REVUNUETYPE);
            return View(apartmanRevenue);
        }

        [HttpGet]
        public ActionResult ApartmantRevenueAdd()
        {
            ViewBag.AID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            ViewBag.REVUNUETYPE = new SelectList(db.REVUNUETYPE, "ID", "REVUNUE");
            return View();
        }

        [HttpPost]
        public ActionResult ApartmantRevenueAdd(ApartmanRevenue apartmanRevenue)
        {
            if (ModelState.IsValid)
            {
                db.ApartmanRevenue.Add(apartmanRevenue);
                db.SaveChanges();
                return RedirectToAction("ApartmantRevenue");
            }
            ViewBag.AID = new SelectList(db.ApartmantDetails, "ID", "NAME", apartmanRevenue.AID);
            ViewBag.REVUNUETYPE = new SelectList(db.REVUNUETYPE, "ID", "REVUNUE", apartmanRevenue.REVUNUETYPE);
            return View(apartmanRevenue);
        }


        public ActionResult ApartmanManagmemtExpend()
        {
            return View(db.ApartmanExpense.ToList());
        }


        [HttpGet]
        public ActionResult ApartmantManagementExpendCreate()
        {
            ViewBag.AID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            ViewBag.EpenseType = new SelectList(db.EXPENSETYPE, "ID", "EXPENSE");
            return View();
        }

        [HttpPost]
        public ActionResult ApartmantManagementExpendCreate(ApartmanExpense apartmanExpense)
        {
            if (ModelState.IsValid)
            {
                db.ApartmanExpense.Add(apartmanExpense);
                db.SaveChanges();
                return RedirectToAction("ApartmanManagmemtExpend");
            }
            ViewBag.AID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            ViewBag.EpenseType = new SelectList(db.EXPENSETYPE, "ID", "EXPENSE");
            return View(apartmanExpense);

        }

        [HttpGet]
        public ActionResult ApartmantManagementExpendEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            ApartmanExpense apartmanExpense = db.ApartmanExpense.Find(id);
            if (apartmanExpense == null)
            {
                return HttpNotFound();

            }
            ViewBag.AID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            ViewBag.EpenseType = new SelectList(db.EXPENSETYPE, "ID", "EXPENSE");
            return View(apartmanExpense);
        }

        [HttpPost]
        public ActionResult ApartmantManagementExpendEdit(ApartmanExpense apartmanExpense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartmanExpense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ApartmanManagmemtExpend");
            }
            ViewBag.AID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            ViewBag.EpenseType = new SelectList(db.EXPENSETYPE, "ID", "EXPENSE");
            return View(apartmanExpense);

        }

        public ActionResult ApartmantManagementExpendDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmanExpense apartmanExpense = db.ApartmanExpense.Find(id);

            if (apartmanExpense == null)
            {
                return HttpNotFound();
            }
            return View(apartmanExpense);

        }


        [HttpGet]
        public ActionResult ApartmantManagementExpendDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmanExpense apartmanExpense = db.ApartmanExpense.Find(id);
            if (apartmanExpense == null)
            {
                return HttpNotFound();

            }
            db.ApartmanExpense.Remove(apartmanExpense);
            return RedirectToAction("ApartmanManagmemtExpend");
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");

        }

        public ActionResult ResidentAccount()
        {
            var result = db.C_ResidentAccountDetailView.ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult ResidentRevenueAdd()
        {
            ResidentRevenus resident = new ResidentRevenus()
            {
                DATE = Convert.ToDateTime(DateTime.Now),
                RAID = 0
            };
            ViewBag.REVENUETYPE = new SelectList(db.REVUNUETYPE, "ID", "REVUNUE");
            ViewBag.REID = new SelectList(db.RESIDENT, "ID", "NAMESURNAME");
            return View(resident);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResidentRevenueAdd(ResidentRevenus residentRevenus)
        {
            if (ModelState.IsValid)
            {
                db.ResidentRevenus.Add(residentRevenus);
                db.SaveChanges();
                return RedirectToAction("ResidentAccount");
            }

            ViewBag.REVENUETYPE = new SelectList(db.REVUNUETYPE, "ID", "REVUNUE", residentRevenus.REVENUETYPE);
            ViewBag.REID = new SelectList(db.RESIDENT, "ID", "NAMESURNAME", residentRevenus.REID);
            return View(residentRevenus);

        }

        [HttpGet]
        public ActionResult ResidentExpendAdd()
        {
            ResidentExpense resident = new ResidentExpense()
            {
                DATE = Convert.ToDateTime(DateTime.Now),
                RAID = 0
            };
            ViewBag.REVENETYPE = new SelectList(db.EXPENSETYPE, "ID", "EXPENSE");
            ViewBag.REID = new SelectList(db.RESIDENT, "ID", "NAMESURNAME");
            return View(resident);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ResidentExpendAdd(ResidentExpense residentExpense)
        {
            if (ModelState.IsValid)
            {
                db.ResidentExpense.Add(residentExpense);
                db.SaveChanges();
                return RedirectToAction("ResidentAccount");
            }

            //ViewBag.RAID = new SelectList(db.ResidentAccount, "ID", "ID", residentRevenus.RAID);
            ViewBag.ExpenseType = new SelectList(db.EXPENSETYPE, "ID", "EXPENSE", residentExpense.REVENETYPE);
            ViewBag.REID = new SelectList(db.RESIDENT, "ID", "NAMESURNAME", residentExpense.REID);
            return View(residentExpense);
        }

       

    }
}