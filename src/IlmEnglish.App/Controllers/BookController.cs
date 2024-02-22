using IlmEnglish.App.Data;
using IlmEnglish.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IlmEnglish.App.Controllers;

public class BookController : Controller
{
    private readonly ILogger<BookController> logger;
    private readonly IAppDbContext dbContext;

    public BookController(
        ILogger<BookController> logger,
        IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromRoute] int? id,
        CourseBook courseBook,
        CancellationToken cancellationToken)
    {
        if(id == null || id == 0 || courseBook == null)
            return BadRequest("Invalid input data.");
        
        Course course = await dbContext.Courses
            .Where(i => i.Id == id)
            .Include(c => c.Books)
            .FirstOrDefaultAsync(default);
        
        if(course == null)
            return NotFound($"Course with Id {id} not found.");

        course.Books.Add(new CourseBook
        {
            Name = courseBook.Name,
            Description = courseBook.Description,
            CoverPageUrl = courseBook.CoverPageUrl,
            HardCopyUrl = courseBook.HardCopyUrl,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        });

        await dbContext.SaveChangesAsync(cancellationToken);

        return RedirectToAction("Index", "Course");
    }
}

