using FluentValidation;
using FluentValidation.AspNetCore;
using MovieCollectionApi.Dto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
        .AddFluentValidationAutoValidation()
        .AddValidatorsFromAssemblyContaining<CreateCollectionDto>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application internal services
builder.Services.RegisterApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
