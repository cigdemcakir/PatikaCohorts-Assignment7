using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommand
{
    private readonly IBookStoreDbContext _context;
    public CreateAuthorModel Model { get; set; }
    
    private readonly IMapper _mapper;
    
    public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName);
        
        if (author is not null) throw new InvalidOperationException($"A author with the name '{Model.FirstName} {Model.LastName}' already exists.");
        
        author = _mapper.Map<Author>(Model);
        
        _context.Authors.Add(author);
        
        _context.SaveChanges();
    }
}

public class CreateAuthorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}