using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrnekProje.Controllers
{
    public class SiniflarController : Controller
    {
        OrnekProje.Models.OrnekProjeEntities db = new Models.OrnekProjeEntities();
        // GET: Siniflar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Save(Models.Siniflar Siniflar)
        {
            try
            {
                if (Siniflar.SinifAdi != null & Siniflar.KontejyanSayisi != null)
                {
                    if (Siniflar.Id == 0)
                    {
                        db.Siniflar.Add(Siniflar); db.SaveChanges();
                    }
                    else
                    {                        
                        Models.Siniflar sinif = db.Siniflar.Where(p => p.Id == Siniflar.Id).FirstOrDefault();
                        sinif.SinifAdi = Siniflar.SinifAdi;
                        sinif.KontejyanSayisi = Siniflar.KontejyanSayisi;
                        sinif.OlusturmaTarihi = DateTime.Now;
                        db.SaveChanges();
                    }
                    return Json(Models.Status.Result.Succes, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Lütfen işleminizi kontrol ediniz.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        [HttpGet]
        public JsonResult GetSiniflar()
        {
            try
            {
                var modelResult = db.Siniflar.ToList();
                if (modelResult == null) // Error
                {
                    //  LogServices.AddLog(Session["S_UserName"].ToString(), "Fatal", "Error", "Error : NotResut", null, null);
                    return Json("Listtelenecek kayıt bulunamadı..", JsonRequestBehavior.AllowGet);
                }

                return Json(modelResult, JsonRequestBehavior.AllowGet); // Success
            }
            catch (Exception ex)
            {
                // LogServices.AddLog(Session["S_UserName"].ToString(), "Fatal", "Error", "Error : " + ex.ToString(), null, ex);
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDeletedById(string Id)
        {
            try
            {
                int myId = Convert.ToInt32(Id);
                Models.Siniflar modelResult = db.Siniflar.Where(p => p.Id == myId).FirstOrDefault();
                if (modelResult != null) // Error
                {
                    db.Siniflar.Remove(modelResult);
                    db.SaveChanges();

                    return Json(Models.Status.Result.Succes, JsonRequestBehavior.AllowGet); // Success
                }
                else
                    return Json("Silinecek kayıt bulunamadı.", JsonRequestBehavior.AllowGet); // Success
            }
            catch (Exception ex)
            {

                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}