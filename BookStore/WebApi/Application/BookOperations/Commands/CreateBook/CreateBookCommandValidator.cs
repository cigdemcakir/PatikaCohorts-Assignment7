using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand> 
{
    public CreateBookCommandValidator()
    {
        // Validate that the GenreId is greater than 0.
        // This ensures the GenreId provided is a positive integer, implying it's a valid reference to an existing genre.
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        
        // Validate that the PageCount is greater than 0.
        // This check ensures that the page count is a positive number, reflecting a realistic and valid page count for a book.
        RuleFor(command => command.Model.PageCount).GreaterThan(0);
        
        // Validate that the PublishDate is not empty and is a date less than the current date.
        // This ensures that the publish date is not only provided but also set in the past, which is logical for book publishing.
        RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
        
        // Validate that the Title is not empty and has a minimum length of 4 characters.
        // This check ensures that a meaningful and sufficiently descriptive title is provided for the book.
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
    }
}