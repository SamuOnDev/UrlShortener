using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Services.JWT;
using UrlShortenerApiBackend.Services.UserUrlListService;

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

builder.Services.AddScoped<IUserUrlListService, UserUrlListService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    //We define the security for authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme // Especificamos el tipo de autenticacion es de tipo "Beaer"
    {
        // Documentamos para que Swagger sepa que esquema de autorizacion tenemos
        Name = "Authorization",
        Type = SecuritySchemeType.Http, // Tipo de esquema o por donde viaja
        Scheme = "Bearer", // Esquema que utiliza nuestra autenticacion
        BearerFormat = "JWT", // Tipo de formato de nuestro Bearer token
        In = ParameterLocation.Header, // Donde va el Bearer token, en este caso en la cabecera
        Description = "JWT Authorization Header using Bearer Scheme" // Descripcion para Swagger
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement // Añadimos el requerimiento 
    {
        {
            new OpenApiSecurityScheme // Especificamos el esquema de seguridad
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
