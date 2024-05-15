using MovieCollectionApi.Data;
using MovieCollectionApi.Repository;
using MovieCollectionApi.Services;

public static class ServiceRegistration
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<HttpClient>();
        services.AddDbContext<MovieDbContext>(ServiceLifetime.Scoped);
        RegisterServices(services);
        RegisterRepositories(services);
    }

    private static void RegisterServices(IServiceCollection services) 
    {
        services.AddScoped<CollectionService>();
        services.AddSingleton<MovieProviderService>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
    }
}
