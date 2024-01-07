using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorQuery
{
    private readonly IBookStoreDbContext _context;
    
    private readonly IMapper _mapper;
    
    public GetAuthorQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public List<AuthorViewModel> Handle()
    {
        var author=_context.Authors.OrderBy(x=>x.Id);
        List<AuthorViewModel> viewModel= _mapper.Map<List<AuthorViewModel>>(author);
        return viewModel;
    }
}
public class AuthorViewModel
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string DateOfBirth { get; set; }

}