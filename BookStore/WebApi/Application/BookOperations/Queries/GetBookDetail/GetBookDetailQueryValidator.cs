using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        // This rule validates the BookId property of the GetBookDetailQuery.
        // It ensures that the BookId provided is greater than 0.
        // A BookId greater than 0 implies that it is a valid and potentially existing ID in the database.
        // This check is essential to avoid querying the database with an invalid or non-existent BookId.
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}