using ACS.RAG.Services.Interfaces;
using ACS.RAG.Services.Models.Settings;
using ACS.RAG.Services.Utilities;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Microsoft.SemanticKernel;

namespace ACS.RAG.Services.Implementations;

public class ChatGPTService : IChatGPTService
{
    private readonly AzureOpenAISettings _settings;
    private readonly ChatRequestSettings chatRequestSettings;

    private readonly KernelBuilder kernelBuilder;
    private IKernel? semanticKernel;
    private IChatCompletion? chatGPT;
    private OpenAIChatHistory? chatHistory;

    private int tokensCount;

    public ChatGPTService(AzureOpenAISettings settings)
    {
        _settings = settings;
        chatRequestSettings = new() { MaxTokens = _settings.MaxCompletionTokens };
        kernelBuilder = new();
    }

    public async Task<string?> GetAnswerAsync(string question, string context)
    {
        if (chatHistory is null) SetUpChat();

        TokensLimitCheck(question, context);

        if (!string.IsNullOrEmpty(context))
            chatHistory!.AddSystemMessage(context);

        chatHistory!.AddUserMessage(question);

        string completion = await chatGPT!.GenerateMessageAsync(chatHistory, chatRequestSettings);

        chatHistory!.AddAssistantMessage(completion);
        tokensCount += completion.TokensCount();

        return completion;
    }

    private void SetUpChat()
    {
        kernelBuilder.WithAzureChatCompletionService(_settings.DeploymentName, _settings.Endpoint, _settings.ServiceKey);
        semanticKernel = kernelBuilder.Build();

        chatGPT = semanticKernel.GetService<IChatCompletion>();

        if (!string.IsNullOrEmpty(_settings.Instructions))
        {
            tokensCount += _settings.Instructions.TokensCount();
            chatHistory = (OpenAIChatHistory)chatGPT.CreateNewChat(_settings.Instructions);
        }
        else
        {
            chatHistory = (OpenAIChatHistory)chatGPT.CreateNewChat();
        }
    }

    private void TokensLimitCheck(string question, string context)
    {
        var tokensSum = tokensCount + question.TokensCount();
        if (!string.IsNullOrEmpty(context))
            tokensSum += context.TokensCount();

        while (tokensSum > _settings.MaxModelTokens && chatHistory?.Messages.Count > 2)
        {
            int targetIndex = 1;
            tokensCount -= chatHistory.Messages[targetIndex].Content.TokensCount();
            chatHistory.Messages.RemoveAt(targetIndex);
        }
        tokensCount = tokensSum;
    }
}
