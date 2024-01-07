using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTest:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteBookCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(_context);
        
        command.BookId = 0;
        
        // Act & Assert 
        FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Book with ID {command.BookId} not found.");
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(_context);
        
        command.BookId = 1;
        
        // Act 
        FluentActions.Invoking(()=>command.Handle()).Invoke();
        
        // Assert 
        var book = _context.Books.SingleOrDefault(x=>x.Id == command.BookId);
        
        book.Should().BeNull();

    }
}