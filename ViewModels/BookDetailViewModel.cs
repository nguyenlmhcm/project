using System.Collections.Generic;
using DoAn1.Models;

namespace DoAn1.ViewModels
{
    public class BookDetailViewModel
    {
        public Book Book { get; set; }
        public List<BestSeller> Books { get; set; }
    }
}