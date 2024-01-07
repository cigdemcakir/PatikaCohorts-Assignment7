using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateBook;

public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand> 
{
    public UpdateGenreCommandValidator()
    {
        // Validate that the Title of the book is not empty and has a minimum length of 4 characters.
        // This ensures that a valid and meaningful title is provided for the book.
        RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty); //bu kuralı when ile bir koşula bağladık.
    }
}