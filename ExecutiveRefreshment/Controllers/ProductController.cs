using ExecutiveRefreshment.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ExecutiveRefreshment.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ExecutiveRefreshment.Controllers
{
    public class ProductController : Controller
    {
        ecommerce2Entities db = new ecommerce2Entities();
        // GET: Product
        public ActionResult Index(string id = "", string title = "", string catid = "",string q="",string sort="", int? Page_No=1)
        {
            int serviceid = 0;
            
            if (!string.IsNullOrEmpty(id))
            {
                 serviceid = Convert.ToInt32(id);
                var query = db.category.Where(a => a.service_id == serviceid).ToList();
                ViewBag.CategoryList = query;
            }
            else
                ViewBag.CategoryList = null;

            ViewBag.ID = id;
            ViewBag.ProductTitle = title;
            ViewBag.CatID = catid;
            var product = db.product.Where(a => a.service_id == serviceid).OrderBy(a=>a.title).AsEnumerable();


            if (!string.IsNullOrEmpty(catid))
            {
                int catID = Convert.ToInt32(catid);
                ViewBag.CategoryName = db.category.First(a => a.id == catID).name;
            }
            else
                ViewBag.CategoryName = db.category.Where(a => a.service_id == serviceid).FirstOrDefault().name;


            if (!string.IsNullOrEmpty(catid))
            {
                 
                product = product.Where(a => a.cat_id == catid);
            }
            if (!string.IsNullOrEmpty(q))
            {
                
                product = product.Where(a => a.title.ToLower().Contains(q.ToLower()) || a.description.ToLower().Contains(q.ToLower()));
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "PHF")
                    product = product.OrderByDescending(a => a.price);
                if (sort == "PLF")
                    product = product.OrderBy(a => a.price);
                if (sort == "ASC")
                    product = product.OrderBy(a => a.title);
                if (sort == "DESC")
                    product = product.OrderByDescending(a => a.title);
            }
            int Size_Of_Page = 15;
            int No_Of_Page = (Page_No ?? 1);
            return View(product.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        public ActionResult Cart(string id = "", string title = "", string catid = "", string q = "", string sort = "", string pid="")
        {
            int serviceid = 0;

            if (!string.IsNullOrEmpty(id))
            {
                serviceid = Convert.ToInt32(id);
                var query = db.category.Where(a => a.service_id == serviceid).ToList();
                ViewBag.CategoryList = query;
               
            }
            else
                ViewBag.CategoryList = null;

            ViewBag.ID = id;
            ViewBag.ProductTitle = title;
            ViewBag.CatID = catid;

            if (!string.IsNullOrEmpty(catid))
            {
                int catID = Convert.ToInt32(catid);
                ViewBag.CategoryName = db.category.First(a => a.id == catID).name;
            }
            else
                ViewBag.CategoryName = db.category.Where(a => a.service_id == serviceid).FirstOrDefault().name;

            var product = db.product.Where(a => a.service_id == serviceid).AsEnumerable();
            if (!string.IsNullOrEmpty(catid))
            {

                product = product.Where(a => a.cat_id == catid);
            }
            if (!string.IsNullOrEmpty(q))
            {
                product = product.Where(a => a.title.ToLower().Contains(q.ToLower()) || a.description.ToLower().Contains(q.ToLower()));
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "PHF")
                    product = product.OrderByDescending(a => a.price);
                if (sort == "PLF")
                    product = product.OrderBy(a => a.price);
                if (sort == "ASC")
                    product = product.OrderBy(a => a.title);
                if (sort == "DESC")
                    product = product.OrderByDescending(a => a.title);
            }
            if (!string.IsNullOrEmpty(pid))
            {
                int productId = Convert.ToInt32(pid);
                ViewBag.Product = product.Where(a => a.id == productId).FirstOrDefault();
            }

                return View(product);
        }

        public ActionResult MyCart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(Cart[] cart)
        {
            MyJsonResult result = new Models.MyJsonResult();
            if (Session["UserID"] != null)
            {
                int CurrentUserID = Convert.ToInt32(Session["UserID"]);
                foreach (var r in cart)
                {
                    var prod = db.product.Where(a => a.id == r.ID);
                    if (prod.Any())
                    {
                        var check = db.cart.Where(a => a.product_id == r.ID && a.user_id == CurrentUserID && a.size == r.Size);
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
                            c.sale_price = r.Price;
                            c.size = r.Size == null ? "N/A" : r.Size;
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
                result.Success = true;
                result.Message = "Added Successfully";
            }
            else
            {
                result.Success = true;
                result.Message = "Added Successfully";
                Session["Cart"] = cart;
            }
            return Json(result);

        }
        [HttpPost]
        public ActionResult UpdateCart(int id,long quantity)
        {
            MyJsonResult result = new Models.MyJsonResult();
             
            if (Session["UserID"] != null)
            {
                int CurrentUserID = Convert.ToInt32(Session["UserID"]);
                var query = db.cart.Where(a => a.product_id == id && a.user_id == CurrentUserID);
                if (query.Any())
                {
                    query.FirstOrDefault().quantity = quantity;
                    db.SaveChanges();
                }
                result.Success = true;
                result.Message = "Updated Successfully";
            }
            else
            {
                result.Success = false;
                result.Message = "User was not loged in";
            }
            return Json(result);

        }
        [HttpPost]
        public ActionResult RemoveCart(Cart cart,int id)
        {
            MyJsonResult result = new Models.MyJsonResult();
            
            if (Session["UserID"] != null)
            {
                int CurrentUserID = Convert.ToInt32(Session["UserID"]);
                var query = db.cart.Where(a => a.product_id == id && a.user_id == CurrentUserID);
                foreach(var d in query)
                {
                    db.cart.Remove(d);
                   
                }
                db.SaveChanges();
                result.Success = true;
                result.Message = "Deleted Successfully";
            }
            else
            {
              Session["Cart"] = cart;
                result.Success = false;
                result.Message = "User was not loged in";
            }
            return Json(result);

        }
        public ActionResult GetCartList()
        {
            MyJsonResult result = new Models.MyJsonResult();

            if (Session["UserID"] != null)
            {
                var lst = new List<Cart>();
                int CurrentUserID = Convert.ToInt32(Session["UserID"]);
                var query = (from c in db.cart
                            join p in db.product
          on c.product_id equals p.id
                            where c.user_id == CurrentUserID
                            select new { c, p }).ToList();
                foreach (var r in query)
                {
                    lst.Add(new Cart
                    {
                        Description=r.p.description,
                        ID=r.c.product_id,
                        Price=r.c.sale_price,
                        Quantity=Convert.ToInt32(r.c.quantity),
                        Size=Convert.ToString(r.c.size),
                        Title=r.p.title
                    });

                }
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
           else if (Session["Cart"] != null)
            {
                Cart[] cart = (Cart[])Session["Cart"];
                return Json(cart, JsonRequestBehavior.AllowGet);
            }
            
            return Json(result);

        }


        [HttpPost]
        public ActionResult CheckOut()
        {
            MyJsonResult result = new Models.MyJsonResult();
            if (Session["UserID"] != null)
            {
                int CurrentUserID = Convert.ToInt32(Session["UserID"]);

                var query = (from c in db.cart
                             join p in db.product on c.product_id equals p.id
                             where c.user_id == CurrentUserID
                             select new { c, p }).ToList();
                final_order order = new final_order();
                long MaxOrderID = 0;
                if (query.Any())
                {
                    var totalPrice = query.Sum(a => (a.c.quantity * a.c.sale_price));
                    order.comment = "";
                    order.date_time = DateTime.Now;
                    order.delievery_status = "Under Processing";
                    order.EditSequence = "";
                    order.from_customer = 0;
                    order.fs = 0;
                    order.get_id_r = 0;
                    order.invoice = 0;
                    order.order_date = DateTime.Now;
                    order.order_id = 0;
                    order.order_updated = 0;
                    order.paid_on = "";
                    order.payment_status = "0";
                    order.po = "";
                    order.qb_o_id = "";
                    order.quickbook_invoice_check = 0;
                    order.quickbook_payment_check = 0;
                    order.rental_check = 0;
                    order.rental_from = DateTime.Now;
                    order.rental_to = DateTime.Now;
                    order.send = 0;
                    order.sign = "";
                    order.tax = 0;
                    order.total_amount = totalPrice;
                    order.TxnId = "";
                    order.user_id = CurrentUserID;
                    db.final_order.Add(order);
                    db.SaveChanges();
                    MaxOrderID = db.final_order.Where(a => a.user_id == CurrentUserID).Max(a => a.id);
                     
                }
                
                foreach (var prod in query)
                {
                    final_order_dtls fo = new final_order_dtls();
                    fo.cart_id = MaxOrderID;
                    fo.category = Convert.ToInt64(prod.p.cat_id);
                    fo.color = prod.c.color;
                    fo.design_image = "";
                    fo.EditSequence = "0";
                    fo.left_qty = 0;
                    fo.minus_qty = 0;
                    fo.original = 1;
                    fo.pending_qty = 0;
                    fo.price_update = 0;
                    fo.product_id = prod.p.id;
                    fo.quantity = prod.c.quantity;
                    fo.sale_price = prod.c.sale_price;
                    fo.service = prod.p.service_id;
                    fo.size = prod.c.size;
                    fo.total = prod.c.total;
                    fo.TxnLineId = "0";

                    db.final_order_dtls.Add(fo);
                    db.SaveChanges();
                    //Updating cart to quantity zero means processed
                    var updatecart = db.cart.First(a => a.id == prod.c.id);
                    db.cart.Remove(updatecart);
                    db.SaveChanges();
                    
                }
                result.Success = true;
                result.Message = "Order Proccessed Successfully";
                return Json(result);
            }
                result.Success = false;
                result.Message = "Cart Empty Found !!!";
            
            return Json(result);

        }

       
    }
}