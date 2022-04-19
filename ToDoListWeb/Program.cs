using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Data;
using ToDoListWeb.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddControllersWithViews(options =>                    //zachowanie kontrollerów
{
    options.Filters.Add(typeof(HandleExceptionFilterAttribute));   //jakikolwiek exception to moj filter sie wywola
});

var dbConnectionString = builder.Configuration.GetConnectionString("DataBaseConnectionString");                //db=database

builder.Services.AddDbContext<MainContext>(options => options.UseSqlServer(dbConnectionString));   //rejestracja dbcontextu

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());  //Mapper

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<ITaskBoardRepository, TaskBoardRepository>();

builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
