using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using KAMS.Models;

namespace KAMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Email model, HttpPostedFileBase myFiles)
        {
            MailMessage mailim = new MailMessage();
            mailim.From = new MailAddress("kurucu741@yandex.com"); //gönderen mail adresi
            mailim.To.Add("kurucu741@gmail.com"); //Alıcı Mail adresi
            mailim.Subject = "KAMS CONTACT," + model.subject; //Konu
            mailim.Body = "Sayın Yetkili," + model.emailadress + " E-Posta adresli, " + model.name +
                " kişisinden gelen mesajınızın içeriği aşağıdaki gibidir; <br>" + model.message; // Mail içerik
            mailim.IsBodyHtml = true; //HTML Formatında göndermek için

            if (myFiles != null)
            {
                mailim.Attachments.Add(new Attachment(myFiles.InputStream, myFiles.FileName));
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("kurucu741@yandex.com.tr", "yiwfpwmhimaufiyo");

            smtp.Port = 587;
            smtp.Host = "smtp.yandex.com";
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mailim);
                TempData["Message"] = "Mesajınız iletilmiştir. En kısa zamanda size geri dönüş sağlanacaktır.";
                return RedirectToAction("Contact");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Mesaj gönderilemedi.Hata nedeni:" + ex.Message;
                return RedirectToAction("Contact");

            }
            //return View();
        }


        public ActionResult Services()
        {
            //ViewBag.Message = "Your services page.";

            return View();
        }
        
        public ActionResult Team()
        {
            //ViewBag.Message = "Your team page.";
            
            return View();
        }

        public ActionResult ozbey()
        {
            return Redirect("https://www.linkedin.com/in/serkanozbeykurucu");
        }

    }
}