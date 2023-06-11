using System.Text.Json.Serialization;

namespace ACS.RAG.Services.Models.DTOs;

public class SearchValue
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
    [JsonPropertyName("metadata_storage_path")]
    public string MetadataStoragePath { get; set; } = string.Empty;
}
