using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTest:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    public DeleteAuthorCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange 
        DeleteAuthorCommand command= new DeleteAuthorCommand(_context);
        
        command.AuthorId=0;
        
        // Act & Assert 
        FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Author with ID {command.AuthorId} not found.");
            
    }
    [Fact]
    public void WhenAuthorBooksExist_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        DeleteAuthorCommand command= new DeleteAuthorCommand(_context);
        
        command.AuthorId=1;
        
        // Act & Assert 
        FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This author cannot be deleted because they have a book.");
       
    }
    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
    {
        // Arrange
        DeleteAuthorCommand command= new DeleteAuthorCommand(_context);
        
        command.AuthorId=4;
        
        // Act 
        FluentActions.Invoking(()=>command.Handle()).Invoke();
        
        // Assert 
        var author = _context.Authors.SingleOrDefault(x=>x.Id == command.AuthorId);
        author.Should().BeNull();
    }
}