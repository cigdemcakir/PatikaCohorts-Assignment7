using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidatorTest:IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
    {
        // Arrange
        DeleteAuthorCommand command= new DeleteAuthorCommand(null);
        
        command.AuthorId = authorId;
        
        // Act
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        
        var result = validator.Validate(command);
        
        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        DeleteAuthorCommand command= new DeleteAuthorCommand(null);
        
        command.AuthorId = 1;
        
        // Act
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        
        var result = validator.Validate(command);
        
        // Assert
        result.Errors.Count.Should().Be(0);
    }
}
