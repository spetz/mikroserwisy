using VideoHub.Services.Search.Api.Models;

namespace VideoHub.Services.Search.Api.Services;

public interface ISearchService
{
    Task<IEnumerable<SearchItem>> SearchAsync(string query);
    Task AddAsync(SearchItem item);
    Task DeleteAsync(long id);
}