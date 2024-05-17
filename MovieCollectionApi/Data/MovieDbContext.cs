using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Data;

public class ApplicationDbContext : IdentityDbContext
{   
    private readonly IConfiguration _config;

    public ApplicationDbContext(IConfiguration config)
    {
        _config = config;
    }

    public ApplicationDbContext(IConfiguration config, DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        _config = config;
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Collection> Collections { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {   
        optionsBuilder.UseSqlite(_config["db_string"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>()
            .Property(movie => movie.Id)
            .ValueGeneratedNever();
        
        modelBuilder.Entity<Collection>()
            .HasMany(c => c.Movies)
            .WithMany();

        modelBuilder.Entity<Collection>()
            .HasOne(c => c.User)
            .WithMany();
    }
}