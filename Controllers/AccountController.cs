using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DoAn1.Models;
using DoAn1.ViewModels;

namespace DoAn1.Controllers
{
    public class AccountController : Controller
    {
        private CSDL db = new CSDL();

        // GET
        public ActionResult Index(int id)
        {
            var user = Session["Account"] as Account;
            if (user.Id != id)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var cartVM = new List<CartDetailVM>();
            
            var carts = db.Cart.Include(c => c.Account).Include(c => c.Coupon).Where(c => c.AccountId == id).ToList();
            foreach (var cart in carts)
            {
                var listCartDetail = db.CartDetail.Include(c => c.Book).Where(c => c.CartId == cart.Id).ToList();
                
                cartVM.Add(new CartDetailVM
                {
                    Cart = cart,
                    Items = listCartDetail
                });
            }
            
            
            return View(cartVM);
        }

        public ActionResult List()
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var acc = db.Account.ToList();

            return View(acc);
        }

        public ActionResult Edit(int id)
        {
            var user = Session["Account"] as Account;
            if (user.Id != id || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var acc = db.Account.SingleOrDefault(b => b.Id == id);
            
                return View("Register", acc);
        }

        public ActionResult Register()
        {
            var acc = new Account();
            return View(acc);
        }
        
        [HttpPost]
        public ActionResult SaveAccount(string name, string username, string password, string address, string phone)
        {
            if (db.Account.SingleOrDefault(acc => acc.UserName == username) != null)
            {
                // exists user
                return RedirectToAction("");
            }

            var newUser = new Account
            {
                Name = name,
                UserName = username,
                Password = password,
                Address = address,
                Phone = phone,
                IsAdmin = false
            };

            db.Account.Add(newUser);
            db.SaveChanges();

            Session["Account"] = newUser;
            return RedirectToAction("Index","Home");
        }

        public ActionResult AddAdmin(int id)
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }

            if (db.Account.SingleOrDefault(a => a.Id == id) == null)
            {
                return RedirectToAction("NotFound", "Authentication");
            }

            db.Account.SingleOrDefault(a => a.Id == id).IsAdmin = true;
            db.SaveChanges();
            
            
            return RedirectToAction("Index", "Admin");
        }
    }
}