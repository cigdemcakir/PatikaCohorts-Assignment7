using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations;

public class BookStoreDbContext:DbContext, IBookStoreDbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
    {
        
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }

    public override int SaveChanges() //temelde sadece db context altında yapılması gereken işlemlere ihtiyacımız var, override edip bookstoredbcontext üzerinden erişilebilir olsun istiyoruz 
    {
        return base.SaveChanges();
    }
}