using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    
    private readonly IMapper _mapper;

    public CreateAuthorCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistAuthor_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        
        var author = new Author()
        {
            FirstName = "FirstName",
            LastName = "Lastname",
            DateOfBirth = new System.DateTime(1970, 1, 1)
        };
        
        _context.Authors.Add(author);
        _context.SaveChanges();
        
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        
        command.Model = new CreateAuthorModel()
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
            DateOfBirth = author.DateOfBirth
        };

        // Act & Assert 
        
        FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be($"A author with the name '{command.Model.FirstName} {command.Model.LastName}' already exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        // Arrange 
        
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        
        CreateAuthorModel model = new CreateAuthorModel
        {
            FirstName = "Test",
            LastName = "Test",
            DateOfBirth = System.DateTime.Now.Date.AddYears(-30)
        };
        command.Model = model;

        // Act 
        
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert 
        
        var author = _context.Authors.SingleOrDefault(x => x.FirstName == model.FirstName && x.LastName == model.LastName);
        
        author.Should().NotBeNull();
        
        author.DateOfBirth.Should().Be(model.DateOfBirth.Date);

    }
    
}