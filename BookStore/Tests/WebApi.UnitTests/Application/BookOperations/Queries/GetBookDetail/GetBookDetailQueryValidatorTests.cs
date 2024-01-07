using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidatorTest:IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
    {
        // Arrange
        GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(null,null);
        bookDetailQuery.BookId = bookId;
        
        // Act
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(bookDetailQuery);
        
        // Assert 
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(null,null);
        
        bookDetailQuery.BookId = 1;
        
        // Act
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        
        var result = validator.Validate(bookDetailQuery);
        
        // Assert 
        result.Errors.Count.Should().Be(0);
    }
}
