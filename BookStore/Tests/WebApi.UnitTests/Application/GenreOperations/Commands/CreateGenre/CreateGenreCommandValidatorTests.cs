using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidatorTest:IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("")]
    [InlineData("P")]
    [InlineData("Pri")]
    [InlineData("Pr")]
    [InlineData(" ")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
    {
        // Arrange
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        
        command.Model = new CreateGenreModel()
        {
            Name = name
        };

        // Act
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count().Should().BeGreaterThan(0);
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // Arrange
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        
        command.Model = new CreateGenreModel()
        {
            Name = "Test"
        };

        // Act
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count().Should().Be(0);
    }
}