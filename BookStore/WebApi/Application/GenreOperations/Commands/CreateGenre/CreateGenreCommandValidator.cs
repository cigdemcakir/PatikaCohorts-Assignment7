using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand> 
{
    public CreateGenreCommandValidator()
    {
        // Validate that the Name is not empty and has a minimum length of 4 characters.
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
    }
}