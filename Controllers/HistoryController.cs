using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DoAn1.Models;
using DoAn1.ViewModels;

namespace DoAn1.Controllers
{
    public class HistoryController : Controller
    {
        private CSDL db = new CSDL();
        // GET
        public ActionResult Index()
        {
            var user = Session["Account"] as Account;
            if (user == null || !user.IsAdmin)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var histories = new List<HistoryVM>();
            var history = db.History.Include(h => h.Account).ToList();

            foreach (var his in history)
            {
                if (histories.Exists(h =>
                    h.Username == his.Account.UserName && h.Date.Day == his.VisitedDate.Day))
                {
                    histories.SingleOrDefault(h =>
                        h.Username == his.Account.UserName && h.Date.Day == his.VisitedDate.Day).Times += 1;
                }
                else
                {
                    var newHis = new HistoryVM
                    {
                        Name = his.Account.Name,
                        Username = his.Account.UserName,
                        Date = his.VisitedDate,
                        Times = 1
                    };
                    histories.Add(newHis);
                }
            }

            return View(histories);
        }
    }
}