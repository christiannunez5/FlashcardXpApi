namespace Application.Common.Abstraction;

public interface IFileToTextService
{
    string ExtractTextFromFile(Stream fileStream);
}