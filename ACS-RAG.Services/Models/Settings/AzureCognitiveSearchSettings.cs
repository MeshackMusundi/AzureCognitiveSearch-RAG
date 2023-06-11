namespace ACS.RAG.Services.Models.Settings;

public class AzureCognitiveSearchSettings
{
    public string ServiceKey { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string Index { get; set; } = string.Empty;
    public string SemanticConfig { get; set; } = string.Empty;
    public string ApiVersion { get; set; } = string.Empty;
    public string ApiURL { get; set; } = string.Empty;
}
