using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SpaServices.Extensions;
using Microsoft.EntityFrameworkCore;
using QuizGame.Application.Interfaces;
using QuizGame.Infrastructure.Data;
using QuizGame.Infrastructure.Services;
using QuizGame.API.Hubs;
using QuizGame.API.Services;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SignalR
builder.Services.AddSignalR();

// Add SPA static files
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot";
});

// Add JWT Authentication
var jwtSecretKey = builder.Configuration["Jwt:SecretKey"];
if (string.IsNullOrEmpty(jwtSecretKey))
{
    throw new InvalidOperationException("JWT SecretKey must be configured in environment variables");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecretKey)),
            ClockSkew = TimeSpan.Zero // No tolerance for expired tokens
        };

        // Configure SignalR to accept JWT from query string
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/gameHub"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });

// Add Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GM", policy => policy.RequireRole("GM"));
    options.AddPolicy("Viewer", policy => policy.RequireRole("Viewer"));
});

// Add Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("login", opts =>
    {
        opts.Window = TimeSpan.FromMinutes(5);
        opts.PermitLimit = 5;
        opts.QueueLimit = 0;
    });
    options.AddFixedWindowLimiter("viewer_verify", opts =>
    {
        opts.Window = TimeSpan.FromMinutes(5);
        opts.PermitLimit = 10;
        opts.QueueLimit = 0;
    });
});

// Configure ForwardedHeaders for Traefik
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// Add CORS from environment variable
var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")
    ?? "http://localhost:3000,http://localhost:5173";
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins.Split(','))
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add DbContext with SQLite
builder.Services.AddDbContext<QuizGameDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=quizgame.db"));

// Register services
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddSingleton<IGameSessionService, GameSessionService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Register background services
builder.Services.AddHostedService<TimerBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<GameHub>("/gameHub");

// Health endpoint
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

// Serve SPA static files
app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "wwwroot";

    // Only use proxy in development
    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
    }
});

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QuizGameDbContext>();

    // Apply migrations in production, use EnsureCreated in development
    if (app.Environment.IsProduction())
    {
        db.Database.Migrate();

        // Only seed if database is empty (no questions exist)
        if (!db.Questions.Any())
        {
            DbSeeder.SeedDatabase(db);
        }
    }
    else
    {
        db.Database.EnsureCreated();

        // Always seed in development
        DbSeeder.SeedDatabase(db);
    }
}

app.Run();
