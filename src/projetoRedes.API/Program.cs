using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using projetoRedes.API.Filters;
using projetoRedes.API.Token;
using projetoRedes.Application;
using projetoRedes.Domain.Security;
using projetoRedes.Infrastructure;
using projetoRedes.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextValue>();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    if (Environment.GetEnvironmentVariable("USE_DOCKER_DB") == "true")
    {
        var dbServer = builder.Configuration["DB_SERVER"];
        var dbDatabase = builder.Configuration["DB_NAME"];
        var dbUser = builder.Configuration["DB_USER"];
        var dbPassword = builder.Configuration["DB_PASSWORD"];
        var dbPort = builder.Configuration["DB_PORT"];

        var connectionString = $"Server={dbServer};Port={dbPort};Database={dbDatabase};User Id={dbUser};Password={dbPassword};";

        var serverVersion = ServerVersion.AutoDetect(connectionString);
        
        options.UseMySql(connectionString, serverVersion, mySqlOptions =>
        {
        });
    }

    else
    {
        var connectionString = builder.Configuration.GetConnectionString("Connection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
});

builder.Services.AddControllers();

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddHttpContextAccessor();

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
