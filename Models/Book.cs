namespace DoAn1.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Review { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }
    }
}