using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTest:IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        
        command.BookId = bookId;
        
        // Act 
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        
        var result = validator.Validate(command);
        
        // Assert 
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(null);
        
        command.BookId = 1;
        
        // Act 
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        
        var result = validator.Validate(command);
        
        // Assert 
        result.Errors.Count.Should().Be(0);
    }
}