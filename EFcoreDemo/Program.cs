using EFcoreDemo.Models.ConfigOptions;
using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.MappingProfiles;
using EFcoreDemo.Repositories;
using EFcoreDemo.Repositories.Interface;
using EFcoreDemo.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BlogService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapper(typeof(PostProfile));


builder.Services.Configure<ConfigOptions>(
    builder.Configuration.GetSection("Position"));
// AutoMapper
builder.Services.AddAutoMapper(typeof(BlogProfile));

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapDefaultControllerRoute();
app.MapStaticAssets();
    
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
