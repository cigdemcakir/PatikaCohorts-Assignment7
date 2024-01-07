using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook;


public class CreateBookCommandTests:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact] 
    public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //Arrange 
        var book = new Book()
        {
            Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", 
            PageCount = 100, 
            PublishDate = new DateTime(1990, 01, 10), 
            GenreId = 2
        };
        
        _context.Books.Add(book);
        _context.SaveChanges();

        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        command.Model = new CreateBookModel()
        {
            Title = book.Title
        };

        //Act & Assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"A book with the title '{book.Title}' already exists.");

    }
    
    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        // Arrange
        CreateBookCommand command = new CreateBookCommand(_context,_mapper);
        
        CreateBookModel model = new CreateBookModel(){
            Title="Hobbit",
            PageCount=100,
            PublishDate=new System.DateTime(2000,1,1),
            GenreId=1
        };
        command.Model=model;
      
        // Act
        FluentActions.Invoking(()=>command.Handle()).Invoke();   

        // Assert
        var book = _context.Books.SingleOrDefault(book => book.Title==model.Title);
        book.Should().NotBeNull(); 
        book.PageCount.Should().Be(model.PageCount); 
        book.PublishDate.Should().Be(model.PublishDate.Date); 
        book.GenreId.Should().Be(model.GenreId); 

    }
}