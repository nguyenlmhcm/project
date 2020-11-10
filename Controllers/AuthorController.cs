using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn1.Models;
using DoAn1.ViewModels;

namespace DoAn1.Controllers
{
    public class AuthorController : Controller
    {
        private CSDL db = new CSDL();

        // GET
        public ActionResult Index()
        {
            var info = db.Info.SingleOrDefault();
            ViewBag.Phone = info.Phone;
            ViewBag.Email = info.Email;
            ViewBag.Address = info.Address;
            
            var authors = db.Author.ToList();
            
            return View(authors);
        }

        public ActionResult Details(int id)
        {
            var info = db.Info.SingleOrDefault();
            ViewBag.Phone = info.Phone;
            ViewBag.Email = info.Email;
            ViewBag.Address = info.Address;

            var books = db.Book.Where(b => b.AuthorId == id).ToList();
            
            var authorVM = new AuthorVM
            {
                Author = db.Author.SingleOrDefault(au => au.Id == id),
                Books = books.OrderBy(b => b.Id).Take(6).ToList()
            };
            
            return View(authorVM);
        }

        public ActionResult Manage()
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var authors = db.Author.ToList();
            
            return View(authors);
        }

        public ActionResult Delete(int id)
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            if (db.Author.SingleOrDefault(b => b.Id == id) != null)
            {
                db.Author.Remove(db.Author.SingleOrDefault(b => b.Id == id));
                db.SaveChanges();
            }
            
            return RedirectToAction("Manage");
        }
        
        public ActionResult Edit(int id)
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var author = db.Author.SingleOrDefault(b => b.Id == id);
            
            if (author != null)
            {
                return View("Create", author);
            }
            
            return RedirectToAction("Manage");
        }
        
        public ActionResult Create()
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var author = new Author();

            return View(author);
        }
        
        [HttpPost]
        public ActionResult Save(int? id, string name, HttpPostedFileBase image)
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            string img = Path.GetFileName(image.FileName);
            string path = Path.Combine(Server.MapPath("~/Images/Authors/"), img);
            image.SaveAs(path);

            if (id != null)
            {
                var authorEdit = db.Author.SingleOrDefault(a => a.Id == id);
                authorEdit.Name = name;
                authorEdit.Image = img;
                
                db.SaveChanges();

                return RedirectToAction("Manage");
            }
            
            var author = new Author
            {
                Name = name,
                Image = img
            };
            db.Author.Add(author);
            db.SaveChanges();

            return RedirectToAction("Manage");
        }
    }
}