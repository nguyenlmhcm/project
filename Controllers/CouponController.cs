using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DoAn1.Models;
using DoAn1.ViewModels;

namespace DoAn1.Controllers
{
    public class CouponController : Controller
    {
        private CSDL db = new CSDL();

        // GET
        public ActionResult Index()
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }

            var coupons = db.Coupon.ToList();
            
            return View(coupons);
        }
        
        [HttpPost]
        public ActionResult AddCoupon(string code, DateTime timeStart, DateTime timeEnd, int percents,string hinh)
        {
            db.Coupon.Add(new Coupon()
            {
              
                Code = code,
                TimeStart = timeStart,
                TimeEnd = timeEnd,
                Percents = percents
            });

            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            var coupon = db.Coupon.SingleOrDefault(b => b.Id == id);
            db.Coupon.Remove(coupon);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}