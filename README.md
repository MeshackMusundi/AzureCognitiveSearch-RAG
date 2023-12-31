# Introduction
This is a simple C# demo highlighting how to have conversational interactions with private data in Azure using Azure Cognitive Search and Azure OpenAI.

Azure Cognitive Search is used for Retrieval Augmented Generation.

# Requirements
You should have the following resources set up on Azure,
- **Azure Storage Container**: This will host your private documents (PDF, Word, Excel,...) that you want to use as the knowledge base. The storage account is added as a data source of your search service.
- **Azure Cognitive Search Service**: The service should be set up in a standard tier which will ensure you can use Semantic Search. Add the storage container as a data source for the search service and set up the indexer, index, and Semantic Search configuration.
- **Azure OpenAI**: Add a model deployment to your Azure OpenAI service, either GPT 3.5 Turbo or GPT 4.

## User Secrets
Add user secrets to the console project as follows, with the neccessary values specified.
```
{
  "AzureOpenAISettings": {
    "ServiceKey": "<Key>",
    "ServiceName": "<Service-Name>",
    "DeploymentName": "<Model-Deployment-Name>",
    "Endpoint": "https://<Service-Name>.openai.azure.com",
    "MaxCompletionTokens": 500,
    "MaxModelTokens": 8192,
    "Instructions": "Assistant is an intelligent chatbot..."
  },
  "AzureCognitiveSearchSettings": {
    "ServiceKey": "<Key>",
    "ServiceName": "<Service-Name>",
    "Index": "<Index-Name>",
    "SemanticConfig": "<Semantic-Config-Name>",
    "ApiVersion": "<API-Version>",
    "ApiURL": "https://<Service-Name>.search.windows.net/indexes/<Index-Name>/docs?api-version=<API-Version>&search={0}&queryLanguage=en-US&queryType=semantic&captions=extractive&answers=extractive%7Ccount-3&semanticConfiguration=<Semantic-Config-Name>"
  }
}
```
