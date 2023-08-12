namespace Demo.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

        public List<BookAuthor> AuthorBooks { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
