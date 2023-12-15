using MagicVilla_VillaAPI;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers(options =>
{
    //options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" + 
                      "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                      "Example: \"Bearer 12345asdf\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Magic Villa V1",
        Description = "API to manage Villa V1",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Dotnetmasterry",
            Url = new Uri("https://example.com/licence/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/licence")
        }
    });
    opt.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "Magic Villa V2",
        Description = "API to manage Villa V2",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Dotnetmasterry",
            Url = new Uri("https://example.com/licence/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/licence")
        }
    });
    opt.SwaggerDoc("v3", new OpenApiInfo
    {
        Version = "v3",
        Title = "Magic Villa V3",
        Description = "API to manage Villa V3",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Dotnetmasterry",
            Url = new Uri("https://example.com/licence/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/licence")
        }
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic Villa V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Magic Villa V2");
        options.SwaggerEndpoint("/swagger/v3/swagger.json", "Magic Villa V3");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
