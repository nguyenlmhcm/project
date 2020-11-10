using System.Collections.Generic;
using DoAn1.Models;

namespace DoAn1.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
    }
}