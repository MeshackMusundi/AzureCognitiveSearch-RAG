using ACS.RAG.Services.Implementations;
using ACS.RAG.Services.Models.Settings;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var azureOpenAISettings = config.GetSection(nameof(AzureOpenAISettings)).Get<AzureOpenAISettings>();
var azureSearchSettings = config.GetSection(nameof(AzureCognitiveSearchSettings)).Get<AzureCognitiveSearchSettings>();

var searchService = new AzureCognitiveSearchService(azureSearchSettings);
var chatGPTService = new ChatGPTService(azureOpenAISettings);

while (true)
{
    var query = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(query))
    {
        if (query.ToLower() == "exit") break;

        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Line)
            .SpinnerStyle(Style.Parse("green"))
            .StartAsync("Processing...", async ctx =>
            {
                var searchResult = await searchService.SearchAsync(query);
                var searchContent = searchResult == null ? string.Empty : searchResult.Content;
                var completion = await chatGPTService.GetAnswerAsync(query, searchContent);

                AnsiConsole.MarkupLine($"\n[teal]{completion}[/]");

                if (searchResult != null && !completion.StartsWith("I'm sorry"))
                {
                    var metadataStoragePathURI = new Uri(searchResult.MetadataStoragePath);
                    var fileName = Path.GetFileName(metadataStoragePathURI.LocalPath);
                    AnsiConsole.MarkupLine($"\n[aqua]Source: {fileName}[/]\n");
                }

                AnsiConsole.Write(new Rule() { Style = Style.Parse("navajowhite1 dim") });
            });

        Console.WriteLine();
    }
}

Console.ReadKey();