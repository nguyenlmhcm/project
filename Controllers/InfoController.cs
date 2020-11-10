using System.Linq;
using System.Web.Mvc;
using DoAn1.Models;

namespace DoAn1.Controllers
{
    public class InfoController : Controller
    {
        private CSDL db = new CSDL();

        // GET
        public ActionResult Index()
        {
            var info = db.Info.SingleOrDefault(inf => inf.Id == 1);
            
            return View(info);
        }

        public ActionResult Edit(string address, string phone, string email)
        {
            var info = db.Info.SingleOrDefault(inf => inf.Id == 1);
            
            info.Address = address;
            info.Phone = phone;
            info.Email = email;

            db.Entry(info);
            db.SaveChanges();

            return Redirect("Index");
        }
    }
}