using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace FinTrack.Tests;

public class CountryControllerShould
{
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public CountryControllerShould()
    {
        _server = new TestServer(new WebHostBuilder().UseStartup<>())
    }

    [Fact]
    public void Test1()
    {
        
    }
}