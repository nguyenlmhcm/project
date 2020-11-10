using System;

namespace DoAn1.Models
{
    public class History
    {
        public int Id { get; set; }
        public DateTime VisitedDate { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }    
    }
}