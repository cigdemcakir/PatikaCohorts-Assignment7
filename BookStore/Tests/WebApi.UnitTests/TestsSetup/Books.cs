using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup;

public static class Books
{
    public static void AddBooks(this BookStoreDbContext context)
    {
        context.Books.AddRange(
            new Book
            {
                GenreId = 1,
                Title = "Lean Startup",
                PageCount = 200,
                PublishDate = new DateTime(2001, 06, 12)
            },
            new Book
            {
                GenreId = 2,
                Title = "Herland",
                PageCount = 250,
                PublishDate = new DateTime(2010, 05, 23)
            },
            new Book
            {
                GenreId = 2,
                Title = "Dune",
                PageCount = 540,
                PublishDate = new DateTime(2001, 12, 21)
            },
            new Book
            {
                GenreId = 2,
                Title = "Pride Prejudice",
                PageCount = 540,
                PublishDate = new DateTime(2001, 12, 21),
                AuthorId = 1
            }
        );
    }
}