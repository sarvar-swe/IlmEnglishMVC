namespace IlmEnglish.App.Models;

public class CourseBook
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoverPageUrl { get; set; }
    public string HardCopyUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
    public List<CourseBookUnit> Units { get; set; } = [];
}