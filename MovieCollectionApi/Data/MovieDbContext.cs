using Microsoft.EntityFrameworkCore;
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Data;

public class MovieDbContext : DbContext
{   
    private readonly IConfiguration _config;

    public MovieDbContext(IConfiguration config)
    {
        _config = config;
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Collection> Collections { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {   
        optionsBuilder.UseSqlite(_config["db_string"]);
    }
}