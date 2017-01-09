using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrnekProje.Models
{
    public static class Menu
    {
        public static OrnekProjeEntities context;
        public static List<Sayfalar> GetMenuList(int type)
        {
            //string Role = HttpContext.Current.Session["S_UserRole"].ToString();
            //int UserRole = Convert.ToInt32(Role);
            context = new OrnekProjeEntities();
            return context.Sayfalar.ToList(); 
        }
    }
}

