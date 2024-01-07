using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.GetBooks;

public class GetBooksQuery
{
    private readonly IBookStoreDbContext _dbContext; 
    
    private readonly IMapper _mapper; 
    
    public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookLists = _dbContext.Books.Include(x=>x.Genre).OrderBy(x => x.Id).ToList<Book>();

        List<BooksViewModel> viewModel = _mapper.Map<List<BooksViewModel>>(bookLists); 

        return viewModel;
    }
}

public class BooksViewModel
{
    public string Title { get; set; }
    public string Genre { get; set; }
    
    public int PageCount { get; set; }

    public string PublishDate { get; set; }   
}