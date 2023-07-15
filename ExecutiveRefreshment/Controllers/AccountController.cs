using ExecutiveRefreshment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExecutiveRefreshment.Entity;
using System.Web.Security;
using CaptchaMvc.HtmlHelpers;

namespace ExecutiveRefreshment.Controllers
{
    public class OrderItem
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }

        public decimal SalePrice { get; set; }
    }
    public class AccountController : Controller
    {
        ecommerce2Entities db = new ecommerce2Entities();
        // GET: Account

        public void SaveAddToCartInDB()
        {
            try
            {

           
            if(Session["Cart"] != null && Session["UserID"] !=null)
            {
                int CurrentUserID = Convert.ToInt32(Session["UserID"]);
                Cart[] cartList = (Cart[])Session["Cart"];
                if (cartList.Length > 0)
                {
                    foreach (var r in cartList) {
                        var prod = db.product.Where(a => a.id == r.ID);
                        if (prod.Any())
                        {
                            var check = db.cart.Where(a => a.product_id == r.ID && a.user_id == CurrentUserID && a.size==r.Size);
                            if (check.Any())
                            {
                                check.FirstOrDefault().quantity = r.Quantity;
                                db.SaveChanges();
                            }
                            else
                            {
                                cart c = new cart();
                                c.product_id = r.ID;
                                c.quantity = r.Quantity;
                                c.sale_price =    r.Price;
                                c.size = r.Size==null?"N/A":r.Size;
                                c.total = Convert.ToDecimal(c.quantity * r.Price);
                                c.user_id = CurrentUserID;
                                c.color = "na";
                                c.design_image = "na";
                                c.session_id = Session.SessionID.ToString();
                                db.cart.Add(c);
                                db.SaveChanges();
                            }
                        }
                    
                    }
                }
            }
            }
            catch
            {

            }
             
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            
            var query = db.user.Where(a => a.user_name == model.user_name);
            if (query.Any())
            {
                if (Common.PasswordEncryption.Base64Decode(query.FirstOrDefault().password) == model.password)
                {
                    FormsAuthentication.SetAuthCookie(model.user_name, false);
                    Session["UserName"] = model.user_name;
                    Session["UserID"] = query.FirstOrDefault().id;
                    SaveAddToCartInDB();
                    return RedirectToAction("Index","Home");
                }
                else
                {

                    ViewBag.Message = "Invalid Password";
                    ModelState.AddModelError("error", "Invalid Password");
                }
            }
            else
            {
                ViewBag.Message = "Invalid User Name";
                ModelState.AddModelError("error", "Invalid User Name");
            }
            return View();
        }
         
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserModel model)
        {

            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                  
            try { 
           // users user = new Entity.users();
            Entity.user user = new Entity.user();
                  
                user.address = model.address;
                user.address2 = model.address2==null?"": model.address2;
                user.city = model.city;
                user.company = model.company==null?"N/A":model.company;
                user.date = DateTime.Now;
                user.email = model.email;
                user.contact_email = model.contact_email;// DateTime.Now.AddYears(3).ToShortDateString();
                  user.contact_name = model.contact_name;
                user.f_name = model.f_name;
                user.contact_phone = model.contact_phone==null?"": model.contact_phone;
                user.phone = model.phone;
                user.photo = "";
                user.contact_position = model.contact_position==null?"": model.contact_position;
                user.state = Convert.ToString(Request.Form["state"]);
                user.user_type = "User";
                user.user_name = model.user_name;
                user.pin = model.pin;
                    user.zip = model.pin;
                user.password = Common.PasswordEncryption.Base64Encode(model.confirm_password);  
                user.l_name = model.l_name;
                    //--------------------------Work with Non-Nullable Value----------------------
                    user.Acquisition = "";
                    user.Area = "";
                    user.auth_login_id = "";
                    user.auth_trans_key = "";
                    user.billing_address = "";
                    user.billing_address2 = "";
                    user.Business = "";
                    user.ChainStoreNum = "";
                    user.check_tax = 0;
                    user.CreditClass = "";
                    user.CreditLimt = "";
                    user.customer_invoice = "";
                    user.cust_com_l = "";
                    user.DateLastDelv = "";
                    user.EditSequence = "";
                    user.fax = "";
                    user.fs = 0;
                    user.HHinvoiceCopies = "";
                    user.ListID = "";
                    user.master_cust = "";
                    user.merchant_acct = "";
                    user.NextDelvDate = "";
                    user.num_list = 0;
                    user.place = "";
                    user.po_no = "";
                    user.PriceList = "";
                    user.publish = 0;
                    user.PurchOrder = "";
                    user.qb_id = "";
                    user.quickbooks_editsequence = "";
                    user.quickbooks_errmsg = "";
                    user.quickbooks_errnum = "";
                    user.quickbooks_listid = "";
                    user.quickbook_check =0;
                    user.res_list_id = "";
                    user.shop_id = 0;
                    user.TaxAuth = "";
                    user.TaxStatus = "";
                   
                db.user.Add(user);
                db.SaveChanges();
                ViewBag.Success = "Success";
                    return View();
                }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                    ViewBag.ErrMessage = "Error: captcha is not valid.";
                }

            }
            else
            {
                ViewBag.ErrMessage = "captcha is not valid.";
            }

           

            return View(model);
        }   

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if(Session["UserName"] != null)
            {
                string username = Convert.ToString(Session["UserName"]);
                var query = db.users.Where(a => a.uname == username);
                if(Common.PasswordEncryption.Base64Decode(query.FirstOrDefault().pass) == model.existing_password)
                {
                    query.FirstOrDefault().pass = Common.PasswordEncryption.Base64Encode(model.confirm_password);
                    db.SaveChanges();
                    ViewBag.Success = "Success";
                }
                else
                {
                    ViewBag.Message = "Old Password Does Not Match";
                }
            }
            return View();
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
            
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult RESTLogin(LoginModel model)
        {
            MyJsonResult result = new Models.MyJsonResult();
            var query = db.user.Where(a => a.user_name == model.user_name);
            if (query.Any())
            {
                if (Common.PasswordEncryption.Base64Decode(query.FirstOrDefault().password) == model.password)
                {
                    FormsAuthentication.SetAuthCookie(model.user_name, false);
                    Session["UserName"] = model.user_name;
                    Session["UserID"] = query.FirstOrDefault().id;
                    result.Message = "Login Success";
                    result.Success = true;
                    SaveAddToCartInDB();
                    return Json(result);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {

                    result.Message = "Invalid Password";
                    result.Success = false;
                }
            }
            else
            {
                result.Message = "Invalid User Name";
                result.Success = false;
            }
            return Json(result);
        }

        public ActionResult RESTForgotPassword(ForgotPasswordModel model)
        {
            MyJsonResult result = new Models.MyJsonResult();
            var query = db.user.Where(a => a.user_name == model.user_name && a.email==model.email);
            if (query.Any())
            {
                string newPassword = Common.PasswordEncryption.GeneratePassword(8);
                query.FirstOrDefault().password = newPassword;
                string password = Common.PasswordEncryption.Base64Decode(newPassword);

                string body = "Hello " + query.FirstOrDefault().f_name + "<br> Your New Password is : " + password;
                string msg = Common.EmailService.SendEmail(model.email, "New Password", body);
                if (msg == "success")
                {
                    db.SaveChanges();
                    result.Message = "New password has been sent to email";
                    result.Success = true;
                }
                else
                {
                    result.Message = msg;
                    result.Success = false;
                }
                
            }
            else
            {
                result.Message = "Invalid User Name or Email";
                result.Success = false;
            }
            return Json(result);
        }

        public ActionResult Customer()
        {

            UserModel user = new UserModel();
            if (Session["UserID"] != null)
            {

                int CurrentUserID = Convert.ToInt32(Session["UserID"]);
          
            var query = db.user.Where(a=>a.id==CurrentUserID).FirstOrDefault();
           
            user.user_name = query.user_name;
            user.password = Common.PasswordEncryption.Base64Decode(query.password);
                user.id = query.id;
            user.phone = query.phone;
            user.pin = query.pin;
            user.f_name = query.f_name;
            user.l_name = query.l_name;
            user.email = query.email;
            user.address = query.address;
            user.address2 = query.address2;
            user.city = query.city;
            user.company = query.company;
            user.contact_email = query.contact_email;
             user.contact_phone = query.contact_phone;
             user.contact_name = query.contact_name;
            user.phone = query.phone;
            user.contact_position = query.contact_position;
            user.state = query.state;
                user.user_type = query.user_type;
                user.fax = query.fax;
                user.date = String.Format("{0:MM-dd-yyyy}", query.date);
                    }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View(user);

        }
        [HttpPost]
        public ActionResult Customer(UserModel model)
        {

             
            if (Session["UserID"] != null)
            {

                int CurrentUserID = Convert.ToInt32(Session["UserID"]);

                var user = db.user.Where(a => a.id == CurrentUserID).FirstOrDefault();

                user.user_name = model.user_name;
                
                user.id = model.id;
                user.phone = model.phone;
                user.pin = model.pin;
                user.f_name = model.f_name;
                user.l_name = model.l_name;
                user.email = model.email;
                user.address = model.address;
                user.address2 = model.address2==null?"":model.address2;
                user.city = model.city;
                user.company = model.company==null?"":model.company;
                user.contact_email = model.contact_email;
                user.contact_phone = model.contact_phone;
                user.contact_name = model.contact_name;
                user.phone = model.phone;
                user.contact_position = model.contact_position;
                user.state = model.state;
                user.user_type = model.user_type;
                user.fax = model.fax;
               // db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Success = true;
                return View(model);
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
           

        }
        public ActionResult MyOrder()
        {
            MyJsonResult result = new Models.MyJsonResult();
           
            if (Session["UserID"] != null)
            {
                long CurrentUserID = Convert.ToInt64(Session["UserID"]);

                var lst = db.final_order.Where(a => a.user_id == CurrentUserID);
            
                return Json(lst.ToList(), JsonRequestBehavior.AllowGet);
            }
            result.Success = false;
            result.Message = "Order Not Found";
            return Json(result);

        }

        public ActionResult Invoice(string orderID="")
        {
            if (Session["UserID"]!=null && !string.IsNullOrEmpty(orderID))
            {
                long CurrentUserID = Convert.ToInt64(Session["UserID"]);
                var user = db.user.Where(a => a.id == CurrentUserID);
               

                long OrderID = Convert.ToInt64(orderID);
                var order = db.final_order.Where(a => a.id == OrderID).FirstOrDefault();

                var query = (from
                             od in db.final_order_dtls


                             join p in db.product  on od.product_id equals p.id
                             where od.cart_id == OrderID
                             select new { od.id,od.quantity,p.description,p.title,od.sale_price }).ToList();
                List<OrderItem> od1 = new List<Controllers.OrderItem>();
                foreach(var r in query)
                {
                    od1.Add(new OrderItem {
                        Description=r.description,
                        ID=r.id,
                        Quantity=r.quantity,
                        SalePrice=r.sale_price,
                        Title=r.title
                    });
                }
                ViewBag.OrderItem = od1;
                ViewBag.User = user.FirstOrDefault();
                ViewBag.Order = order;
            }
            else
            {

            }
            return View();
        }
        public ActionResult MyOrderItem(long orderID)
        {
            MyJsonResult result = new Models.MyJsonResult();

            if (Session["UserID"] != null)
            {
                long CurrentUserID = Convert.ToInt64(Session["UserID"]);

                var lst = db.final_order_dtls.Where(a => a.cart_id == orderID);

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            result.Success = false;
            result.Message = "Order Item Not Found";
            return Json(result);

        }

    }
}