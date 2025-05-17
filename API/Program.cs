using API.Data;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    _ = opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

//AddScoped will effectively create a service avilable 
//for entirety of the request
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

//configure the http request pipeline
//order is important from here

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3000");
});
app.MapControllers();

DbInitializer.InitDb(app);

app.Run();
