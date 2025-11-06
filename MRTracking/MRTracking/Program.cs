using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using MRTracking.Data;
using MRTracking.Repository;
using MRTracking.Services;
using Microsoft.EntityFrameworkCore;
using MRTracking.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MRTracking.Filters;
using System.Text.Json;
using System.Text.Json.Serialization;
using MRTracking.Converters;

// Configure Npgsql to handle timestamps properly
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<MRTrackingDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<MRTrackingDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register services and repositories
builder.Services.AddScoped<IMedicalRepresentativeRepository, MedicalRepresentativeRepository>();
builder.Services.AddScoped<IMedicalRepresentativeService, MedicalRepresentativeService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IMedicalRepresentativeVisitRepository, MedicalRepresentativeVisitRepository>();
builder.Services.AddScoped<IMedicalRepresentativeVisitService, MedicalRepresentativeVisitService>();
builder.Services.AddScoped<IScheduleVisitRepository, ScheduleVisitRepository>();
builder.Services.AddScoped<IScheduleVisitService, ScheduleVisitService>();
builder.Services.AddScoped<IMedicalStoreRepository, MedicalStoreRepository>();
builder.Services.AddScoped<IMedicalStoreService, MedicalStoreService>();
builder.Services.AddScoped<IMRGroupRepository, MRGroupRepository>();
builder.Services.AddScoped<IMRGroupService, MRGroupService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// Register controllers
builder.Services.AddControllers(options =>
{
    //options.Filters.Add<CustomAuthorizeAttribute>(); // Register as a global filter
})
.AddJsonOptions(options =>
{
    // Add the UTC DateTime converters
    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
    options.JsonSerializerOptions.Converters.Add(new NullableUtcDateTimeConverter());
});

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MR Tracking", Version = "v1" });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateIssuerSigningKey = true,
//        ValidateLifetime = true,
//        ValidIssuer = builder.Configuration["JWT.Issuer"],
//        ValidAudience = builder.Configuration["JWT.Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT.Key"]))
//    });

// Identity Configuration (Only ApplicationUser and ApplicationRole)
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<MRTrackingDBContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, MRTrackingDBContext, Guid>>()
.AddRoleStore<RoleStore<ApplicationRole, MRTrackingDBContext, Guid>>();

// Remove this line as it conflicts with the custom ApplicationUser and ApplicationRole
// builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<MRTrackingDBContext>()
//    .AddDefaultTokenProviders()
//    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Default");

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
// Enable Swagger in all environments (remove this block if you want Swagger only in Development)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("./v1/swagger.json", "MR Tracking API V1");  // Use relative path for subdirectory deployment
    c.RoutePrefix = "swagger";  // Access Swagger UI at /swagger
});

// Ensure CORS is used before authentication and authorization
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
