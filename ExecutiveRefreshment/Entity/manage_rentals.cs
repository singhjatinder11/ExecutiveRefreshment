//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExecutiveRefreshment.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class manage_rentals
    {
        public long id { get; set; }
        public string customer { get; set; }
        public System.DateTime invoice_date { get; set; }
        public long lease_no { get; set; }
        public System.DateTime date { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public long publish { get; set; }
        public System.DateTime date_time { get; set; }
        public string invoice_period { get; set; }
        public System.DateTime invoice_start_date { get; set; }
        public System.DateTime next_invoice { get; set; }
        public decimal tax { get; set; }
        public string check_tax { get; set; }
        public System.DateTime email_date { get; set; }
        public string po { get; set; }
    }
}
