using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommand
{
    private readonly IBookStoreDbContext _dbContext;
    public int BookId { get; set; }

    public UpdateBookModel Model { get; set; }
    
    public UpdateBookCommand(IBookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id==BookId);

        if (book is null)  throw new InvalidOperationException("Book to be updated not found!");

        book.Title = Model.Title != default ? Model.Title : book.Title;
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        
        _dbContext.SaveChanges(); 
    }

}

public class UpdateBookModel
{
    public string Title { get; set; }

    public int GenreId { get; set; }  
}