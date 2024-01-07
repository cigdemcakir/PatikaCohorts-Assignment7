using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryTest : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGenreNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        GetGenreDetailQuery genreDetailQuery = new GetGenreDetailQuery(_context, _mapper);
        genreDetailQuery.GenreId = 999;
        
        // Act & Assert 
        FluentActions
            .Invoking(() => genreDetailQuery.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"Genre with ID {genreDetailQuery.GenreId} not found.");
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeReturn()
    {
        // Arrange
        GetGenreDetailQuery genreDetailQuery = new GetGenreDetailQuery(_context, _mapper);
        
        genreDetailQuery.GenreId = 1;
        
        // Act 
        FluentActions.Invoking(() => genreDetailQuery.Handle()).Invoke();

        // Assert
        var genre = _context.Genres.SingleOrDefault(genre => genre.Id == genreDetailQuery.GenreId);
        
        genre.Should().NotBeNull();
    }
}