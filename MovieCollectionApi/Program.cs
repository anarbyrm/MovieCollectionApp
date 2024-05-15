using FluentValidation;
using FluentValidation.AspNetCore;
using MovieCollectionApi.Data;
using MovieCollectionApi.Dto;
using MovieCollectionApi.Repository;
using MovieCollectionApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
        .AddFluentValidationAutoValidation()
        .AddValidatorsFromAssemblyContaining<CreateCollectionDto>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<HttpClient>();

builder.Services.AddDbContext<MovieDbContext>(ServiceLifetime.Scoped);
builder.Services.AddScoped<CollectionRepository>();
builder.Services.AddScoped<CollectionService>();
builder.Services.AddSingleton<MovieProviderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
