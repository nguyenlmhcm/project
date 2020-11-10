using System;

namespace DoAn1.Models
{
    public class Coupon
    {
        public int Id { get; set; }
         
        public string Code { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public int Percents { get; set; }
    }
}