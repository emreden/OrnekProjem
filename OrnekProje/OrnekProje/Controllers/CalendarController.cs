using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
namespace OrnekProje.Controllers
{
    public class CalendarController : Controller
    {
        OrnekProje.Models.OrnekProjeEntities db = new Models.OrnekProjeEntities();
        // GET: Calendar
        public ActionResult Index()
        {           
            return View();
        }
        public JsonResult Save(Models.Sinavlar Sinavlar)
        {
            try
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Sinavlar]"); db.SaveChanges();
                Models.SinavHaftasi Shaftasi = db.SinavHaftasi.FirstOrDefault();
                var Dersler = db.Dersler.ToList().OrderBy(p => p.Donem & p.OgrenciSayisi);
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
                        foreach (var DersItem in Dersler)
                        {
                            foreach (var SinifItem in Siniflar)
                            {                               
                                if (SinifItem.KontejyanSayisi > DersItem.OgrenciSayisi)
                                {
                                    Sinav.DersId = DersItem.Id;
                                    Sinav.SinifId = SinifItem.Id;
                                    Sinav.SınavHaftasiId = Shaftasi.Id;
                                    Sinav.SınavTarihi = SinavGunuDt;
                                    Sinav.Saat = Saat;
                                    db.Sinavlar.Add(Sinav);
                                    db.SaveChanges();
                                    break;
                                }
                            }
                            Saat += 2; // Sınav  
                            if (Saat == 17)
                            { SinavGunuDt = SinavGunuDt.AddDays(1); Saat = 9; }
                            else Saat++; // Ara  
                        }
                        break;
                    }                    
                }
                while (SinavGunuDt == BitDt);

                return Json(Models.Status.Result.Succes, JsonRequestBehavior.AllowGet); // Success   

            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        public ActionResult Save2()
        {
            try
            {
          
                Models.SinavHaftasi Shaftasi = db.SinavHaftasi.FirstOrDefault();
                var Dersler = db.Dersler.ToList().OrderBy(p => p.Donem);
                var Siniflar = db.Siniflar.ToList();
                DateTime BasDt = Convert.ToDateTime(Shaftasi.BaslangicTarihi);
                DateTime BitDt = Convert.ToDateTime(Shaftasi.BitisTarihi);
                DateTime SinavGunuDt = BasDt;
                int Saat = 9;
                Models.Sinavlar Sinav = new Models.Sinavlar();
                foreach (var DersItem in Dersler)
                {
                    foreach (var SinifItem in Siniflar)
                    {
                        do {

                                if (SinavGunuDt.DayOfWeek != DayOfWeek.Saturday && SinavGunuDt.DayOfWeek != DayOfWeek.Sunday)
                                        // Tatil günü değil ise
                                {
                                    if (SinifItem.KontejyanSayisi > DersItem.OgrenciSayisi)
                                    {
                                        Sinav.DersId = DersItem.Id;
                                        Sinav.SinifId = SinifItem.Id;
                                        Sinav.SınavHaftasiId = Shaftasi.Id;
                                        Sinav.SınavTarihi = SinavGunuDt;
                                        Sinav.Saat = Saat;
                                        db.Sinavlar.Add(Sinav);
                                        db.SaveChanges();
                                        break;
                                    }
                                }

                           } while (SinavGunuDt == BitDt);

                    }
                    
                }
                return View();

            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        public class Event
        {

            public string id { get; set; }
            public string resourceId { get; set; }
            public string title { get; set; }

            public bool AllDay { get; set; }

            public string start { get; set; }

            public string end { get; set; }

            //
            // You can add the other properties if required
            //2017-01-09T09:00:00
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, VaryByParam = "none", Duration = 0)]
        //public JsonResult GetCalendarValues()
        //{
        //    var events = new List<Event>();

        //    events.Add(new Event
        //    {//id: '1', resourceId: 'a', start: '2017-01-06', end: '2016-12-08', title: 'event 1'
        //        id = "1",
        //        resourceId = "a",
        //        title = "My First Event",
        //        start = "2017-01-09T09:00:00",// DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss"),
        //        end = "2017-01-09T12:00:00"//DateTime.Now.AddHours(5).ToString("yyyy-MM-dd HH:mm:ss"),
        //    });
        //    return Json(events, JsonRequestBehavior.AllowGet); // Success 
        //}
        public JsonResult GetCalendarValues()
        {
            var events = new List<Event>();
            var Sinav=db.Sinavlar.ToList();
           
            foreach (var item in Sinav)
            {
                var Dersler = db.Dersler.Where(p => p.Id == item.DersId).FirstOrDefault();
                var Sinif = db.Siniflar.Where(p => p.Id == item.SinifId).FirstOrDefault();
                DateTime SinavTarihi = Convert.ToDateTime(item.SınavTarihi);
                int startHour = Convert.ToInt32(item.Saat);
                int endHour   = Convert.ToInt32(item.Saat) + 2;
                events.Add(new Event
                {   //id: '1', resourceId: 'a', start: '2017-01-06', end: '2016-12-08', title: 'event 1'
                    id = item.Id.ToString(),
                    resourceId = item.SinifId.ToString(),
                    title = Dersler.DersAdi + " " + Dersler.Donem + ".Dönem",
                    start = SinavTarihi.AddHours(startHour).ToString("yyyy-MM-dd HH:mm:ss"),
                    end = SinavTarihi.AddHours(endHour).ToString("yyyy-MM-dd HH:mm:ss")
                   // "2017-01-09T12:00:00"//DateTime.Now.AddHours(5).ToString("yyyy-MM-dd HH:mm:ss"),
                });
            }
            
            return Json(events, JsonRequestBehavior.AllowGet); // Success 
        }
        public class resources
        {

            public string id { get; set; }            
            public string title { get; set; }           

        }
        public JsonResult GetSinif()
        {
            var resources = new List<resources>();
            var sinif = db.Siniflar.ToList();

            foreach (var item in sinif)
            {
                resources.Add(new resources
                {   
                    id = item.Id.ToString(),                  
                    title = item.SinifAdi
                });
            }

            return Json(resources, JsonRequestBehavior.AllowGet); // Success 
        }
    }
}