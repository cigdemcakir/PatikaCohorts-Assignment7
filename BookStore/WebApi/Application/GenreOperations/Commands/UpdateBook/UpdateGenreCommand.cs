using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateBook;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }
    
    private readonly IBookStoreDbContext _dbContext;
    
    public UpdateGenreCommand(IBookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
        
        if (genre is null)
        {
            throw new InvalidOperationException("Genre to be updated not found!");
        }

        if (_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
        {
            throw new InvalidOperationException("A book genre with the same name already exists.");
        }

        genre.Name = string.IsNullOrEmpty(Model.Name.ToLower()) ? genre.Name : Model.Name;
        genre.IsActive = Model.IsActive;
        _dbContext.SaveChanges();

    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}