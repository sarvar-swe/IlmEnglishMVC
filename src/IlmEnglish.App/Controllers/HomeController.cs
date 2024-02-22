using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IlmEnglish.App.Models;
using IlmEnglish.App.Data;
using Microsoft.EntityFrameworkCore;

namespace IlmEnglish.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly IAppDbContext dbContext;

    public HomeController(
        ILogger<HomeController> logger,
        IAppDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public IActionResult Index()
    {
        List<Course> courseList = [.. dbContext.Courses.Include(c => c.Books)];
        return View(courseList);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
