using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn1.Models;
using DoAn1.ViewModels;

namespace DoAn1.Controllers
{
    public class HomeController : Controller
    {
        private CSDL db = new CSDL();
        public ActionResult Index()
        {
            var data = new HomeViewModel
            {
                NewRelease = db.NewRelease.Include(release => release.Book).ToList(),
                Comming = db.Comming.Include(comming => comming.Book).ToList(),
                BestSeller = db.BestSeller.Include(seller => seller.Book).ToList(),
                Winner = db.Winner.Include(winner => winner.Book).ToList(),
                WhatsNew = db.WhatsNew.Include(whatsnew => whatsnew.Book).ToList(),
                Author = db.Author.OrderBy(author => author.Id).Take(6).ToList(),
                Info = db.Info.SingleOrDefault()
            };
            return View(data);
        }

        public ActionResult About()
        {
            var info = db.Info.SingleOrDefault();
            ViewBag.Phone = info.Phone;
            ViewBag.Email = info.Email;
            ViewBag.Address = info.Address;
            return View();
        }
        
        public ActionResult Contact()
        {
            var info = db.Info.SingleOrDefault();
            
            ViewBag.Info = info;
            ViewBag.Phone = info.Phone;
            ViewBag.Email = info.Email;
            ViewBag.Address = info.Address;
            
            return View();
        }
    }
}