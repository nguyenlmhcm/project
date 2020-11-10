using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DoAn1.Models;
using DoAn1.ViewModels;

namespace DoAn1.Controllers
{
    public class HomeHandlerController : Controller
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
            
            var homeHanler = new HomeHandlerVM
            {
                NewRelease = db.NewRelease.Include(b => b.Book).ToList(),
                Winner = db.Winner.Include(b => b.Book).ToList(),
                BestSeller = db.BestSeller.Include(b => b.Book).ToList(),
                WhatsNew = db.WhatsNew.Include(b => b.Book).ToList(),
                Comming = db.Comming.Include(b => b.Book).ToList(),
                Books = db.Book.ToList()
            };
            
            return View(homeHanler);
        }
        
        //New Realse
        [HttpPost]
        public ActionResult AddNewRealse(int id)
        {
            if (db.NewRelease.Count() >= 6)
            {
                return RedirectToAction("Index");
            }
            db.NewRelease.Add(new NewRelease
            {
                BookId = id
            });

            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult RemoveNewRealse(int id)
        {
            var book = db.NewRelease.SingleOrDefault(b => b.Id == id);
            db.NewRelease.Remove(book);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        
        // Winner
        [HttpPost]
        public ActionResult AddWinner(int id)
        {
            if (db.Winner.Count() >= 6)
            {
                return RedirectToAction("Index");
            }
            else
            {
                db.Winner.Add(new Winner
                {
                    BookId = id
                });
            }
            
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult RemoveWinner(int id)
        {
            var book = db.Winner.SingleOrDefault(b => b.Id == id);
            db.Winner.Remove(book);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        
        // Comming
        [HttpPost]
        public ActionResult AddComming(int id)
        {
            if (db.Comming.Count() >= 6)
            {
                return RedirectToAction("Index");
            }
            else
            {
                db.Comming.Add(new Comming
                {
                    BookId = id
                });
            }
            
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult RemoveComming(int id)
        {
            var book = db.Comming.SingleOrDefault(b => b.Id == id);
            db.Comming.Remove(book);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        
        // WhatsNew
        [HttpPost]
        public ActionResult AddWhatsNew(int id)
        {
            if (db.WhatsNew.Count() >= 6)
            {
                return RedirectToAction("Index");
            }
            else
            {
                db.WhatsNew.Add(new WhatsNew
                {
                    BookId = id
                });
            }
            
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult RemoveWhatsNew(int id)
        {
            var book = db.WhatsNew.SingleOrDefault(b => b.Id == id);
            db.WhatsNew.Remove(book);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        
        //BestSeller
        [HttpPost]
        public ActionResult AddBestSeller(int id)
        {
            if (db.BestSeller.Count() >= 6)
            {
                return RedirectToAction("Index");
            }
            else
            {
                db.BestSeller.Add(new BestSeller
                {
                    BookId = id
                });
            }
            
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult RemoveBestSeller(int id)
        {
            var book = db.BestSeller.SingleOrDefault(b => b.Id == id);
            db.BestSeller.Remove(book);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}