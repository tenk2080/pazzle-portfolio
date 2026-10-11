using Microsoft.EntityFrameworkCore;
using practice.Models;

namespace practice.Data;

public class AppDbContext : DbContext
{
    // ─── Основные сущности ───
    public DbSet<User> Users => Set<User>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Direction> Directions => Set<Direction>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Attempt> Attempts => Set<Attempt>();
    public DbSet<UserAttemptAnswer> UserAttemptAnswers => Set<UserAttemptAnswer>();
    public DbSet<UserSelectedOption> UserSelectedOptions => Set<UserSelectedOption>();
    public DbSet<UserTextAnswer> UserTextAnswers => Set<UserTextAnswer>();
    public DbSet<TestResult> TestResults => Set<TestResult>();

    // ─── Join-таблицы ───
    public DbSet<StudentGroup> StudentGroups => Set<StudentGroup>();
    public DbSet<StudentTest> StudentTests => Set<StudentTest>();
    public DbSet<GroupTest> GroupTests => Set<GroupTest>();
    public DbSet<ProjectTest> ProjectTests => Set<ProjectTest>();
    public DbSet<CourseTest> CourseTests => Set<CourseTest>();
    public DbSet<DirectionTest> DirectionTests => Set<DirectionTest>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        //  User
         
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Login).IsRequired();
            e.Property(x => x.Email).IsRequired();
            e.Property(x => x.PasswordHash).IsRequired();
            e.Property(x => x.FirstName).IsRequired();
            e.Property(x => x.LastName).IsRequired();

            // Enum → string
            e.Property(x => x.Role)
                .HasConversion<string>()
                .IsRequired();

            e.Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Уникальные индексы
            e.HasIndex(x => x.Login).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
        });

       
        //  Student (1:1 → User)
        
        modelBuilder.Entity<Student>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(30);

            e.Property(x => x.VkProfileLink).IsRequired();

            e.Property(x => x.UserId).IsRequired();

            // Уникальный FK = связь 1:1
            e.HasIndex(x => x.UserId).IsUnique();

            e.HasOne(x => x.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

         
        //  Group
         
        modelBuilder.Entity<Group>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Name).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();

            e.Property(x => x.DirectionId).IsRequired();
            e.Property(x => x.CourseId).IsRequired();
            e.Property(x => x.ProjectId).IsRequired();

            e.HasOne(x => x.Direction)
                .WithMany(d => d.Groups)
                .HasForeignKey(x => x.DirectionId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Course)
                .WithMany(c => c.Groups)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Project)
                .WithMany(p => p.Groups)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        
        //  Course
         
        modelBuilder.Entity<Course>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Name).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

         
        //  Project
        
        modelBuilder.Entity<Project>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Name).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

        
        //  Direction
        
        modelBuilder.Entity<Direction>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Name).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

       
        //  Test
        
        modelBuilder.Entity<Test>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Title).IsRequired();
            e.Property(x => x.Description).IsRequired();

            e.Property(x => x.IsRepeatable).HasDefaultValue(false);
            e.Property(x => x.IsPublic).HasDefaultValue(false);

            // Enum → string
            e.Property(x => x.Type)
                .HasConversion<string>()
                .HasDefaultValue(TestType.KnowledgeCheck);

            e.Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            e.Property(x => x.PublishedAt).IsRequired();
            e.Property(x => x.Deadline).IsRequired();

            e.Property(x => x.DurationMinutes).IsRequired(false);
            e.Property(x => x.PassingScore).IsRequired(false);
            e.Property(x => x.MaxAttempts).IsRequired(false);

            // Test → Questions: 1:many, Cascade
            e.HasMany(x => x.Questions)
                .WithOne(q => q.Test)
                .HasForeignKey(q => q.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

         
        //  Question
        
        modelBuilder.Entity<Question>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Text).IsRequired();
            e.Property(x => x.Number).IsRequired();

            e.Property(x => x.Description).HasMaxLength(2000);

            // Enum → string
            e.Property(x => x.AnswerType)
                .HasConversion<string>()
                .HasDefaultValue(AnswerType.SingleChoice);

            e.Property(x => x.IsScoring).HasDefaultValue(true);
            e.Property(x => x.MaxScore).IsRequired(false);

            e.Property(x => x.TestId).IsRequired();

            // Уникальный составной индекс
            e.HasIndex(x => new { x.TestId, x.Number }).IsUnique();
        });

         
        //  Answer
        
        modelBuilder.Entity<Answer>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Text).IsRequired();
            e.Property(x => x.IsCorrect).IsRequired();
            e.Property(x => x.QuestionId).IsRequired();

            // Answer → Question: many:1, Cascade
            e.HasOne(x => x.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

         
        //  Attempt
       
        modelBuilder.Entity<Attempt>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.StartedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            e.Property(x => x.SubmittedAt).IsRequired(false);
            e.Property(x => x.Score).IsRequired(false);

            e.Property(x => x.TestId).IsRequired();
            e.Property(x => x.StudentId).IsRequired();

            // Attempt → Test: many:1, Restrict
            e.HasOne(x => x.Test)
                .WithMany(t => t.Attempts)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Restrict);

            // Attempt → Student: many:1, Cascade
            e.HasOne(x => x.Student)
                .WithMany(s => s.Attempts)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

         
        //  UserAttemptAnswer
        
        modelBuilder.Entity<UserAttemptAnswer>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.IsCorrect).HasDefaultValue(false);
            e.Property(x => x.ScoreAwarded).HasDefaultValue(0);

            e.Property(x => x.AttemptId).IsRequired();
            e.Property(x => x.QuestionId).IsRequired();

            // Уникальный составной индекс: один ответ на вопрос в попытке
            e.HasIndex(x => new { x.AttemptId, x.QuestionId }).IsUnique();

            // → Attempt: many:1, Cascade
            e.HasOne(x => x.Attempt)
                .WithMany(a => a.UserAttemptAnswers)
                .HasForeignKey(x => x.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            // → Question: many:1, Restrict
            e.HasOne(x => x.Question)
                .WithMany(q => q.UserAttemptAnswers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // → SelectedAnswer (SingleChoice): nullable, SetNull
            e.HasOne(x => x.SelectedAnswer)
                .WithMany()
                .HasForeignKey(x => x.SelectedAnswerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        
        //  UserSelectedOption
        
        modelBuilder.Entity<UserSelectedOption>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.UserAttemptAnswerId).IsRequired();
            e.Property(x => x.AnswerId).IsRequired();

            // → UserAttemptAnswer: many:1, Cascade
            e.HasOne(x => x.UserAttemptAnswer)
                .WithMany(uaa => uaa.SelectedOptions)
                .HasForeignKey(x => x.UserAttemptAnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            // → Answer: many:1, Restrict
            e.HasOne(x => x.Answer)
                .WithMany(a => a.UserSelectedOptions)
                .HasForeignKey(x => x.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

         
        //  UserTextAnswer (1:1 → UserAttemptAnswer)
         
        modelBuilder.Entity<UserTextAnswer>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.TextAnswer)
                .IsRequired()
                .HasMaxLength(4000);

            e.Property(x => x.UserAttemptAnswerId).IsRequired();

            e.HasOne(x => x.UserAttemptAnswer)
                .WithOne(uaa => uaa.UserTextAnswer)
                .HasForeignKey<UserTextAnswer>(x => x.UserAttemptAnswerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

         
        //  TestResult
        
        modelBuilder.Entity<TestResult>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Passed).IsRequired();
            e.Property(x => x.TestId).IsRequired();
            e.Property(x => x.AttemptId).IsRequired();
            e.Property(x => x.StudentId).IsRequired();

            // Уникальный составной индекс
            e.HasIndex(x => new { x.TestId, x.StudentId, x.AttemptId }).IsUnique();

            // → Test: Restrict
            e.HasOne(x => x.Test)
                .WithMany(t => t.TestResults)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Restrict);

            // → Attempt: Cascade
            e.HasOne(x => x.Attempt)
                .WithMany(a => a.TestResults)
                .HasForeignKey(x => x.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            // → Student: Cascade
            e.HasOne(x => x.Student)
                .WithMany(s => s.TestResults)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        
        //  StudentGroup (Student ↔ Group, many:many)
        
        modelBuilder.Entity<StudentGroup>(e =>
        {
            e.HasKey(x => new { x.StudentId, x.GroupId });

            e.Property(x => x.EnrolledAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            e.Property(x => x.IsActive).HasDefaultValue(true);

            e.HasOne(x => x.Student)
                .WithMany(s => s.StudentGroups)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Group)
                .WithMany(g => g.StudentGroups)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        });

   
        //  StudentTest → таблица test_students
        
        modelBuilder.Entity<StudentTest>(e =>
        {
            e.ToTable("test_students");
            e.HasKey(x => new { x.StudentId, x.TestId });

            e.Property(x => x.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            e.HasOne(x => x.Student)
                .WithMany(s => s.StudentTests)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Test)
                .WithMany(t => t.StudentTests)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        });
         
        //  GroupTest → таблица test_groups
         
        modelBuilder.Entity<GroupTest>(e =>
        {
            e.ToTable("test_groups");
            e.HasKey(x => new { x.GroupId, x.TestId });

            e.Property(x => x.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            e.HasOne(x => x.Group)
                .WithMany(g => g.GroupTests)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Test)
                .WithMany(t => t.GroupTests)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        
        //  ProjectTest → таблица test_projects
      
        modelBuilder.Entity<ProjectTest>(e =>
        {
            e.ToTable("test_projects");
            e.HasKey(x => new { x.ProjectId, x.TestId });

            e.Property(x => x.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            e.HasOne(x => x.Project)
                .WithMany(p => p.ProjectTests)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Test)
                .WithMany(t => t.ProjectTests)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        
        //  CourseTest → таблица test_courses
         
        modelBuilder.Entity<CourseTest>(e =>
        {
            e.ToTable("test_courses");
            e.HasKey(x => new { x.CourseId, x.TestId });

            e.Property(x => x.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            e.HasOne(x => x.Course)
                .WithMany(c => c.CourseTests)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Test)
                .WithMany(t => t.CourseTests)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

       
        //  DirectionTest → таблица test_directions
      
        modelBuilder.Entity<DirectionTest>(e =>
        {
            e.ToTable("test_directions");
            e.HasKey(x => new { x.DirectionId, x.TestId });

            e.Property(x => x.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            e.HasOne(x => x.Direction)
                .WithMany(d => d.DirectionTests)
                .HasForeignKey(x => x.DirectionId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Test)
                .WithMany(t => t.DirectionTests)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
