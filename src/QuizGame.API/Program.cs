using Microsoft.EntityFrameworkCore;
using QuizGame.Application.Interfaces;
using QuizGame.Infrastructure.Data;
using QuizGame.Infrastructure.Services;
using QuizGame.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SignalR
builder.Services.AddSignalR();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add DbContext with SQLite
builder.Services.AddDbContext<QuizGameDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=quizgame.db"));

// Register services
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddSingleton<IGameSessionService, GameSessionService>();

// Register background services
builder.Services.AddHostedService<TimerBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapHub<GameHub>("/gameHub");

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QuizGameDbContext>();
    db.Database.EnsureCreated();

    // Seed the database with test questions
    DbSeeder.SeedDatabase(db);
}

app.Run();
