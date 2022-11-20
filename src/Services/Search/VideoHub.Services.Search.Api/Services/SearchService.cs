using MongoDB.Driver;
using MongoDB.Driver.Linq;
using VideoHub.Services.Search.Api.Models;

namespace VideoHub.Services.Search.Api.Services;

internal sealed class SearchService : ISearchService
{
    private const string CollectionName = "items";
    private readonly IMongoDatabase _database;

    public SearchService(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task<IEnumerable<SearchItem>> SearchAsync(string query)
        => await _database
            .GetCollection<SearchItem>(CollectionName)
            .AsQueryable()
            .Where(x => x.Data.Contains(query))
            .ToListAsync();

    public Task AddAsync(SearchItem item)
        => _database
            .GetCollection<SearchItem>(CollectionName)
            .InsertOneAsync(item);

    public Task DeleteAsync(long id)
        => _database
            .GetCollection<SearchItem>(CollectionName)
            .DeleteOneAsync(x => x.Id == id);
}