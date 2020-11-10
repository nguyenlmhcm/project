using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DoAn1.Models;
using System.Data.Entity;


namespace DoAn1.Controllers
{
    public class AdminController : Controller
    {
        private CSDL db = new CSDL();
        // GET
        public ActionResult Index()
        {
            var user = Session["Account"] as Account;
            if (user == null || user.IsAdmin == false)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }

            ViewBag.Logged = user.Name;
            ViewBag.Visited = db.History.Count(c => c.VisitedDate.Day == DateTime.Now.Day);

            var cart = db.Cart.Include(c => c.Account).Include(c => c.Coupon).Where(c => c.IsShiped == false).ToList();
            
            return View(cart);
        }

        public ActionResult ApiCart()
        {
            List<int> data = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                data.Add(db.Cart.Count(c => c.DateOrder.Month == i));
            }
            
            return Json(new {data}, JsonRequestBehavior.AllowGet );
        }
        
        public ActionResult ApiTotal()
        {
            List<int> data = new List<int>();
            
            for (int i = 1; i <= 12; i++)
            {
                int total = 0;
                var cartMonth = db.Cart.Where(cart => cart.DateOrder.Month == i).ToList();
                foreach (var cart in cartMonth)
                {
                    total += cart.TotalPrice;
                }
                data.Add(total);
            }
            
            return Json(new {data}, JsonRequestBehavior.AllowGet );
        }
    }
}