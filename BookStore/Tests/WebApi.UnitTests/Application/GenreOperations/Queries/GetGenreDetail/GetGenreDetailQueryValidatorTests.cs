using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidatorTest:IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
    {
        // Arrange
        GetGenreDetailQuery genreDetailQuery = new GetGenreDetailQuery(null,null);
        
        genreDetailQuery.GenreId = genreId;
        
        // Act 
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(genreDetailQuery);
        
        // Assert
        result.Errors.Count.Should().BeGreaterThan(0); 
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        GetGenreDetailQuery genreDetailQuery = new GetGenreDetailQuery(null,null);
        
        genreDetailQuery.GenreId = 1;
        
        // Act 
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(genreDetailQuery);
        
        // Assert 
        result.Errors.Count.Should().Be(0);
    }
}