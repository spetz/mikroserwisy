using System.Diagnostics.CodeAnalysis;
using Micro.Testing;
using VideoHub.Services.Videos.Core.Entities;
using Xunit;
using Xunit.Abstractions;

namespace VideoHub.Services.Videos.Tests.Contract.Provider.Endpoints;

[ExcludeFromCodeCoverage]
[Collection(Const.TestCollection)]
public class ChannelsEndpointsContractTests : IDisposable
{
    [Fact]
    public async Task should_honour_channels_consumer_endpoints_pacts()
    {
        var pactFile = _endpointContract.GetPactFile();
        await _testServer.StartAsync();
        await _testDatabase.Context.Videos.AddAsync(new Video(1, 1, "test", 1,
            TimeSpan.FromMinutes(1), DateTime.UtcNow));
        await _testDatabase.Context.SaveChangesAsync();
        
        _endpointContract.Verifier
            .ServiceProvider(_endpointContract.Provider, _testServer.Url)
            .WithFileSource(pactFile)
            .WithSslVerificationDisabled()
            .Verify();
    }

    #region Arrange

    private readonly TestDatabase _testDatabase;
    private readonly EndpointContract _endpointContract;
    private readonly TestServer _testServer;

    public ChannelsEndpointsContractTests(ITestOutputHelper output)
    {
        _testDatabase = new TestDatabase();
        _endpointContract = new EndpointContract("channels", "videos", output);
        _testServer = new TestServer("VideoHub.Services.Videos.Api", output);
    }

    #endregion

    public void Dispose()
    {
        _endpointContract.Dispose();
        _testServer.Dispose();
        _testDatabase.Dispose();
    }
}