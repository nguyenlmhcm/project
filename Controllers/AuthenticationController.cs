using System;
using System.Linq;
using System.Web.Mvc;
using DoAn1.Models;

namespace DoAn1.Controllers
{
    public class AuthenticationController : Controller
    {
        private CSDL db = new CSDL();
        // GET
        public ActionResult Login()
        {
            var user = Session["Account"] as Account;
            if (user != null)
            {
                if (user.IsAdmin) return RedirectToAction("Index", "Admin");
                return RedirectToAction("Index", "Home"); //detail user
            }
            
            var info = db.Info.SingleOrDefault();
            ViewBag.Phone = info.Phone;
            ViewBag.Email = info.Email;
            ViewBag.Address = info.Address;
            
            return View();
        }

        public ActionResult Logout()
        {
            Session["Account"] = null;
            
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public ActionResult Authen(string username, string password)
        {
            var user = db.Account.SingleOrDefault(u => u.UserName == username && u.Password == password);
            if (user == null) return RedirectToAction("Login");

            Session["Account"] = user;
            
            if (user.IsAdmin) return RedirectToAction("Index", "Admin");

            var his = new History
            {
                AccountId = user.Id,
                VisitedDate = DateTime.Now
            };

            db.History.Add(his);
            db.SaveChanges();
            
            // Detail user
            return RedirectToAction("Index","Home");
        }
        
        [ChildActionOnly]
        public ActionResult LoginSummary()
        {
            var user = Session["Account"] as Account;
            return PartialView(user);
        }

        public ActionResult NotFound()
        {
            return View();
        }
        
        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}