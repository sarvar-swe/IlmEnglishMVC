using IlmEnglish.App.Data;
using IlmEnglish.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IlmEnglish.App.Controllers;

public class PracticeController : Controller
{
    private readonly ILogger<PracticeController> logger;
    private readonly IAppDbContext dbContext;

    public PracticeController(
        ILogger<PracticeController> logger,
        IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult PracticeVocabulary([FromQuery] int? order)
    {
        if(order == 1)
            return PartialView("WordPractice");
        else if (order == 2)
            return PartialView("AudioPractice");
        else
            return PartialView("ImagePractice");
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromRoute] int? id, CancellationToken cancellationToken)
    {
        if (id == null || id == 0)
            return NotFound();

        CourseBookUnit unit = await dbContext.CourseBookUnits
            .Where(u => u.Id == id)
            .Include(u => u.Vocabulary)
            .FirstOrDefaultAsync(cancellationToken);

        if (unit == null)
            return NotFound();

        return PartialView(unit.Vocabulary.ToList());
    }
}

