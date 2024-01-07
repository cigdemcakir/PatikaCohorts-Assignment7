using FluentValidation;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
{
    public GetAuthorDetailQueryValidator()
    {
        RuleFor(x=>x.AuthorId).GreaterThan(0);
    }
}