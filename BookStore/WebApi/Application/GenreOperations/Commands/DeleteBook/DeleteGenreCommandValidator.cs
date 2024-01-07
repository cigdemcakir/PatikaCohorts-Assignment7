using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.DeleteBook;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        // This rule validates the GenreId property of the DeleteBookCommand.
        // It ensures that the GenreId provided is greater than 0.
        // A GenreId greater than 0 indicates that it is a valid and potentially existing ID in the database.
        // This check is important to prevent attempts to delete a book with an invalid or non-existent ID,
        // which helps maintain data integrity and avoids unnecessary operations on the database.
        RuleFor(command => command.GenreId).GreaterThan(0).NotEmpty();
    }
}
