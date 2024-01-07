using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    private readonly IBookStoreDbContext _context;
    public int AuthorId { get; set; }
    
    public DeleteAuthorCommand(IBookStoreDbContext context)
    {
        _context = context;
    }
    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
        
        if (author is null) throw new InvalidOperationException($"Author with ID {AuthorId} not found.");
        
        if(_context.Books.Any(x => x.AuthorId == author.Id)) throw new InvalidOperationException("This author cannot be deleted because they have a book.");
            
        _context.Authors.Remove(author);
        
        _context.SaveChanges();
    }
}