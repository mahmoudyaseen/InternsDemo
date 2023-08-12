using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BooksAuthors { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<BookAuthor>().ToTable("BookAuthor");
            modelBuilder.Entity<Blog>().ToTable("Blog");

            modelBuilder.Entity<Book>().Property(x => x.BookId).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Author>().Property(x => x.AuthorId).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Blog>().Property(x => x.BlogId).HasColumnName("Id").ValueGeneratedOnAdd();

            modelBuilder.Entity<Book>().Property(x => x.Title).HasMaxLength(450);
            modelBuilder.Entity<Author>().Property(x => x.Name).HasMaxLength(250);
            modelBuilder.Entity<Blog>().Property(x => x.Title).HasMaxLength(450);

            modelBuilder.Entity<Book>().HasKey(x => x.BookId);
            modelBuilder.Entity<Author>().HasKey(x => x.AuthorId);
            modelBuilder.Entity<Blog>().HasKey(x => x.BlogId);

            modelBuilder.Entity<BookAuthor>().HasKey(x => new { x.AuthorId, x.BookId });

            modelBuilder.Entity<Book>().HasIndex(b => b.Title).IsUnique();

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.AuthorBooks)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(a => a.BookAuthors)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>()
                .HasMany(a => a.Blogs)
                .WithOne(b => b.Author)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
