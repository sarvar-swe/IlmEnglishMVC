namespace IlmEnglish.App.Models;

public class CourseBookUnit
{
    public int Id { get; set; }
    public int UnitNumber { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public int CourseBookId { get; set; }
    public CourseBook CourseBook { get; set; }
    public List<CourseBookUnitAudio> Audios { get; set; } = [];
    public List<CourseBookUnitWord> Vocabulary { get; set; } = [];
}