using IlmEnglish.App.Data;
using IlmEnglish.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IlmEnglish.App.Controllers;
public class AudioController : Controller
{
    private readonly ILogger<AudioController> logger;
    private readonly IAppDbContext dbContext;

    public AudioController(
        ILogger<AudioController> logger,
        IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public async Task<IActionResult> Index([FromRoute] int? id, CancellationToken cancellationToken)
    {
        if(id == null || id == 0)
            return NotFound();
        
        CourseBookUnit unit = await dbContext.CourseBookUnits
            .Where(u => u.Id == id)
            .Include(u => u.Audios)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(unit == null)
            return NotFound();

        return PartialView(unit.Audios.ToList());
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
        [FromForm] string name,
        [FromForm] string url,
        [FromForm] string mimeType,
        CancellationToken cancellationToken)
    {
        if(unitId == null || unitId == 0)
            return NotFound();
        
        CourseBookUnit unit = await dbContext.CourseBookUnits
            .Where(u => u.Id == unitId)
            .Include(u => u.Audios)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(unit == null)
            return NotFound();
        
        unit.Audios.Add(new CourseBookUnitAudio
        {
            Name = name,
            Url = url,
            MimeType = mimeType,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        });

        await dbContext.SaveChangesAsync(cancellationToken);

        return RedirectToAction("Create", "Audio");
    }
}
