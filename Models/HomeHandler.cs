namespace DoAn1.Models
{
    public class NewRelease
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
    public class Comming
    {
        public int Id { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
    public class BestSeller
    {
        public int Id { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
    public class Winner
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }

    public class WhatsNew
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}