using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExecutiveRefreshment.Models
{
    public class UserModel
    {
        public long id { get; set; }

        public string user_type { get; set; }
        [Required]
        [Display (Name ="First Name")]
        public string f_name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string l_name { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string user_name { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "New Password and Confirm Password Does not match")]
        public string confirm_password { get; set; }
        [Required]
        [Display(Name = "Address 1")]
        public string address { get; set; }
        
        [Display(Name = "Address 2")]
        public string address2 { get; set; }
        [Required]
        [Display(Name = "Phone No")]
        public string phone { get; set; }
         
        [Display(Name = "Zip")]
        public string zip { get; set; }
        public string place { get; set; }
        public string date { get; set; }
        public int publish { get; set; }
        public string photo { get; set; }
        public int shop_id { get; set; }
        public int num_list { get; set; }
        [Required]
        [Display(Name = "City")]
        public string city { get; set; }
        [Required]
        [Display(Name = "State")]
        public string state { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
         
        public string pin { get; set; }
        public string merchant_acct { get; set; }
        public string auth_login_id { get; set; }
        public string auth_trans_key { get; set; }
        [Required]
        [Display(Name = "Company")]
        public string company { get; set; }
        [Required]
        [Display(Name = "Contact Name")]
        public string contact_name { get; set; }
        [Required]
        [Display(Name = "Contact Position")]
        public string contact_position { get; set; }
        [Required]
        [Display(Name = "Contact Email")]
        [DataType(DataType.EmailAddress)]
        public string contact_email { get; set; }
        [Required]
        [Display(Name = "Contact Phone")]
        public string contact_phone { get; set; }
        public string qb_id { get; set; }
        public int quickbook_check { get; set; }
        public string customer_invoice { get; set; }
        public string fax { get; set; }
        public decimal fs { get; set; }
        public string master_cust { get; set; }
        public string billing_address { get; set; }
        public string billing_address2 { get; set; }
        public string ChainStoreNum { get; set; }
        public string Area { get; set; }
        public string CreditClass { get; set; }
        public string CreditLimt { get; set; }
        public string TaxAuth { get; set; }
        public string TaxStatus { get; set; }
        public string PriceList { get; set; }
        public string DateLastDelv { get; set; }
        public string HHinvoiceCopies { get; set; }
        public string Business { get; set; }
        public string Acquisition { get; set; }
        public string PurchOrder { get; set; }
        public string NextDelvDate { get; set; }
        public string quickbooks_listid { get; set; }
        public string quickbooks_editsequence { get; set; }
        public string quickbooks_errnum { get; set; }
        public string quickbooks_errmsg { get; set; }
        public string ListID { get; set; }
        public string EditSequence { get; set; }
        public long check_tax { get; set; }
        public string cust_com_l { get; set; }
        public string res_list_id { get; set; }
        public string po_no { get; set; }
    }
}