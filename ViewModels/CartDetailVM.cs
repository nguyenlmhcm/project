using System.Collections.Generic;
using DoAn1.Models;

namespace DoAn1.ViewModels
{
    public class CartDetailVM
    {
        public List<CartDetail> Items { get; set; }
        public Cart Cart { get; set; }
    }
}