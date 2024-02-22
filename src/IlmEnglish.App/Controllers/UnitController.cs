using IlmEnglish.App.Data;
using IlmEnglish.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IlmEnglish.App.Controllers;

public class UnitController : Controller
{
    private readonly ILogger<UnitController> logger;
    private readonly IAppDbContext dbContext;

    public UnitController(
        ILogger<UnitController> logger,
        IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult> Index([FromRoute] int? id, CancellationToken cancellationToken)
    {
        if(id == null || id == 0)
            return NotFound();
        
        CourseBook book = await dbContext.CourseBooks
            .Where(u => u.Id == id)
            .Include(u => u.Units)
            .FirstOrDefaultAsync(cancellationToken);

        return View(book.Units.ToList());
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromRoute] int? id,
        CourseBookUnit courseBookUnit,
        CancellationToken cancellationToken)
    {
        if(id == null || id == 0 || courseBookUnit == null)
            return BadRequest("Invalid input data.");
        
        CourseBook book = await dbContext.CourseBooks
            .Where(i => i.Id == id)
            .Include(c => c.Units)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(book == null)
            return NotFound($"Course with Id {id} not found.");

        book.Units.Add(new CourseBookUnit
        {
            
            UnitNumber = courseBookUnit.UnitNumber,
            Name = courseBookUnit.Name,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        });

        await dbContext.SaveChangesAsync(cancellationToken);

        return RedirectToAction("Create", "Unit");
    }
}
