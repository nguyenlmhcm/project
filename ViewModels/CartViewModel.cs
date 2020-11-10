using System.Collections.Generic;
using DoAn1.Models;

namespace DoAn1.ViewModels
{
    public class CartViewModel
    {
        public List<Item> Items { get; set; }
        public int Total { get; set; }

        public string Coupon { get; set; }
    }
}