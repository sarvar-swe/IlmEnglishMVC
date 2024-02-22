using IlmEnglish.App.Models;
using Microsoft.EntityFrameworkCore;

namespace IlmEnglish.App.Data;

public interface IAppDbContext
{
    DbSet<Course> Courses { get; set; }
    DbSet<CourseBook> CourseBooks { get; set; }
    DbSet<CourseBookUnit> CourseBookUnits { get; set; }
    DbSet<CourseBookUnitAudio> CourseBookUnitAudios { get; set; }
    DbSet<CourseBookUnitWord> CourseBookUnitWords { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}