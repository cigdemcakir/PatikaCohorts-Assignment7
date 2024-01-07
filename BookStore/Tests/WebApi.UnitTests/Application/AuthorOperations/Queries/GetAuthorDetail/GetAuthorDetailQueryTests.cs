using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryTest:IClassFixture<CommonTestFixture>
{ 
    private readonly BookStoreDbContext _context;
    
    private readonly IMapper _mapper;
    
    public GetAuthorDetailQueryTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAuthorNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        GetAuthorDetailQuery authorDetailQuery = new GetAuthorDetailQuery(_context,_mapper);
        
        authorDetailQuery.AuthorId = 999;
        
        // Act & Assert
        FluentActions
            .Invoking(()=>authorDetailQuery.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Author with ID {authorDetailQuery.AuthorId} not found.");
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeReturn()
    {
        // Arrange
        GetAuthorDetailQuery authorDetailQuery = new GetAuthorDetailQuery(_context,_mapper);
        
        authorDetailQuery.AuthorId =1;
        
        // Act 
        FluentActions.Invoking(()=>authorDetailQuery.Handle()).Invoke();
        
        // Assert 
        var author = _context.Authors.SingleOrDefault(author=>author.Id==authorDetailQuery.AuthorId);
        author.Should().NotBeNull();
    }
}