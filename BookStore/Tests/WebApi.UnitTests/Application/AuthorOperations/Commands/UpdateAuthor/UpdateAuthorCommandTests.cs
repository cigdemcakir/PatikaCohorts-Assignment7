using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class   UpdateAuthorCommandTest:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public UpdateAuthorCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        UpdateAuthorCommand command= new UpdateAuthorCommand(_context);
        command.AuthorId=0;

        // Act & Assert 
        FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author to be updated not found!");

    }


    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeUpdate()
    {
        // Arrange 
        UpdateAuthorCommand command= new UpdateAuthorCommand(_context);
        
        command.AuthorId=1;
        
        command.Model = new UpdateAuthorModel()
        {
            FirstName = "Updated FirstName",
            LastName = "Updated LastName",
        };

        // Act 
        FluentActions.Invoking(()=>command.Handle()).Invoke();

        // Assert
        var author = _context.Authors.SingleOrDefault(author=>author.Id==command.AuthorId);
        
        author.FirstName.Should().Be(command.Model.FirstName);
        author.LastName.Should().Be(command.Model.LastName);

    }
}
