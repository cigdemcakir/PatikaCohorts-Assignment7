using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidatorTest:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    
    public UpdateBookCommandValidatorTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }
    
    [Theory]
    [InlineData(0,"Lor",1)]
    [InlineData(0,"Lord",0)]
    [InlineData(0,"",0)]
    [InlineData(1,"",1)]
    [InlineData(1,"",0)]
    [InlineData(1," ",1)]
    [InlineData(1,"Lord of The Rings",0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId,string title,int genreId)
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(_context);
        
        command.BookId = bookId;
        
        command.Model = new UpdateBookModel()
        {
            Title = title,
            GenreId = genreId,
        };
        
        // Act 
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        
        var result = validator.Validate(command);
        
        // Assert 
        result.Errors.Count.Should().BeGreaterThan(0);

    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldBeUpdated()
    {
        UpdateBookCommand command = new UpdateBookCommand(null);
        
        command.BookId = 1; 
        
        command.Model = new UpdateBookModel()
        {
            Title="Test Book",
            GenreId=1,
        };
        
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}