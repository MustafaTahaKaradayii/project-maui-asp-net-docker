/*
before docker
var connectionString = "Server=localhost;Port=3306;Database=SimpleNotesDb;User=root;Password=7227;";
*/

using Microsoft.EntityFrameworkCore;
using SimpleNotesApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Get connection string from environment variable or fallback
var connectionString = builder.Configuration.GetConnectionString("Default")
                       ?? "Server=localhost;Port=3306;Database=simplenotesdb;User=root;Password=7227;";

builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply Migrations Automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
    dbContext.Database.Migrate(); // This applies all migrations at startup
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();


