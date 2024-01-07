using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    private readonly IBookStoreDbContext _context;
    public int AuthorId { get; set; }
    public UpdateAuthorModel Model { get; set; }
    
    public UpdateAuthorCommand(IBookStoreDbContext context)
    {
        _context = context;
    }
        
    public void Handle()
    {
        var author=_context.Authors.SingleOrDefault(a => a.Id == AuthorId);
        
        if (author is null) throw new InvalidOperationException("Author to be updated not found!");
        
        author.FirstName = Model.FirstName != default ? Model.FirstName : author.FirstName;
        author.LastName = Model.LastName != default ? Model.LastName : author.LastName;

        _context.SaveChanges();
    }
}

public class UpdateAuthorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}