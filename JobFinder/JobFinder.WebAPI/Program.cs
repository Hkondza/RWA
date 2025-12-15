using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Mapping;
using JobFinder.WebAPI.Repositories;
using JobFinder.WebAPI.Repositories.Interfaces;
using JobFinder.WebAPI.Services;
using JobFinder.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// AutoMapper (ISPRAVNO)
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<JobFinderProfile>();
});

// DbContext
builder.Services.AddDbContext<JobFinderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JobFinderDB")));

// Dependency Injection
builder.Services.AddScoped<IJobOfferRepository, JobOfferRepository>();
builder.Services.AddScoped<IJobOfferService, JobOfferService>();

builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
