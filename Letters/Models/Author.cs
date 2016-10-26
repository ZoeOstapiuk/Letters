namespace Letters.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        // TODO: make a collection later
        public Letter Letter { get; set; }
    }
}
