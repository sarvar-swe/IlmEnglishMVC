using IlmEnglish.App.Data;
using IlmEnglish.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IlmEnglish.App.Controllers;
public class WordController : Controller
{
    private readonly ILogger<WordController> logger;
    private readonly IAppDbContext dbContext;

    public WordController(
        ILogger<WordController> logger,
        IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromRoute] int? id, CancellationToken cancellationToken)
    {
        if(id == null || id == 0)
            return NotFound();
        
        CourseBookUnit unit = await dbContext.CourseBookUnits
            .Where(u => u.Id == id)
            .Include(u => u.Vocabulary)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(unit == null)
            return NotFound();

        return PartialView(unit.Vocabulary.ToList());
    }

    [HttpGet]
    public async Task<ActionResult> Create([FromRoute] int? id, CancellationToken cancellationToken)
    {
        if(id == null || id == 0)
            return NotFound();
        
        CourseBook book = await dbContext.CourseBooks
            .Where(b => b.Id == id)
            .Include(b => b.Units)
            .FirstOrDefaultAsync(cancellationToken);

        if(book == null)
            return NotFound();
        
        return View(book.Units.ToList());
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] int? unitId,
        [FromForm] string english,
        [FromForm] string uzbek,
        [FromForm] string russian,
        [FromForm] string imageUrl,
        CancellationToken cancellationToken)
    {
        if(unitId == null || unitId == 0)
            return NotFound();

        CourseBookUnit unit = await dbContext.CourseBookUnits
            .Where(u => u.Id == unitId)
            .Include(u => u.Vocabulary)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(unit == null)
            return NotFound();
        
        unit.Vocabulary.Add(new CourseBookUnitWord
        {
            English = english,
            Uzbek = uzbek,
            Russian = russian,
            ImageUrl = imageUrl,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        });

        await dbContext.SaveChangesAsync(cancellationToken);

        return RedirectToAction("Create", "Word");
    }

    [HttpGet]
    public async Task<IActionResult> Practice([FromRoute] int? id, CancellationToken cancellationToken)
    {
        if(id == null || id == 0)
            return NotFound();
        
        CourseBookUnit unit = await dbContext.CourseBookUnits
            .Where(u => u.Id == id)
            .Include(u => u.Vocabulary)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(unit == null)
            return NotFound();

        return PartialView(unit.Vocabulary.ToList());
    }

}