using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context =
               new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if (context.Books.Any())
            {
                return;
            }
            
            context.Authors.AddRange( 
                new Author
                {
                    FirstName = "Stephen",
                    LastName = "King",
                    DateOfBirth = new DateTime(1947, 9, 21)
                },
                new Author
                {
                    FirstName = "Jane",
                    LastName = "Austin",
                    DateOfBirth = new DateTime(1775, 12, 16)
                },
                new Author
                {
                    FirstName = "Dan",
                    LastName = "Brown",
                    DateOfBirth = new DateTime(1964, 6, 22)
                }                   
            );
            
            context.Genres.AddRange(
                new Genre
                {
                    Name = "Personal Growth"
                },
                new Genre
                {
                    Name = "Science Fiction"
                }, 
                new Genre
                {
                    Name = "Romance"
                }
            );
            
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
                }
            );

            context.SaveChanges();

        }
    }
}