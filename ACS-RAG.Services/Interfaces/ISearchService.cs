using ACS.RAG.Services.Models.DTOs;

namespace ACS.RAG.Services.Interfaces;

public interface ISearchService
{
    Task<SearchResult?> SearchAsync(string query);
}
