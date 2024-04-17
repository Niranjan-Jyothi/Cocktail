using Cocktails.Data.Implementation;
using Cocktails.Middleware;
using Cocktails.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using NReco.Logging.File;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDataServices();
builder.Services.ConfigureServices();

//Registering http client factory
builder.Services.AddHttpClient<CocktailDataProvider>();

//Registering DB context
builder.Services.AddDbContextPool<AppDbContext>(options => 
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CocktailDbConnection") ?? ""));

// Add logging
builder.Services.AddLogging(loggingBuilder => {
    var loggingSection = builder.Configuration.GetSection("Logging");
    loggingBuilder.AddFile(loggingSection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
    options => options.WithOrigins("http://localhost:5173").AllowAnyMethod()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandler>();

app.Run();
