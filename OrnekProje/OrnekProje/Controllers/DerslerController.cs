using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrnekProje.Controllers
{
    public class DerslerController : Controller
    {
        // GET: Dersler
        OrnekProje.Models.OrnekProjeEntities db = new Models.OrnekProjeEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetDersler()
        {
            try
            {
                var modelResult = db.Dersler.ToList();
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
                Models.Dersler modelResult = db.Dersler.Where(p => p.Id == myId).FirstOrDefault();
                if (modelResult != null) // Error
                {
                    db.Dersler.Remove(modelResult);
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
        public ActionResult Save(Models.Dersler Dersler)
        {
            try
            {   
                if (Dersler.OgrenciSayisi != null & Dersler.Donem !=null)
                {
                    if (Dersler.Id == 0)
                    {
                        db.Dersler.Add(Dersler);
                        db.SaveChanges();
                    }
                    else
                    {
                        Models.Dersler ders = db.Dersler.Where(p => p.Id == Dersler.Id).FirstOrDefault();
                        ders.DersAdi = Dersler.DersAdi;
                        ders.Aciklama = Dersler.Aciklama;
                        ders.OgrenciSayisi = Dersler.OgrenciSayisi;
                        ders.Donem = Dersler.Donem;
                        ders.OlusturmaTarihi = DateTime.Now;
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
    }
}