using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryTest:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    
    public GetBookDetailQueryTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
       
    [Fact]
    public void WhenBookNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(_context,_mapper);
        
        bookDetailQuery.BookId = 999;
        
        // Act & Assert 
        FluentActions
            .Invoking(()=>bookDetailQuery.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Book with ID {bookDetailQuery.BookId} not found.");
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeReturn()
    {
        // Arrange
        GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(_context,_mapper);
        
        bookDetailQuery.BookId =3;
        
        // Act
        FluentActions.Invoking(()=>bookDetailQuery.Handle()).Invoke();
        
        // Assert 
        var book = _context.Books.SingleOrDefault(book=>book.Id==bookDetailQuery.BookId);
        book.Should().NotBeNull();
    }
}