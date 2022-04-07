using Microsoft.EntityFrameworkCore;
using SRMSDataAccess.Models;
using SRMSRepositories.IRepositories;
using SRMSRepositories.Repositories;
using SRMSServices.IServices;
using SRMSServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Sql Server Connection though  EntityFramework
builder.Services.AddDbContext<SrmsContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));
//Dependency Injection for Repository
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();





//Dependency Injection for Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
