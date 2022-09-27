using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Services.JWT;
using UrlShortenerApiBackend.Services.User;

var builder = WebApplication.CreateBuilder(args);

// SQL Connection
const string CONNECTIONNAME = "UrlShortenerDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);
builder.Services.AddDbContext<UrlShortenerDBContext>(options => options.UseSqlServer(connectionString));

// JWT Autorization
builder.Services.AddJwtTokenServices(builder.Configuration);

//Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder => // El nombre puede ser el que queramos. 
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
