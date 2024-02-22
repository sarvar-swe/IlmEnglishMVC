using IlmEnglish.App.Models;
using Microsoft.EntityFrameworkCore;

namespace IlmEnglish.App.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseBook> CourseBooks { get; set; }
    public DbSet<CourseBookUnit> CourseBookUnits { get; set; }
    public DbSet<CourseBookUnitAudio> CourseBookUnitAudios { get; set; }
    public DbSet<CourseBookUnitWord> CourseBookUnitWords { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Course>()
            .HasMany(e => e.Books)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();

        modelBuilder.Entity<CourseBook>()
            .HasKey(e => e.Id);
        
        modelBuilder.Entity<CourseBook>()
            .HasMany(e => e.Units)
            .WithOne(e => e.CourseBook)
            .HasForeignKey(e => e.CourseBookId)
            .IsRequired();
        
        modelBuilder.Entity<CourseBookUnit>()
            .HasKey(e => e.Id);
        
        modelBuilder.Entity<CourseBookUnit>()
            .HasMany(e => e.Audios)
            .WithOne(e => e.CourseBookUnit)
            .HasForeignKey(e => e.CourseBookUnitId)
            .IsRequired();

        modelBuilder.Entity<CourseBookUnit>()
            .HasMany(e => e.Vocabulary)
            .WithOne(e => e.CourseBookUnit)
            .HasForeignKey(e => e.CourseBookUnitId)
            .IsRequired();
        
        modelBuilder.Entity<CourseBookUnitAudio>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<CourseBookUnitWord>()
            .HasKey(e => e.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}