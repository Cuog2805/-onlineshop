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
    
    public partial class Nhanvien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nhanvien()
        {
            this.Phanquyens = new HashSet<Phanquyen>();
        }
    
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TenNV { get; set; }
        public string Sdt { get; set; }
        public Nullable<System.DateTime> Ngaysinh { get; set; }
        public string Diachi { get; set; }
        public Nullable<int> IDLoaiNV { get; set; }
        public string Chucvu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phanquyen> Phanquyens { get; set; }
    }
}
