using MovieCollectionApi.Data;
using MovieCollectionApi.Repository;
using MovieCollectionApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<HttpClient>();

builder.Services.AddDbContext<MovieDbContext>(ServiceLifetime.Singleton);
builder.Services.AddSingleton<CollectionRepository>();
builder.Services.AddSingleton<CollectionService>();
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
