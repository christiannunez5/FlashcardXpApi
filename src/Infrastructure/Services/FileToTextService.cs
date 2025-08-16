using Application.Common.Abstraction;
using UglyToad.PdfPig;

namespace Infrastructure.Services;

public class FileToTextService : IFileToTextService
{
    public string ExtractTextFromFile(Stream fileStream)
    {
        using var pdf = PdfDocument.Open(fileStream);
        var text = string.Join("\n", pdf.GetPages().Select(p => p.Text));
        return text;
    }
}