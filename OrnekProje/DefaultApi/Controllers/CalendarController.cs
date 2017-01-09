using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DefaultApi.Controllers
{
    public class CalendarController : ApiController
    {
        public class Event
        {

            public string id { get; set; }
            public string resourceId { get; set; }
            public string title { get; set; }

            public bool AllDay { get; set; }

            public string start { get; set; }

            public string end { get; set; }

        }
        public HttpResponseMessage Get()
        {
            //
            // FullCalendar will keep passing the start and end values in as you navigate through the calendar pages
            //  You should therefore use these days to determine what events you should return . Ie
            //  i.e.     var events = db.Events.Where(event => event.Start > start && event.End < end);

            //
            // Below is dummy data to show you how the event object can be serialized 
            //
            var events = new List<Event>();

            events.Add(new Event
            {//id: '1', resourceId: 'a', start: '2017-01-06', end: '2016-12-08', title: 'event 1'
                id = "1",
                resourceId = "a",
                title = "My First Event",

                start = "2017-01-09T09:00:00",// DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss"),
                end = "2017-01-09T12:00:00"//DateTime.Now.AddHours(5).ToString("yyyy-MM-dd HH:mm:ss"),
            });
            return Request.CreateResponse(HttpStatusCode.OK, events, Request.GetConfiguration());

        }

        [System.Web.Http.Route("api/Calendar/getCalendarValues")]
        [System.Web.Http.HttpGet]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, VaryByParam = "none", Duration = 0)]
        public string getCalendarValues()
        {
            try
            {
                List<Event> events = new List<Event>();

                events.Add(new Event
                {
                    id = "1",
                    resourceId = "a",
                    title = "My First Event",
                    start = "2017-01-09T09:00:00",// DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss"),
                    end = "2017-01-09T12:00:00"//DateTime.Now.AddHours(5).ToString("yyyy-MM-dd HH:mm:ss"),
                });

                // return events; // Success
                string result = new JavaScriptSerializer().Serialize(events);
                return result;
                //return Request.CreateResponse(HttpStatusCode.OK, result, Request.GetConfiguration());
                //sreturn events;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }
}
