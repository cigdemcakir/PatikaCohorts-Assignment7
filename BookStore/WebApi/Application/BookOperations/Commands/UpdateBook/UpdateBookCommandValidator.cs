using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidator:AbstractValidator<UpdateBookCommand> 
{
    public UpdateBookCommandValidator()
    {
        // Validate that the BookId is greater than 0.
        // This ensures that the ID is valid (IDs are typically positive).
        RuleFor(command => command.BookId).GreaterThan(0);
        
        // Validate that the GenreId of the book being updated is greater than 0.
        // This check ensures that a valid GenreId is provided.
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        
        // Validate that the Title of the book is not empty and has a minimum length of 4 characters.
        // This ensures that a valid and meaningful title is provided for the book.
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
    }
}