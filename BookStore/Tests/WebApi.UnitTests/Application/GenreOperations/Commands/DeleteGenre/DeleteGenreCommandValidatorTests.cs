using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteBook;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTest:IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
    {
        // Arrange
        DeleteGenreCommand command= new DeleteGenreCommand(null);
        
        command.GenreId = genreId;
        
        // Act
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        
        var result = validator.Validate(command);
        
        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        DeleteGenreCommand command= new DeleteGenreCommand(null);
        
        command.GenreId = 1;
        
        // Act
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        
        var result = validator.Validate(command);
        
        // Assert
        result.Errors.Count.Should().Be(0);
    }
}