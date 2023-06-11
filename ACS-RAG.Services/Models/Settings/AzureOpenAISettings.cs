namespace ACS.RAG.Services.Models.Settings;

public class AzureOpenAISettings
{
    public string ServiceKey { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public int MaxCompletionTokens { get; set; }
    public int MaxModelTokens { get; set; }
    public string Instructions { get; set; } = string.Empty;
}
