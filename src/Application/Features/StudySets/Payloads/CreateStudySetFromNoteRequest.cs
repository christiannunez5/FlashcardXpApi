namespace Application.Features.StudySets.Payloads;

public class CreateStudySetFromNoteRequest
{
    public string? StudySetId { get; set; }
    public required string NoteContent { get; set; }
}