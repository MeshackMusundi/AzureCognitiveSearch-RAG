using System.Text.Json.Serialization;

namespace ACS.RAG.Services.Models.DTOs;

public class SearchResponse
{
    [JsonPropertyName("@search.answers")]
    public SemanticAnswer[] SearchAnswers { get; set; } = Array.Empty<SemanticAnswer>();
    [JsonPropertyName("value")]
    public SearchValue[] SearchValues { get; set; } = Array.Empty<SearchValue>();
}
