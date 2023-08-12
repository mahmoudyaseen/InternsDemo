namespace Demo.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }

        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
