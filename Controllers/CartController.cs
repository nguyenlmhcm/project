using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DoAn1.Models;
using DoAn1.ViewModels;

namespace DoAn1.Controllers
{
    public class CartController : Controller
    {
        private CSDL db = new CSDL();
        // GET
        public ActionResult Index(string coupon)
        {
            var info = db.Info.SingleOrDefault();
            ViewBag.Phone = info.Phone;
            ViewBag.Email = info.Email;
            ViewBag.Address = info.Address;
            
            var cart = Session["Cart"] as CartViewModel;

            if (cart != null)
            {
                int total = 0;
                foreach (var item in cart.Items)
                {
                    total += item.Book.Price * item.Count;
                }

                cart.Total = total;
            }

            if (coupon != null)
            {
                var cp = db.Coupon.SingleOrDefault(c => c.Code == coupon);
                if (cp != null && cart != null && cp.TimeStart < DateTime.Now && cp.TimeEnd > DateTime.Now)
                {
                    cart.Total = (cart.Total * (100 - cp.Percents)) / 100;
                    cart.Coupon = cp.Code;
                }
                else
                {
                    cart.Coupon = null;
                }
            }
            
            return View(cart);
        }
        
        public ActionResult AddToCart(int id)
        {
            var user = Session["Account"] as Account;
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            
            var book = db.Book.SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                // Not found
                return RedirectToAction("NotFound","Authentication");
            }
            
            if (Session["Cart"] is CartViewModel cart)
            {
                if (cart.Items.Exists(x => x.Book.Id == id))
                {
                    cart.Items.SingleOrDefault(i => i.Book.Id == id).Count++;
                }
                else
                {
                    cart.Items.Add(new Item { Book = book, Count = 1 });
                }
            }
            else
            {
                var newcart = new CartViewModel
                {
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Book = book,
                            Count = 1
                        }
                    },
                    Total = book.Price
                };

                Session["Cart"] = newcart;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Plus(int id)
        {
            var cart = Session["Cart"] as CartViewModel;
           
            if (cart == null) return RedirectToAction("NotFound","Authentication");
            
            foreach(var item in cart.Items)
            {
                if (item.Book.Id == id)
                    item.Count += 1;
            }

            return RedirectToAction("Index");
        }
        
        public ActionResult Minus(int id)
        {
            var cart = Session["Cart"] as CartViewModel;
            
            if (cart == null) return RedirectToAction("NotFound","Authentication");

            foreach (var item in cart.Items)
            {
                if (item.Book.Id == id)
                {
                    item.Count -= 1;
                    if (item.Count == 0)
                        return RemoveItem(id);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveItem(int id)
        {
            var cart = Session["Cart"] as CartViewModel;
            
            if (cart == null) return RedirectToAction("NotFound","Authentication");

            var item = cart.Items.Single(i=>i.Book.Id == id);
            cart.Items.Remove(item);

            return RedirectToAction("Index");
        }
        
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = Session["Cart"] as CartViewModel;

            if (cart != null)
            {
                ViewBag.Price = cart.Total;
                ViewBag.Count = cart.Items.Count;
            }
            return PartialView();
        }

        public ActionResult SaveOrder()
        {
            var user = Session["Account"] as Account;
            if (user == null)
            {
                RedirectToAction("Login","Authentication");
            }
            
            var cart = Session["Cart"] as CartViewModel;
            if (cart == null)
            {
                return RedirectToAction("Index", "Book");
            }

            var couponDefault = db.Coupon.SingleOrDefault(c => c.Code == "NULL");
            
            int couponId = couponDefault.Id;
            
            if (cart.Coupon != null)
            {
                couponId = db.Coupon.SingleOrDefault(cp => cp.Code == cart.Coupon).Id;
            }

            var newCart = new Cart
            {
                TotalItem = cart.Items.Count,
                TotalPrice = cart.Total,
                AccountId = user.Id,
                CouponId = couponId,
                DateOrder = DateTime.Today,
                IsShiped = false,
            };
            db.Cart.Add(newCart);
            db.SaveChanges();

            foreach (var item in cart.Items)
            {
                db.CartDetail.Add(new CartDetail
                {
                    BookId = item.Book.Id,
                    CartId = newCart.Id,
                    Quantity = item.Count
                });
            }
            db.SaveChanges();

            Session["Cart"] = null;
            
            // Cart for user 
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var cart = db.Cart.Include(c => c.Account).Include(c => c.Coupon).SingleOrDefault(c => c.Id == id);
            if (cart == null) return RedirectToAction("NotFound", "Authentication");
            var cartDetailVM = new CartDetailVM
            {
                Items = db.CartDetail.Include(c => c.Book).Where(c => c.CartId == id).ToList(),
                Cart = cart

            };

            ViewBag.CouponDefault = db.Coupon.SingleOrDefault(c => c.Code == "NULL").Id;
            
            return View(cartDetailVM);
        }

        public ActionResult Shiped(int id)
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            if (db.Cart.SingleOrDefault(c => c.Id == id) != null)
            {
                db.Cart.SingleOrDefault(c => c.Id == id).IsShiped = true;
                db.SaveChanges();
            }
            
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult List()
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var cart = db.Cart.Include(c => c.Coupon).Include(c => c.Account).ToList();
            
            return View(cart);
        }
    }
}