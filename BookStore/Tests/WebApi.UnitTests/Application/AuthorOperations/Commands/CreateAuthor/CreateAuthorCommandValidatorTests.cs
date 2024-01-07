using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","")]
        [InlineData("J","AUS")]
        [InlineData("JANE","")]
        [InlineData("JANE","AUS")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string firstName,string lastName)
        {
            // Arrange 
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
           
            command.Model = new CreateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = DateTime.Now.Date.AddYears(-40)
            };
            
            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result= validator.Validate(command);
            
            // Assert 
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReeturnError()
        {
            // Arrange 
            CreateAuthorCommand command= new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorModel(){
                FirstName = "Test Firstname",
                LastName = "Test Lastname",
                DateOfBirth = DateTime.Now.Date
            };
            // Act 
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            
            // Assert 
            result.Errors.Count().Should().BeGreaterThan(0);

        }

        // Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            
            command.Model= new CreateAuthorModel{
                FirstName = "Test Firstname",
                LastName = "Test Lastname",
                DateOfBirth = DateTime.Now.Date.AddYears(-40)
            };
            
            // Act 
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result= validator.Validate(command);
            
            // Assert 
            result.Errors.Count().Should().Be(0);
        }
    }