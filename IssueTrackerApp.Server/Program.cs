using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using IssueTrackerApp.Server.Infrastructure;
using IssueTrackerApp.Server.Application.Issues.Commands;
using IssueTrackerApp.Server.Application.Issues;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
    builder => builder
    .WithOrigins("https://localhost:7196")
    .WithOrigins("https://localhost:14201")
    .AllowAnyHeader()
    .AllowAnyMethod());
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;
builder.Services.AddDbContext<IssueDbContext>(opt => opt.UseSqlite(
    configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddScoped<IValidator<CreateIssueCommand>, CreateIssueCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateIssueCommand>, UpdateIssueCommandValidator>();

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
