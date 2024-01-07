using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        // This rule validates the BookId property of the DeleteBookCommand.
        // It ensures that the BookId provided is greater than 0.
        // A BookId greater than 0 indicates that it is a valid and potentially existing ID in the database.
        // This check is important to prevent attempts to delete a book with an invalid or non-existent ID,
        // which helps maintain data integrity and avoids unnecessary operations on the database.
        RuleFor(command => command.BookId).GreaterThan(0);
    }
}
