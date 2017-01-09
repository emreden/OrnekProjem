using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrnekProje.Controllers
{
    public class SinavlarController : Controller
    {
        OrnekProje.Models.OrnekProjeEntities db = new Models.OrnekProjeEntities();
        // GET: Sinavlar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Save(Models.Sinavlar Sinavlar)
        {
            try
            {
                Models.SinavHaftasi Shaftasi=   db.SinavHaftasi.FirstOrDefault();
                var Dersler = db.Dersler.ToList().OrderBy(p => p.Donem);
                var Siniflar = db.Siniflar.ToList();
                DateTime BasDt = Convert.ToDateTime(Shaftasi.BaslangicTarihi);
                DateTime BitDt = Convert.ToDateTime(Shaftasi.BitisTarihi);
                DateTime SinavGunuDt = BasDt;
                int Saat = 9;
                Models.Sinavlar Sinav = new Models.Sinavlar();
                do
                {
                    if (SinavGunuDt.DayOfWeek != DayOfWeek.Saturday && SinavGunuDt.DayOfWeek != DayOfWeek.Sunday) // Tatil günü değil ise
                    {
                        foreach(var DersItem in Dersler)
                        {
                            foreach (var SinifItem in Siniflar)
                            {
                                if(SinifItem.KontejyanSayisi> DersItem.OgrenciSayisi)
                                {
                                    Sinav.DersId = DersItem.Id;
                                    Sinav.SinifId = SinifItem.Id;
                                    Sinav.SınavHaftasiId = Shaftasi.Id;
                                    Sinav.SınavTarihi = SinavGunuDt;
                                    //Sinav.Saat = Saat.ToString();
                                    db.Sinavlar.Add(Sinav);
                                    break;
                                }                                
                            }
                            Saat += 2; // Sınav  
                            if (Saat == 17)
                                break;
                            Saat ++; // Ara  
                        }
                    }
                    SinavGunuDt.AddDays(1);
                }
                while (SinavGunuDt == BitDt);

                return Json(Models.Status.Result.Succes, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                throw;
            }
        }
    }
}