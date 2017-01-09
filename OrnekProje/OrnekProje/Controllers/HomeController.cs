using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrnekProje.Controllers
{
    public class HomeController : Controller
    {
        OrnekProje.Models.OrnekProjeEntities db = new Models.OrnekProjeEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(Models.Kullanicilar user)
        {
            string UserMail = user.Mail;
            string UserPassword = user.Parola;
            var nusers = db.Kullanicilar.Where(p => p.Mail == UserMail && p.Parola == UserPassword).FirstOrDefault();
            if (nusers != null)
            {
                Session.Add("S_UserId", nusers.Id);
                Session.Add("S_UserName", nusers.Mail);

                return Redirect("/Dersler");
            }
            return Redirect("/Home");
        }
    }
}