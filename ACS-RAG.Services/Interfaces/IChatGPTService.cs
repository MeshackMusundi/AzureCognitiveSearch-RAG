namespace ACS.RAG.Services.Interfaces;

public interface IChatGPTService
{
    Task<string?> GetAnswerAsync(string question, string context);
}
