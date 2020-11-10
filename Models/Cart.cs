using System;

namespace DoAn1.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int TotalItem { get; set; }

        public int TotalPrice { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

        public int CouponId { get; set; }

        public Coupon Coupon { get; set; }
        public DateTime DateOrder { get; set; }
        public bool IsShiped { get; set; }
    }
}