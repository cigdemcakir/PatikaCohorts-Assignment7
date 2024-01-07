using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailValidatorTest:IClassFixture<CommonTestFixture>
{ 
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
    {
        // Arrange
        GetAuthorDetailQuery authorDetailQuery = new GetAuthorDetailQuery(null,null);
        
        authorDetailQuery.AuthorId = authorId;
        
        // Act 
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        
        var result = validator.Validate(authorDetailQuery);
        
        // Assert 
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        GetAuthorDetailQuery authorDetailQuery = new GetAuthorDetailQuery(null,null);
        
        authorDetailQuery.AuthorId = 1;
        
        // Act 
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        
        var result = validator.Validate(authorDetailQuery);
        
        // Assert 
        result.Errors.Count.Should().Be(0);
    }
}