//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace demo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_BillDetail
    {
        public int id { get; set; }
        public int bill_id { get; set; }
        public int pro_id { get; set; }
        public int amount { get; set; }
        public int price { get; set; }
    
        public virtual tbl_Bill tbl_Bill { get; set; }
        public virtual tbl_Product tbl_Product { get; set; }
    }
}
