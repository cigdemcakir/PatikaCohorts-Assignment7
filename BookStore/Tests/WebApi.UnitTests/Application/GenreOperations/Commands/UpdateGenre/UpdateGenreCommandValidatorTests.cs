using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidatorTest:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    
    public UpdateGenreCommandValidatorTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Theory]
    [InlineData(0," ")]
    [InlineData(1," ")]
    [InlineData(1,"Ka")]
    [InlineData(0,"Du")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId, string name)
    {
        // Arrange
        UpdateGenreCommand command= new UpdateGenreCommand(null);
        
        command.GenreId = genreId;
        
        command.Model = new UpdateGenreModel()
        {
            Name = name
        };
        
        // Act
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        
        var result = validator.Validate(command);
        
        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        UpdateGenreCommand command = new UpdateGenreCommand(null);
        
        command.GenreId = 1;
        
        command.Model = new UpdateGenreModel()
        {
            Name = "Test"
        };
        
        // Act
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        
        var result = validator.Validate(command);
        
        // Assert
        result.Errors.Count.Should().Be(0);
    }
}