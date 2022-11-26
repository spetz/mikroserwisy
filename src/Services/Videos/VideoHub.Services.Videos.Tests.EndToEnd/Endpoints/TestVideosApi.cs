using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace VideoHub.Services.Videos.Tests.EndToEnd.Endpoints;

internal class TestVideosApi : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }

    public TestVideosApi()
    {
        Client = WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("test");
            })
            .CreateClient();
    }
}