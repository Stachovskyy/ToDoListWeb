using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Entities;
using ToDoListWeb.Data;
using ToDoListWeb.Filters;
using ToDoListWeb.Settings;
using ToDoListWeb.Extensions;
using Microsoft.OpenApi.Models;
using ToDoListWeb.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(HandleExceptionFilterAttribute));
});

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<MainContext>()
    .AddDefaultTokenProviders();

var dbConnectionString = builder.Configuration.GetConnectionString("DataBaseConnectionString");

builder.Services.AddDbContext<MainContext>(options => options.UseSqlServer(dbConnectionString));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<ITaskBoardRepository, TaskBoardRepository>();

builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();

builder.Services.AddAuth(builder.Configuration.GetSection("JsonWebToken").Get<JwtSettings>());

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JsonWebToken"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT containing userid claim",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });

    var security =
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    },
                    UnresolvedReference = true
                },
                new List<string>()
            }
        };
    options.AddSecurityRequirement(security);
});

var app = builder.Build();

app.UseRouting();

app.UseAuth();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API v1");
    options.RoutePrefix = string.Empty;
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = (MainContext)scope.ServiceProvider.GetService(typeof(MainContext));
    dbContext.Database.Migrate();

    var roleManager = (RoleManager<Role>)scope.ServiceProvider.GetService(typeof(RoleManager<Role>));
    DataBaseSeeder.AddRole(roleManager, "Admin").GetAwaiter().GetResult();

    var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>));
    DataBaseSeeder.AddAdminAsync(userManager, "admin@admin.pl", "Admin", "Admin", "Admin1@").GetAwaiter().GetResult();
}

app.MapControllers();

app.Run();

