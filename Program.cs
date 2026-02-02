using Microsoft.EntityFrameworkCore;
using ProbarGiladassss.Data.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<TestingContext>(options =>
    options.UseSqlite("Data Source=testing.db"));


var app = builder.Build();

app.MapControllers();

app.Run();
