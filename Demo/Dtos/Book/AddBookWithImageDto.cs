namespace Demo.Dtos.Book
{
    public class AddBookWithImageDto
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }
    }
}
