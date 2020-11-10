namespace DoAn1.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

        public string Message { get; set; }
    }
}