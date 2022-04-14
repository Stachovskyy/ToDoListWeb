using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoListWeb.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var dbConnectionString = builder.Configuration.GetConnectionString("DataBaseConnectionString");                //db=database

builder.Services.AddDbContext<MainContext>(options => options.UseSqlServer(dbConnectionString));   //rejestracja dbcontextu

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());  //Mapper

builder.Services.AddScoped<ITaskRepository, TaskRepository>();    //

//app.MapGet("/", () => "Hello World!"); 
var app = builder.Build();
app.MapControllers();
app.Run();
