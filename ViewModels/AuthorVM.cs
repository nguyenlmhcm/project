using System.Collections.Generic;
using DoAn1.Models;

namespace DoAn1.ViewModels
{
    public class AuthorVM
    {
        public Author Author { get; set; }
        public List<Book> Books { get; set; }
    }
}