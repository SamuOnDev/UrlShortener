using UrlShortenerApiBackend.Models.DataModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace UrlShortenerApiBackend.Services.JWT
{
    public static class AddJwtTokenServicesExtensions
    {
        public static void AddJwtTokenServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            // Add JWT Settings
            var bindJwtSettings = new JwtSettings();
            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            // Estamos pasandole los valores que nosotros tenemos dentro de appsettings.json al servicio. 
            // De esta manera no tendremos que reintroducir el valor cada vez. 

            // Add Singleton of JWT Settings
            Services.AddSingleton(bindJwtSettings); // Singleton - Al iniciarse la aplicación se ejecuta una sola vez la instrucción “AddSingleton(bindJwtSettings)” y la instancia queda creada para el resto de vida de la aplicación.

            Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Le estamos diciendo a nuestra aplicacion que tipo de autenticacion vamos a utilizar, en este caso JWT.
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Le decimos como tiene que comprobar a los usuarios, en este caso sera utilizando igualmente JWT.    
                })
                .AddJwtBearer(options =>
                {
                    // Estas son las configuraciones que va a tener el JWT Bearer. 
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters() // configurar los parametros de validacion del toke. 
                    {
                        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey, // Utilizaremos las opciones que hemos configurado en el JSON y que antes hemos bindeado a la variable bindJwtSettings
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)), // Cogemos nuestra clave del JSON y la codificamos para que al usarla nuestro programa sea secreta.
                        ValidateIssuer = bindJwtSettings.ValidateIssuer,
                        ValidIssuer = bindJwtSettings.ValidIssuer,
                        ValidateAudience = bindJwtSettings.ValidateAudience,
                        ValidAudience = bindJwtSettings.ValidAudicence,
                        RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                        ValidateLifetime = bindJwtSettings.ValidateLifetime,
                        ClockSkew = TimeSpan.FromDays(1) // tiempo de validacion 
                    };
                });
        }
    }
}
