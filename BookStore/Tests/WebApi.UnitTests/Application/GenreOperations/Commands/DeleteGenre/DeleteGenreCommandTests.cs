using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTest:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteGenreCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        DeleteGenreCommand command= new DeleteGenreCommand(_context);
        
        command.GenreId = 999;
        
        // Act & Assert
        FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be($"Genre with ID {command.GenreId} not found.");

    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
    {
        // Arrange
        DeleteGenreCommand command= new DeleteGenreCommand(_context);
        
        command.GenreId = 1;
        
        // Act
        FluentActions.Invoking(()=>command.Handle()).Invoke();
        
        // Assert
        var genre = _context.Genres.SingleOrDefault(genre=>genre.Id==command.GenreId);
        
        genre.Should().BeNull();
    }
}