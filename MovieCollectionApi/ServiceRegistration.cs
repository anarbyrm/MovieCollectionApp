using Microsoft.AspNetCore.Identity;
using FluentValidation;
using FluentValidation.AspNetCore;
using MovieCollectionApi.Dto;
using MovieCollectionApi.Data;
using MovieCollectionApi.Repository;
using MovieCollectionApi.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<HttpClient>();
        services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);

        RegisterAuth(services);
        AddSwaggerWithOptions(services);

        RegisterServices(services);
        RegisterRepositories(services);
        RegisterIdentity(services);
        RegisterFluidValidation(services);
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

    private static void RegisterIdentity(IServiceCollection services)
    {
        services
            .AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }

    private static void RegisterFluidValidation(IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateCollectionDto>();
    }

    private static void RegisterAuth(IServiceCollection services)
    {
        services.AddAuthentication();
    }

    private static void AddSwaggerWithOptions(IServiceCollection services)
    {
        services.AddSwaggerGen(options => 
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme{
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }
}
