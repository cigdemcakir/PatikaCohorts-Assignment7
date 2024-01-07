using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
        context.Authors.AddRange( 
            new Author
            {
                FirstName = "Stephen",
                LastName = "King",
                DateOfBirth = new DateTime(1947, 9, 21)
            },
            new Author
            {
                FirstName = "Jane",
                LastName = "Austin",
                DateOfBirth = new DateTime(1775, 12, 16)
            },
            new Author
            {
                FirstName = "Dan",
                LastName = "Brown",
                DateOfBirth = new DateTime(1964, 6, 22)
            } ,
            new Author
            {
                FirstName = "Orhan",
                LastName = "Pamuk",
                DateOfBirth = new DateTime(1964, 6, 22)
            }       
        );
    }
}