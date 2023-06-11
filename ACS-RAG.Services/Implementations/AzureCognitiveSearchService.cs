using ACS.RAG.Services.Interfaces;
using ACS.RAG.Services.Models.DTOs;
using ACS.RAG.Services.Models.Settings;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;
using System.Text.Json;
using ACS.RAG.Services.Utilities;

namespace ACS.RAG.Services.Implementations;

public class AzureCognitiveSearchService : ISearchService
{
    private readonly AzureCognitiveSearchSettings _settings;

    public AzureCognitiveSearchService(AzureCognitiveSearchSettings settings) => _settings = settings;

    public async Task<SearchResult?> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            throw new ArgumentNullException(nameof(query));

        var apiURL = string.Format(_settings.ApiURL, query);

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Application.Json));
        client.DefaultRequestHeaders.Add("api-key", _settings.ServiceKey);
        using HttpResponseMessage response = await client.GetAsync(apiURL);

        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<SearchResponse>(json);
        if (result is null) return null;

        var searchValues = result.SearchValues;
        if (!searchValues.Any()) return null;

        var searchValue = result.SearchValues.First();
        return new SearchResult(searchValue.Content, searchValue.MetadataStoragePath.DecodeBase64URL());
    }
}
