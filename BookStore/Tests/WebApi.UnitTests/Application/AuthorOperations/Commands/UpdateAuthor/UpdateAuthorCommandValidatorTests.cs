using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class  UpdateAuthorCommandValidatorTest:IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, "Jane", "Austen")]
    [InlineData(1," "," ")]
    [InlineData(0,"Jane"," ")]
    [InlineData(1,"Ja","Au")]
    [InlineData(0,"Jan","Aus")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId,string firstNamee,string lastNamee)
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null);
        
        command.AuthorId = authorId;
        
        command.Model = new UpdateAuthorModel()
        {
            FirstName = firstNamee,
            LastName = lastNamee
        };

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
       
    // Happy path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null);
        
        command.AuthorId = 1;
        
        command.Model = new UpdateAuthorModel()
        {
            FirstName = "Jane",
            LastName = "Austin"
        };

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}