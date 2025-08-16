using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Application.Common.Abstraction;
using Domain.Entities.Flashcards;

namespace Infrastructure.Services;

public class AiService : IAiService
{
    private readonly HttpClient _httpClient;
    
    public AiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<Flashcard>> GenerateFlashcardFromText(string content, CancellationToken cancellationToken)
    {
        var prompt = "Extract all terms and their one-sentence definitions from the text below." +
                     "\n\nReturn ONLY a valid JSON array like this:\n[\n  {\n    \"term\": \"your term here\",\n    \"definition\": \"a one-sentence definition here.\"\n  }\n]\n\nDo not include any extra text, explanation, or markdown. " +
                     $"Only return a JSON array.\n\nText: {content}\n";
        
        var payload = new
        {
            model = "gpt-4.1",
            input = new[]
            {
                new { role = "user", content = prompt }
            }
        };
        
        var request = new HttpRequestMessage(HttpMethod.Post, "responses");
        request.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json"
        );
        
        var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        using var doc = JsonDocument.Parse(jsonResponse);
        
        var messageContent = doc.RootElement
            .GetProperty("output")[0]
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString();
       
        var flashcards = JsonSerializer.Deserialize<List<Flashcard>>(
            messageContent ?? throw new Exception("Something went wrong."),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ) ?? throw new Exception("Something went wrong.");

        return flashcards;
    }
    
    public async Task<List<Flashcard>> GenerateFlashcardFromTopic(string topic, CancellationToken cancellationToken)
    {
        var prompt =
            "You are a flashcard generator. Given a topic, generate a list of important terms and their one-sentence definitions related to that topic." +
            "\n\nReturn ONLY a valid JSON array in the following format:\n[\n  {\n    \"term\": \"Term here\",\n    \"definition\": \"One-sentence definition here.\"\n  }\n]\n\n" +
            "Rules:\n" +
            "- Provide concise, clear definitions.\n" +
            "- Include at least 10 relevant terms, more if needed.\n" +
            "- Do not include any extra text, explanation, or markdown outside the JSON array.\n\n" +
            $"Topic: {topic}";
        
        var payload = new
        {
            model = "gpt-4.1",
            input = new[]
            {
                new { role = "user", content = prompt }
            }
        };
        var jsonPayload = JsonSerializer.Serialize(payload);
        var stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
       
        var response = await _httpClient.PostAsync("responses", stringContent, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        
        using var doc = JsonDocument.Parse(responseContent);
        
        var messageContent = doc.RootElement
            .GetProperty("output")[0]
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString();
       
        var flashcards = JsonSerializer.Deserialize<List<Flashcard>>(
            messageContent ?? throw new Exception("Something went wrong."),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ) ?? throw new Exception("Something went wrong.");

        return flashcards;

    }
}