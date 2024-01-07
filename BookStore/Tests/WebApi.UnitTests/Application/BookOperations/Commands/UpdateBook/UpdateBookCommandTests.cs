using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandTest:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    
    public UpdateBookCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }
        
    [Fact]
    public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(_context);

        command.BookId = 999;
        
        command.Model = new UpdateBookModel()
        {
            Title = "Updated Title",
            GenreId = 2
        };

        // Act & Assert
        FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book to be updated not found!");
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(_context);
        
        command.BookId = 2;
        
        command.Model= new UpdateBookModel()
        {
            Title = "Updated Title",
            GenreId = 2
        };
           
        // Act;
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //Assert 
        var book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
        
        book.Title.Should().Be(command.Model.Title);
        
        book.GenreId.Should().Be(command.Model.GenreId);
            
    }
    
    
}