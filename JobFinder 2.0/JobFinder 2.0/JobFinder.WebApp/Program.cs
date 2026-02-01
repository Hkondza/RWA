using BLL.Mapping;
using BLL.Repositories;
using BLL.Repositories.Interfaces;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Data;
using JobFinder.WebApp.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfileVM>();
    cfg.AddProfile<JobFinderProfile>();
});

builder.Services.AddDbContext<JobFinderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JobFinderDB"))
);



builder.Services.AddScoped<ILogRepository,LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IJobTypeRepository, JobTypeRepository>();

builder.Services.AddScoped<IJobOfferRepository, JobOfferRepository>();
builder.Services.AddScoped<IJobOfferService, JobOfferService>();

builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IFirmRepository, FirmRepository>();
builder.Services.AddScoped<IFirmService, FirmService>();

builder.Services.AddScoped<IUserFirmRepository, UserFirmRepository>();
builder.Services.AddScoped<IUserFirmService, UserFirmService>();

builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();
builder.Services.AddScoped<IWorkerService, WorkerService>();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2); // isto kao JWT
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
