using Microsoft.EntityFrameworkCore;
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Data;

public class MovieDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Collection> Collections { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=MovieData.db");
    }
}