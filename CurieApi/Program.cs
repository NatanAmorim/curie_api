using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
      policy =>
      {
        policy.WithOrigins(
          // TODO replace placeholder origins when deploying
          "https://microsoft.com",
          "https://outlook.live.com"
        )
        .AllowAnyHeader()
        .WithMethods("GET", "POST", "PUT", "DELETE");
      });

  // This is for local development only
  options.AddPolicy("AllowAll",
          builder =>
          {
            builder
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
          });
});
builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
  options.UseNpgsql(
    builder.Configuration.GetConnectionString("Sql")
  )
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "Curie API",
    Description = "An example description",
    // TermsOfService = new Uri("https://example.com/terms"),
    // Contact = new OpenApiContact
    // {
    //   Name = "Example Contact",
    //   Url = new Uri("https://example.com/contact")
    // },
    // License = new OpenApiLicense
    // {
    //   Name = "Example License",
    //   Url = new Uri("https://example.com/license")
    // }
  });

  options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
  {
    Description = "Default authorization using Bearer schema (\"bearer {token}\")",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey
  });

  options.OperationFilter<SecurityRequirementsOperationFilter>();

  // This is used to generate swagger docs in XML
  // More info in: https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio-code
  String xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration.GetSection("Authentication:Schemes:Bearer:Issuer").Value!,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
          .GetBytes(builder.Configuration.GetSection("Authentication:Schemes:Bearer:Secret").Value!)
        )
      };
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Host.UseSerilog(
  (context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

// builder.Services.AddRateLimiter(options =>
// {
//   options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
// });

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options =>
    {
      options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
      options.RoutePrefix = string.Empty;
    });
  // app.UseExceptionHandler(); // TODO see how to use it
  app.UseCors("AllowAll");
}
else
{
  app.UseCors();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// app.UseRateLimiter();

app.Run();
