namespace IlmEnglish.App.Models;

public class CourseBookUnitAudio
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string MimeType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public int CourseBookUnitId { get; set; }
    public CourseBookUnit CourseBookUnit { get; set; }
}