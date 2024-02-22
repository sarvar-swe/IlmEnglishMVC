using IlmEnglish.App.Data;
using IlmEnglish.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IlmEnglish.App.Controllers;

public class CourseController : Controller
{
    private readonly ILogger<CourseController> logger;
    private readonly IAppDbContext dbContext;

    public CourseController(
        ILogger<CourseController> logger,
        IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Course> courseList = [.. dbContext.Courses.Include(c => c.Books)];
        return View(courseList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course course, CancellationToken cancellationToken)
    {
        course.CreatedAt = DateTime.UtcNow;
        course.ModifiedAt = DateTime.UtcNow;

        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync(cancellationToken);
        return RedirectToAction("Index");
    }
}
