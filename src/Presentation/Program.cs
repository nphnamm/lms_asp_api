using Domain.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Presentation.Middleware;
using System.Text.Json.Serialization;
using System.Text.Json;
using Domain.Enums;
using Application.Common.Extensions;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL Configuration
var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Identity Configuration
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// JWT Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

    // Map "role" claim to the default role claim type
    options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
});

// Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new FlexibleExerciseTypeEnumConverter());
    });

// Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(p => { p.EnableAnnotations(); });

// Add MediatR
builder.Services.AddMediatR(p =>
{
    p.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly);
    p.AddDiUser();
    p.AddDiLesson();
    p.AddDiCourse();
});
// Add TokenService
builder.Services.AddScoped<ITokenService, TokenService>();

// Add OTP and Email services
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFileStorageService, MinioStorageService>();

// Add cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:4200") // Add your frontend URLs here
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Add CORS middleware before other middleware
app.UseCors("AllowAllOrigins");

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Add user context middleware
app.UseUserContext();

// Seed the database
// 
if (args.Contains("--seed"))
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            // Call your seeding logic here
            await DbSeeder.SeedData(app.Services);
            Console.WriteLine("Database seeding completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
        }
    }

    // Exit the application after seeding
    return;
}

app.Use(async (context, next) =>
{
    var user = context.User;

    Console.WriteLine($"User: {user.Identity.Name}");
    foreach (var claim in user.Claims)
    {
        Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
    }

    if (user.Identity?.IsAuthenticated == true)
    {
        Console.WriteLine($"User: {user.Identity.Name}");
        foreach (var claim in user.Claims)
        {
            Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
        }
    }
    else
    {
        Console.WriteLine("User is not authenticated.");
    }
    await next();
});

app.MapControllers();
app.Run();


//hehe new deployyyyyy
// Custom converter for ExerciseType to accept both string and number
public class FlexibleExerciseTypeEnumConverter : JsonConverter<ExerciseType>
{
    public override ExerciseType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out int intValue))
        {
            return (ExerciseType)intValue;
        }
        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();
            if (Enum.TryParse<ExerciseType>(str, true, out var result))
                return result;
            if (int.TryParse(str, out int intStrValue))
                return (ExerciseType)intStrValue;
        }
        throw new JsonException($"Unable to convert value to ExerciseType: {reader.GetString()}");
    }

    public override void Write(Utf8JsonWriter writer, ExerciseType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}