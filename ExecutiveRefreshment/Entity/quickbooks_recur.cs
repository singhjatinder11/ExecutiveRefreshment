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
    
    public partial class quickbooks_recur
    {
        public long quickbooks_recur_id { get; set; }
        public string qb_username { get; set; }
        public string qb_action { get; set; }
        public string ident { get; set; }
        public string extra { get; set; }
        public string qbxml { get; set; }
        public Nullable<long> priority { get; set; }
        public long run_every { get; set; }
        public long recur_lasttime { get; set; }
        public System.DateTime enqueue_datetime { get; set; }
    }
}
