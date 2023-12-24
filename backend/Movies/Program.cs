using Microsoft.Extensions.Configuration;
using Movies;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<TMDbApiSettings>(
    builder.Configuration.GetSection("TMDbApiSettings"));

builder.Services.AddHttpClient<TMDbApiClient>();
builder.Services.AddTransient<MoviesController>();

builder.WebHost.ConfigureKestrel(serverOptions => {
    serverOptions.ListenAnyIP(8000);
});

builder.Services.AddCors(options => {
    options.AddPolicy("ReactMovies",
                      builder => builder.WithOrigins("http://localhost:3000")
                                     .AllowAnyOrigin()
                                     .AllowAnyMethod()
                                     .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapGet("/", async context => {
        await context.Response.WriteAsync("Hello Movies");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context => {
    await context.Response.WriteAsync("Hello Movies");
});

app.UseCors("ReactMovies");

app.Run();
