using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTest:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public UpdateGenreCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId=180;

        // Act & Assert
        FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre to be updated not found!");
     
    }

    [Fact]
    public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        
        command.GenreId = 2;
        
        command.Model = new UpdateGenreModel()
        {
            Name="Romance"
        };

        // Act & Assert
        FluentActions
            .Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("A book genre with the same name already exists.");
            
    }
  
    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
        //Arrange
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        
        UpdateGenreModel model = new UpdateGenreModel()
        {
            Name = "Valid Genre Name"
        };

        command.Model = model;
        
        command.GenreId = 2;

        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //Assert
        var genre = _context.Genres.SingleOrDefault(c => c.Id == command.GenreId);
        genre.Should().NotBeNull();
        genre.Name.Should().Be(model.Name);
    }
}