
namespace Demo.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
    }
}
