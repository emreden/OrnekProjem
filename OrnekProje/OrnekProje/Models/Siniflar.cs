//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrnekProje.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Siniflar
    {
        public int Id { get; set; }
        public string SinifAdi { get; set; }
        public Nullable<short> KontejyanSayisi { get; set; }
        public Nullable<System.DateTime> OlusturmaTarihi { get; set; }
        public string OlusturanKullanici { get; set; }
    }
}