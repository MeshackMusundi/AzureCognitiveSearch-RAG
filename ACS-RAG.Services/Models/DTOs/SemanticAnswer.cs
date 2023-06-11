using System.Text.Json.Serialization;

namespace ACS.RAG.Services.Models.DTOs;

public class SemanticAnswer
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}
