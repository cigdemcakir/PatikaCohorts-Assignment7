using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteBook;

public class DeleteGenreCommand
{
    private readonly IBookStoreDbContext _dbContext;
    public int GenreId { get; set; }
    
    public DeleteGenreCommand(IBookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.FirstOrDefault(x => x.Id==GenreId);

        if (genre is null)  throw new InvalidOperationException($"Genre with ID {GenreId} not found.");

        _dbContext.Genres.Remove(genre);
        _dbContext.SaveChanges(); 
    }

}