using Microsoft.EntityFrameworkCore;
using QuizGame.Domain.Entities;

namespace QuizGame.Infrastructure.Data;

public class QuizGameDbContext : DbContext
{
    public QuizGameDbContext(DbContextOptions<QuizGameDbContext> options)
        : base(options)
    {
    }

    public DbSet<Question> Questions => Set<Question>();
    public DbSet<RegularQuestionDetails> RegularQuestionDetails => Set<RegularQuestionDetails>();
    public DbSet<McqQuestionDetails> McqQuestionDetails => Set<McqQuestionDetails>();
    public DbSet<ListQuestionAnswer> ListQuestionAnswers => Set<ListQuestionAnswer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Question configuration
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(q => q.Id);

            entity.Property(q => q.Type)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(q => q.Difficulty)
                .IsRequired();

            entity.Property(q => q.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(q => q.TextFr)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(q => q.TextNl)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(q => q.Category)
                .HasMaxLength(200);

            entity.Property(q => q.Tags)
                .HasMaxLength(500);
        });

        // RegularQuestionDetails configuration (1:1 with Question)
        modelBuilder.Entity<RegularQuestionDetails>(entity =>
        {
            entity.HasKey(r => r.QuestionId);

            entity.Property(r => r.AnswerFr)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(r => r.AnswerNl)
                .IsRequired()
                .HasMaxLength(500);

            entity.HasOne(r => r.Question)
                .WithOne(q => q.RegularDetails)
                .HasForeignKey<RegularQuestionDetails>(r => r.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // McqQuestionDetails configuration (1:1 with Question)
        modelBuilder.Entity<McqQuestionDetails>(entity =>
        {
            entity.HasKey(m => m.QuestionId);

            entity.Property(m => m.ChoiceAFr).IsRequired().HasMaxLength(300);
            entity.Property(m => m.ChoiceANl).IsRequired().HasMaxLength(300);
            entity.Property(m => m.ChoiceBFr).IsRequired().HasMaxLength(300);
            entity.Property(m => m.ChoiceBNl).IsRequired().HasMaxLength(300);
            entity.Property(m => m.ChoiceCFr).IsRequired().HasMaxLength(300);
            entity.Property(m => m.ChoiceCNl).IsRequired().HasMaxLength(300);

            entity.Property(m => m.CorrectChoice)
                .IsRequired()
                .HasConversion<string>();

            entity.HasOne(m => m.Question)
                .WithOne(q => q.McqDetails)
                .HasForeignKey<McqQuestionDetails>(m => m.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ListQuestionAnswer configuration (many:1 with Question)
        modelBuilder.Entity<ListQuestionAnswer>(entity =>
        {
            entity.HasKey(l => l.Id);

            entity.Property(l => l.AnswerFr)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(l => l.AnswerNl)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(l => l.AltSpellings)
                .HasMaxLength(500);

            entity.HasOne(l => l.Question)
                .WithMany(q => q.ListAnswers)
                .HasForeignKey(l => l.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
