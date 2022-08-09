using KAMS.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KAMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        private string CreatePassword(int length, string chars)
        {
            StringBuilder Password = new StringBuilder();
            Random Rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                Password.Append(chars[Rnd.Next(0, chars.Length)]);
            }

            return Password.ToString();
        }
        public DbApartmantManagamentSystemsEntities2 db = new DbApartmantManagamentSystemsEntities2();
        public ActionResult Index()
        {
            ViewBag.USERCOUNT = (from x in db.USERS select x.ID).Count();
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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Member()
        {
            ViewBag.YETKIID = new SelectList(db.YETKILER, "ID", "YETKI");
            return View(db.USERS.ToList());
        }
        [HttpGet]
        public ActionResult MemberAdd()
        {
            string chars = "abcdefghijklmnoprstuvyzwxqABCDEFGHJKLMNOPQRSTUVWXYZ1234567890!@$?_-";
            string Parola = CreatePassword(10, chars);
            USERS u = new USERS();
            u.ct = new SelectList(db.COUNTRY, "ID", "COUNTRY1");
            u.c = new SelectList(db.CITY, "ID", "CITY1");
            u.t = new SelectList(db.TOWNS, "ID", "TOWNS1");
            u.d = new SelectList(db.DISTRICTS, "ID", "DISTRICT");
            u.C_PASSWORD = Parola;
            ViewBag.COUNTRY = u.ct;
            ViewBag.CITY = u.c;
            ViewBag.TOWNS = u.t;
            ViewBag.DISTRICT = u.d;
            ViewBag.YETKIID = new SelectList(db.YETKILER, "ID", "YETKI");

            return View(u);
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
        [HttpPost]
        public ActionResult MemberAdd(USERS users, HttpPostedFileBase img)
        {

            string path = Server.MapPath("~/assets/img/adminImg/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    string fileName = Path.GetFileName(img.FileName);
                    img.SaveAs(path + fileName);
                    users.img = "/assets/img/adminImg/" + fileName;
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
                //users.img = "~/App_Data/" + filename ;
                db.USERS.Add(users);
                db.SaveChanges();
                return RedirectToAction("Member");
            }

            //if (ModelState.IsValid)
            //{
            //    db.USERS.Add(users);
            //    db.SaveChanges();
            //    return RedirectToAction("Member");
            //}

            users.ct = new SelectList(db.COUNTRY, "ID", "COUNTRY1");
            users.c = new SelectList(db.CITY, "ID", "CITY1");
            users.t = new SelectList(db.TOWNS, "ID", "TOWNS1");
            users.d = new SelectList(db.DISTRICTS, "ID", "DISTRICT");

            ViewBag.COUNTRY = users.ct;
            ViewBag.CITY = users.c;
            ViewBag.TOWNS = users.t;
            ViewBag.DISTRICT = users.d;
            ViewBag.YETKIID = new SelectList(db.YETKILER, "ID", "YETKI");
            return View(users);
        }
        public ActionResult MemberDetail(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERS users = db.USERS.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);

        }

        [HttpGet]
        public ActionResult MemberDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERS user = db.USERS.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult MemberDelete(int id)
        {
            USERS user = db.USERS.Find(id);
            db.USERS.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Member");
        }

        [HttpGet]
        public ActionResult MemberEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERS user = db.USERS.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            //TOWNS == İLÇE
            //USERS u = new USERS();
            user.ct = new SelectList(db.COUNTRY, "ID", "COUNTRY1", user.COUNTRY);
            user.c = new SelectList(db.CITY, "ID", "CITY1", user.CITY);
            user.t = new SelectList(db.TOWNS, "ID", "TOWNS1", user.TOWNS);
            user.d = new SelectList(db.DISTRICTS, "ID", "DISTRICT", user.DISTRICT);
            ViewBag.COUNTRY = user.ct;
            ViewBag.CITY = user.c;
            ViewBag.TOWNS = user.t;
            ViewBag.DISTRICT = user.d;

            ViewBag.YETKIID = new SelectList(db.YETKILER, "ID", "YETKI", user.YETKIID);
            return View(user);
        }

        [HttpPost]
        public ActionResult MemberEdit(USERS user, HttpPostedFileBase img)
        {

            string path = Server.MapPath("~/assets/img/adminImg/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    string fileName = Path.GetFileName(img.FileName);
                    img.SaveAs(path + fileName);
                    user.img = "/assets/img/adminImg/" + fileName;
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Member");
            }
            ViewBag.COUNTRY = new SelectList(db.COUNTRY, "ID", "COUNTRY1", user.COUNTRY);
            ViewBag.CITY = new SelectList(db.CITY, "ID", "CITY1", user.CITY);
            ViewBag.TOWNS = new SelectList(db.TOWNS, "ID", "TOWNS1", user.TOWNS);
            ViewBag.DISTRICT = new SelectList(db.DISTRICTS, "ID", "DISTRICT", user.DISTRICT);
            ViewBag.YETKIID = new SelectList(db.YETKILER, "ID", "YETKI", user.YETKIID);
            return View(user);
        }

        public ActionResult ApartmantAidat()
        {

            var result = (from x in db.ApartmanAidat
                          join y in db.ApartmantDetails on x.AID equals y.ID
                          select new
                          {
                              Text = x.TUTAR.ToString(),
                              Value = x.ID.ToString()
                          }).ToList();

            KAMS.Models.ApartmanAidat apartmanAidat = new ApartmanAidat();
            KAMS.Models.ApartmantDetails apartmanDetails = new ApartmantDetails();
            ApartmantAidatVDetail a = new ApartmantAidatVDetail
            {
                Aidats = db.ApartmanAidat.ToList(),
                ApartmantDetails = db.ApartmantDetails.ToList()
            };


            //var result = db.ApartmanAidat.ToList();
            return PartialView(result);
        }

        public ActionResult ApartmantLists()
        {

            var result = db.C_ApartmantDetailView.ToList();
            return View(result);
        }
        public int apartmanid;
        public int blocid;
        [HttpGet]
        public ActionResult ApartmanBlocAdd(int? id)
        {
            apartmanid = Convert.ToInt32(id);
            var result = db.ApartmantDetails.FirstOrDefault(x => x.ID == id);
            if (id != null)
            {
                ViewBag.APID = new SelectList(db.ApartmantDetails.Where(x => x.ID == id), "ID", "NAME");
            }
            else
            {
                ViewBag.APID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult ApartmanBlocAdd(ApartmantBlocs apartmantBlocs)
        {
            if (ModelState.IsValid)
            {
                db.ApartmantBlocs.Add(apartmantBlocs);
                db.SaveChanges();
                return RedirectToAction("ApartmantLists");
            }
            ViewBag.APID = new SelectList(db.ApartmantDetails.Where(x => x.ID == apartmanid), "ID", "NAME");
            return View(apartmantBlocs);
        }


        [HttpGet]
        public ActionResult ApartmanAidatAdd(int? id)
        {
            apartmanid = Convert.ToInt32(id);
            var result = db.ApartmantDetails.FirstOrDefault(x => x.ID == id);
            ViewBag.AID = new SelectList(db.ApartmantDetails.Where(x => x.ID == id), "ID", "NAME");
            return View();
        }
        [HttpPost]
        public ActionResult ApartmanAidatAdd(ApartmanAidat apartmanAidat)
        {

            if (ModelState.IsValid)
            {
                db.ApartmanAidat.Add(apartmanAidat);
                db.SaveChanges();
                return RedirectToAction("ApartmantLists");
            }
            ViewBag.AID = new SelectList(db.ApartmantDetails.Where(x => x.ID == apartmanid), "ID", "NAME");
            return View(apartmanAidat);
        }

        [HttpGet]
        public ActionResult ApartmantKatAdd(int? id)
        {
            if (id != null)
            {
                ViewBag.ABID = new SelectList(db.ApartmantBlocs.Where(x => x.APID == id), "ID", "BLOCNAME");
                ViewBag.ADID = new SelectList(db.ApartmantDetails.Where(x => x.ID == id), "ID", "NAME");
            }
            else
            {
                ViewBag.ABID = new SelectList(db.ApartmantBlocs, "ID", "BLOCNAME");
                ViewBag.ADID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ApartmantKatAdd(KATLAR kat)
        {
            if (ModelState.IsValid)
            {
                db.KATLAR.Add(kat);
                db.SaveChanges();
                return RedirectToAction("ApartmantLists");
            }
            ViewBag.ABID = new SelectList(db.ApartmantBlocs, "ID", "BLOCNAME");
            ViewBag.ADID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            return View(kat);
        }

        [HttpGet]
        public ActionResult ApartmantDaireAdd(int? id)
        {
            DAIRELER daire = new DAIRELER();

            ViewBag.ADID = new SelectList(db.ApartmantDetails.Where(x => x.ID == id), "ID", "NAME");
            ViewBag.KATID = new SelectList(db.KATLAR, "ID", "KATNO");
            daire.blocid = new SelectList(db.ApartmantBlocs.Where(x=> x.APID == id), "ID", "BLOCNAME");
            ViewBag.ABID = daire.blocid;
            return View();
        }

        [HttpPost]
        public ActionResult ApartmantDaireAdd(DAIRELER daires)
        {


            if (ModelState.IsValid)
            {
                db.DAIRELER.Add(daires);
                db.SaveChanges();
                return RedirectToAction("ApartmantLists");
            }
            ViewBag.ADID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            ViewBag.ABID = new SelectList(db.ApartmantBlocs, "ID", "BLOCNAME");
            ViewBag.KATID = new SelectList(db.KATLAR, "ID", "KATNO");
            return View(daires);
        }


        public JsonResult Katgetir(int p)
        {

            var katlar = (from k in db.KATLAR
                          join ab in db.ApartmantBlocs on k.ApartmantBlocs.ID equals ab.ID
                          where k.ApartmantBlocs.ID == p
                          select new
                          {
                              Text = k.KATNO,
                              Value = k.ID.ToString()
                          }
                          ).ToList();
            return Json(katlar, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ApartmantDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ApartmantDetails apartmantDetails = db.ApartmantDetails.Find(id);
            C_ApartmantDetailView c_ApartmantDetailView = db.C_ApartmantDetailView.Find(id);
            if (c_ApartmantDetailView == null)
            {
                return HttpNotFound();
            }
            return View(c_ApartmantDetailView);
        }
        public ActionResult ApartmantBlokSayisi()
        {
            //KAMS.Models.ApartmantBlocs apartmanBlocs = new ApartmantBlocs();
            ApartmantAidatVDetail a = new ApartmantAidatVDetail
            {
                Blocs = db.ApartmantBlocs.ToList()
            };
            //var result = db.ApartmanAidat.ToList();
            return PartialView(a);
        }

        [HttpGet]
        public ActionResult ApartmantAdd()
        {
            ApartmantDetails apartmant = new ApartmantDetails();
            apartmant.ct = new SelectList(db.COUNTRY, "ID", "COUNTRY1");
            apartmant.c = new SelectList(db.CITY, "ID", "CITY1");
            apartmant.t = new SelectList(db.TOWNS, "ID", "TOWNS1");
            apartmant.d = new SelectList(db.DISTRICTS, "ID", "DISTRICT");

            ViewBag.CID = apartmant.ct;
            ViewBag.CTID = apartmant.c;
            ViewBag.TWID = apartmant.t;
            ViewBag.DTID = apartmant.d;
            return View();
        }

        [HttpPost]
        public ActionResult ApartmantAdd(ApartmantDetails apartmant)
        {

            if (ModelState.IsValid)
            {
                db.ApartmantDetails.Add(apartmant);
                db.SaveChanges();
                return RedirectToAction("ApartmantLists");
            }
            ViewBag.CTID = new SelectList(db.CITY, "ID", "CITY1");
            ViewBag.CID = new SelectList(db.COUNTRY, "ID", "COUNTRY1");
            ViewBag.DTID = new SelectList(db.DISTRICTS, "ID", "DISTRICT");
            ViewBag.TWID = new SelectList(db.TOWNS, "ID", "TOWNS1");

            return View(apartmant);
        }


        [HttpGet]
        public ActionResult ApartmantEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmantDetails apartmantDetails = db.ApartmantDetails.Find(id);
            if (apartmantDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.CTID = new SelectList(db.CITY, "ID", "CITY1", apartmantDetails.CTID);
            ViewBag.CID = new SelectList(db.COUNTRY, "ID", "COUNTRY1", apartmantDetails.CID);
            ViewBag.DTID = new SelectList(db.DISTRICTS, "ID", "DISTRICT", apartmantDetails.DTID);
            ViewBag.TWID = new SelectList(db.TOWNS, "ID", "TOWNS1", apartmantDetails.TWID);
            return View(apartmantDetails);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApartmantEdit([Bind(Include = "ID,NAME,CID,CTID,TWID,DTID,C_ADDRESS")] ApartmantDetails apartmantDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartmantDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ApartmantLists");
            }
            ViewBag.CTID = new SelectList(db.CITY, "ID", "CITY1", apartmantDetails.CTID);
            ViewBag.CID = new SelectList(db.COUNTRY, "ID", "COUNTRY1", apartmantDetails.CID);
            ViewBag.DTID = new SelectList(db.DISTRICTS, "ID", "DISTRICT", apartmantDetails.DTID);
            ViewBag.TWID = new SelectList(db.TOWNS, "ID", "TOWNS1", apartmantDetails.TWID);
            return View(apartmantDetails);
        }

        [HttpGet]
        public ActionResult ApartmantDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmantDetails apartmantDetails = db.ApartmantDetails.Find(id);
            if (apartmantDetails == null)
            {
                return HttpNotFound();
            }
            return View(apartmantDetails);
        }

        [HttpPost]
        public ActionResult ApartmantDelete(int id)
        {
            ApartmantDetails apartmantDetails = db.ApartmantDetails.Find(id);
            db.ApartmantDetails.Remove(apartmantDetails);
            db.SaveChanges();
            return RedirectToAction("ApartmantLists");
        }

        public ActionResult ApartmanManagmemtUser()
        {
            return View(db.AMU.ToList());
        }


        public ActionResult ApartmantManagementDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AMU amu = db.AMU.Find(id);
            if (amu == null)
            {
                return HttpNotFound();
            }
            return View(amu);
        }

        [HttpGet]
        public ActionResult ApartmantManagementEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AMU amu = db.AMU.Find(id);
            if (amu == null)
            {
                return HttpNotFound();
            }
            ViewBag.ADID = new SelectList(db.ApartmantDetails, "ID", "NAME", amu.ADID);
            ViewBag.UID = new SelectList(db.USERS, "ID", "USERNAME", amu.UID);
            return View(amu);
        }
        [HttpPost]
        public ActionResult ApartmantManagementEdit(AMU amu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ApartmanManagmemtUser");
            }
            ViewBag.ADID = new SelectList(db.ApartmantDetails, "ID", "NAME", amu.ADID);
            ViewBag.UID = new SelectList(db.USERS, "ID", "USERNAME", amu.UID);
            return View(amu);
        }

        [HttpGet]
        public ActionResult ApartmantManagementDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AMU amu = db.AMU.Find(id);
            if (amu == null)
            {
                return HttpNotFound();
            }
            return View(amu);
        }

        [HttpPost]
        public ActionResult ApartmantManagementDelete(int id)
        {

            AMU amu = db.AMU.Find(id);
            db.AMU.Remove(amu);
            db.SaveChanges();
            return RedirectToAction("ApartmanManagmemtUser");
        }

        public ActionResult ApartmantManagementCreate()
        {
            ViewBag.ADID = new SelectList(db.ApartmantDetails, "ID", "NAME");
            ViewBag.UID = new SelectList(db.USERS, "ID", "USERNAME");
            return View();
        }

        [HttpPost]
        public ActionResult ApartmantManagementCreate(AMU amu)
        {
            if (ModelState.IsValid)
            {
                db.AMU.Add(amu);
                db.SaveChanges();
                return RedirectToAction("ApartmanManagmemtUser");
            }

            ViewBag.ADID = new SelectList(db.ApartmantDetails, "ID", "NAME", amu.ADID);
            ViewBag.UID = new SelectList(db.USERS, "ID", "USERNAME", amu.UID);
            return View(amu);
        }

    }
}