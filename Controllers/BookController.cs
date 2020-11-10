using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn1.Models;
using DoAn1.ViewModels;

namespace DoAn1.Controllers
{
    public class BookController : Controller
    {
        private CSDL db = new CSDL();
        // GET
        public ActionResult Index(int? author, int? category, int? min, int? max, string query, int? page)
        {
            var books = db.Book.Include(book => book.Category).Include(book => book.Author).ToList();
            
            if (author != null)
            {
                books = books.Where(book => book.AuthorId == author).ToList();
            }
            
            if (category != null)
            {
                books = books.Where(book => book.CategoryId == category).ToList();
            }

            if (min == null || min < 0)
            {
                min = 0;
            }

            if (max == null)
            {
                max = int.MaxValue;
            }

            if (query != null)
            {
                books = books.Where(book => book.Name.ToLower().IndexOf(query.ToLower(), StringComparison.Ordinal) != -1).ToList();
            }
            books = books.Where(book => book.Price > min && book.Price < max).ToList();
            
            int pageNumber = page ?? 1;
            books = books.OrderBy(f => f.Id).Skip((pageNumber - 1) * 4).Take(4).ToList();
            ViewBag.Page = pageNumber;
            int bookCount = db.Book.Count();
            if (bookCount % 4 == 0)
            {
                ViewBag.Pages = bookCount / 4;
            }
            else
            {
                ViewBag.Pages = (bookCount / 4) + 1;
            }
            
            var data = new BookViewModel
            {
                Books = books,
                Authors = db.Author.ToList(),
                Categories = db.Category.ToList()
            };
            
            var info = db.Info.SingleOrDefault();
            ViewBag.Phone = info.Phone;
            ViewBag.Email = info.Email;
            ViewBag.Address = info.Address;
            
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var info = db.Info.SingleOrDefault();
            ViewBag.Phone = info.Phone;
            ViewBag.Email = info.Email;
            ViewBag.Address = info.Address;
            
            var book = db.Book.Include(b => b.Author).Include(b => b.Category).SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                return RedirectToAction("NotFound", "Authentication");
            }

            var bookDetailVm = new BookDetailViewModel
            {
                Book = book,
                Books = db.BestSeller.Include(b => b.Book).ToList()
            };
                
            return View(bookDetailVm);
        }

        public ActionResult Manage()
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            var books = db.Book.Include(b=>b.Author).Include(b => b.Category).ToList();
            
            return View(books);
        }

        public ActionResult Delete(int id)
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }
            
            if (db.Book.SingleOrDefault(b => b.Id == id) != null)
            {
                db.Book.Remove(db.Book.SingleOrDefault(b => b.Id == id));
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
            
            var book = db.Book.SingleOrDefault(b => b.Id == id);
            
            if (book != null)
            {
                var bookVM = new BookViewModel
                {
                    Book = book,
                    Authors = db.Author.ToList(),
                    Categories = db.Category.ToList()
                };
                return View("Create", bookVM);
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
            
            var book = new BookViewModel
            {
                Book = new Book(),
                Authors = db.Author.ToList(),
                Categories = db.Category.ToList()
            };

            return View(book);
        }
        
        [HttpPost]
        public ActionResult SaveBook(int? id, string name, int price, int authorId, int categoryId, string description, string review, HttpPostedFileBase image)
        {
            var user = Session["Account"] as Account;
            if (user.IsAdmin == false || user == null)
            {
                return RedirectToAction("UnAuthorized", "Authentication");
            }

            string img = null;

            if (image != null)
            {
                img = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/Books/"), img);
                image.SaveAs(path);
            }
            
            if (id != null)
            {
                var bookEdit = db.Book.SingleOrDefault(b => b.Id == id);

                bookEdit.Name = name;
                bookEdit.Price = price;
                bookEdit.Description = description;
                bookEdit.Review = review;
                bookEdit.AuthorId = authorId;
                bookEdit.CategoryId = categoryId;
                if (img != null)
                {
                    bookEdit.Image = img;
                }

                db.Entry(bookEdit);
                db.SaveChanges();
                
                return RedirectToAction("Manage");
            }
            var book = new Book
            {
                Name = name,
                Price = price,
                Description = description,
                Review = review,
                AuthorId = authorId,
                CategoryId = categoryId,
                Image = img
            };
            
            db.Book.Add(book);
            db.SaveChanges();

            return RedirectToAction("Manage");
        }
    }
}