//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace baitap_mvc_3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hinhanh
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public int khID { get; set; }
    
        public virtual Khachhang Khachhang { get; set; }
    }
}