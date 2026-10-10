public class WritingAssignment : Assignment
{
    private string _title;

    public WritingAssignment(string studentName, string topic, string title)
        : base(studentName, topic)
    {
        _title = title;
    }

    public string GetWritingInformation()
    {
        // _studentName is private in the base class, so use the public getter
        string studentName = GetStudentName();
        return $"{_title} by {studentName}";
    }
}