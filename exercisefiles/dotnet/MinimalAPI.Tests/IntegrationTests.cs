// namespace IntegrationTests;

// public class IntegrationTests : IClassFixture<TestWebApplicationFactory<Program>>
// {
//     private readonly HttpClient _client;

//     public IntegrationTests(TestWebApplicationFactory<Program> factory)
//     {
//         _client = factory.CreateClient();
//     }

//     [Fact]
//     public async Task Get_ReturnsHelloWorld()
//     {
//         // Arrange

//         // Act
//         var response = await _client.GetAsync("/");

//         // Assert
//         response.EnsureSuccessStatusCode();
//         var content = await response.Content.ReadAsStringAsync();
//         Assert.Equal("Hello World!", content);
//     }
// }

using System.Diagnostics;

namespace IntegrationTests;

public class IntegrationTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public IntegrationTests(TestWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsHelloWorld()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello World!", content);
    }

    [Fact]
    public async Task Get_DaysBetweenDates_ReturnsCorrectDays()
    {
        // Arrange
        var date1 = DateTime.Now;
        var date2 = date1.AddDays(10);
        var url = $"/DaysBetweenDates?date1={Uri.EscapeDataString(date1.ToString("O"))}&date2={Uri.EscapeDataString(date2.ToString("O"))}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("10", content);
    }

    [Fact]
    public async Task Get_ValidatePhoneNumber_ReturnsTrue()
    {
        // Arrange
        var phoneNumber = "+34123456789";
        var url = $"/validatephonenumber?phoneNumber={Uri.EscapeDataString(phoneNumber)}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("true", content);
    }

    [Fact]
    public async Task Get_ValidateSpanishDNI_ReturnsValid()
    {
        // Arrange
        var dni = "12345678Z";
        var url = $"/validatespanishdni?dni={dni}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("valid", content.Trim('"'));
    }

    [Fact]
    public async Task Get_ReturnColorCode_ReturnsHexCode()
    {
        // Arrange
        var color = "red";
        var url = $"/returncolorcode?color={color}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("#FF0000", content.Trim('"')); // Assuming the color red has this HEX code in colors.json
    }

    [Fact]
    public async Task Get_TellMeAJoke_ReturnsJoke()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/tellmeajoke");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains(" ", content); // Assuming the joke contains a space between setup and punchline
    }

    // [Fact]
    // public async Task Get_MoviesByDirector_ReturnsMovies()
    // {
    //     // Arrange
    //     var director = "Christopher Nolan";
    //     var url = $"/moviesbydirector?director={director}";

    //     // Act
    //     var response = await _client.GetAsync(url);

    //     // Assert
    //     response.EnsureSuccessStatusCode();
    //     var content = await response.Content.ReadAsStringAsync();
    //     Assert.Contains("Title", content); // Assuming the response contains movie titles
    // }

    [Fact]
    public async Task Get_ParseUrl_ReturnsHost()
    {
        // Arrange
        var urlToParse = "https://www.example.com";
        var url = $"/parseurl?someurl={urlToParse}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("www.example.com", content.Trim('"'));
    }

    [Fact]
    public async Task Get_ListFiles_ReturnsFiles()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/listfiles");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        Debug.WriteLine(content);
        
        Assert.Contains(".json", content); // Assuming there are .json files in the directory
        Assert.Contains(".dll", content); // Assuming there are .cs files in the directory
    }

    [Fact]
    public async Task Get_CalculateMemoryConsumption_ReturnsMemory()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/calculatememoryconsumption");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.True(double.TryParse(content, out var memory));
        Assert.True(memory > 0);
    }

    [Fact]
    public async Task Get_RandomEuropeanCountry_ReturnsCountry()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/randomeuropeancountry");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("(", content); // Assuming the response contains a country and its ISO code in parentheses
    }
}
