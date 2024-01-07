using FluentValidation;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailQueryValidator()
    {
        // This rule validates the GenreId property of the GetBookDetailQuery.
        // It ensures that the GenreId provided is greater than 0.
        // A GenreId greater than 0 implies that it is a valid and potentially existing ID in the database.
        // This check is essential to avoid querying the database with an invalid or non-existent GenreId.
        RuleFor(query => query.GenreId).GreaterThan(0);
    }
}