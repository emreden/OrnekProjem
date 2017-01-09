using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrnekProje.Controllers
{
    public class SinavHaftasiController : Controller
    {
        OrnekProje.Models.OrnekProjeEntities db = new Models.OrnekProjeEntities();
        // GET: SinavHaftasi
        public ActionResult Index()
        {
            return View();
        }
        public partial class SinavHafta
        {
            public int Id { get; set; }
            public string BaslangicTarihi { get; set; }
            public string BitisTarihi { get; set; }
            public string OlusturmaTarihi { get; set; }
            public string OlusturanKullanici { get; set; }
        }
        
        public JsonResult Save(int Id,string BasTar, string BitTar)
        {
            try
            {
                if (BasTar != null & BitTar != null)
                {
                    if (Id == 0)
                    {
                        Models.SinavHaftasi shaftasi = new Models.SinavHaftasi();
                        shaftasi.BaslangicTarihi = Convert.ToDateTime(BasTar);
                        shaftasi.BitisTarihi = Convert.ToDateTime(BitTar);
                        shaftasi.OlusturmaTarihi = DateTime.Now;
                        db.SinavHaftasi.Add(shaftasi);
                        db.SaveChanges();
                    }
                    else
                    {
                        Models.SinavHaftasi shaftasi = db.SinavHaftasi.Where(p => p.Id == Id).FirstOrDefault();
                        shaftasi.BaslangicTarihi = Convert.ToDateTime(BasTar);
                        shaftasi.BaslangicTarihi = Convert.ToDateTime(BitTar);
                        shaftasi.OlusturmaTarihi = DateTime.Now;
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
        public JsonResult GetSinavHaftasi()
        {
            try
            {
                var modelResult = db.SinavHaftasi.ToList();
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
        public ActionResult GetSinavHaftasiById(string Id) {
            try
            {
                int myId = Convert.ToInt32(Id);
                Models.SinavHaftasi modelResult = db.SinavHaftasi.Where(p => p.Id == myId).FirstOrDefault();
                if (modelResult != null) // Error
                {  
                    return Json(modelResult, JsonRequestBehavior.AllowGet); // Success
                }
                else
                    return Json(Models.Status.Result.Error, JsonRequestBehavior.AllowGet); // Success
            }
            catch (Exception ex)
            {

                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDeletedById(string Id)
        {
            try
            {
                int myId = Convert.ToInt32(Id);
                Models.SinavHaftasi modelResult = db.SinavHaftasi.Where(p => p.Id == myId).FirstOrDefault();
                if (modelResult != null) // Error
                {
                    db.SinavHaftasi.Remove(modelResult);
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