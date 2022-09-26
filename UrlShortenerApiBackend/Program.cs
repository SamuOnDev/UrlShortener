using Microsoft.EntityFrameworkCore;
using UrlShortenerApiBackend.DataAcces;

var builder = WebApplication.CreateBuilder(args);

// SQL Connection
const string CONNECTIONNAME = "UrlShortenerDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);
builder.Services.AddDbContext<UrlShortenerDBContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
