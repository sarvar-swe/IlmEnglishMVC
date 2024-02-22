namespace IlmEnglish.App.Models;

public class CourseBookUnitWord
{
    public int Id { get; set; }
    public string English { get; set; }
    public string Uzbek { get; set; }
    public string Russian { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public int CourseBookUnitId { get; set; }
    public CourseBookUnit CourseBookUnit { get; set; }
}