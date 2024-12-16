using MinimalAPI.dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ADD NEW ENDPOINTS HERE

/// <summary>
/// Returns "Hello World!".
/// </summary>
app.MapGet("/", () => "Hello World!");

/// <summary>
/// Calculates the number of days between two dates.
/// </summary>
/// <param name="date1">The first date.</param>
/// <param name="date2">The second date.</param>
/// <returns>The number of days between the two dates.</returns>
app.MapGet("/DaysBetweenDates", (DateTime date1, DateTime date2) => 
{
    return Results.Ok((date2 - date1).TotalDays);
});

/// <summary>
/// Validates a Spanish phone number.
/// </summary>
/// <param name="phoneNumber">The phone number to validate.</param>
/// <returns>True if the phone number is valid, otherwise false.</returns>
app.MapGet("/validatephonenumber", (string phoneNumber) => 
{
    var regex = new Regex(@"^\+34\d{9}$");
    bool isValid = regex.IsMatch(phoneNumber);
    return Results.Ok(isValid);
});

/// <summary>
/// Validates a Spanish DNI.
/// </summary>
/// <param name="dni">The DNI to validate.</param>
/// <returns>"valid" if the DNI is valid, otherwise "invalid".</returns>
app.MapGet("/validatespanishdni", (string dni) => 
{
    var dniLetter = dni.Substring(dni.Length - 1);
    var dniNumber = int.Parse(dni.Substring(0, dni.Length - 1));
    var validLetters = "TRWAGMYFPDXBNJZSQVHLCKE";
    var valid = validLetters[dniNumber % 23] == dniLetter[0];
    return Results.Ok(valid ? "valid" : "invalid");
});

/// <summary>
/// Returns the HEX color code for a given color name.
/// </summary>
/// <param name="color">The name of the color.</param>
/// <returns>The HEX color code.</returns>
app.MapGet("/returncolorcode", (string color) => 
{
    var colors = System.Text.Json.JsonSerializer.Deserialize<List<Color>>(System.IO.File.ReadAllText("colors.json"));
    var colorCode = colors?.FirstOrDefault(c => c.Name == color)?.Code.HEX;
    return Results.Ok(colorCode);
});

/// <summary>
/// Returns a random programming joke.
/// </summary>
/// <returns>A random programming joke.</returns>
app.MapGet("/tellmeajoke", async () => 
{
    var client = new HttpClient(); // TODO: Use IHttpClientFactory
    var response = await client.GetAsync("https://official-joke-api.appspot.com/jokes/programming/random");
    var jsonResponse = await response.Content.ReadAsStringAsync();
    var jokes = JsonSerializer.Deserialize<List<Joke>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    var joke = jokes?.FirstOrDefault();
    return joke != null ? Results.Ok(joke.Setup + " " + joke.Punchline) : Results.NotFound("No joke found.");
});

/// <summary>
/// Returns a list of movies by a given director.
/// </summary>
/// <param name="director">The name of the director.</param>
/// <returns>A list of movies by the director.</returns>
app.MapGet("/moviesbydirector", async (string director) => 
{
    var client = new System.Net.Http.HttpClient(); // TODO: Use IHttpClientFactory
    var apiKey = "api-key"; // Replace with your actual OMDB API key
    var response = await client.GetAsync($"http://www.omdbapi.com/?apikey={apiKey}&s={director}&type=movie");
    var movies = System.Text.Json.JsonSerializer.Deserialize<MovieResponse>(await response.Content.ReadAsStringAsync());
    return Results.Ok(movies.Search);
});

/// <summary>
/// Parses a URL and returns the host.
/// </summary>
/// <param name="someurl">The URL to parse.</param>
/// <returns>The host of the URL.</returns>
app.MapGet("/parseurl", (string someurl) => 
{
    var uri = new Uri(someurl);
    return Results.Ok(uri.Host);
});

/// <summary>
/// Returns a list of files in the current directory.
/// </summary>
/// <returns>A list of files in the current directory.</returns>
app.MapGet("/listfiles", () => 
{
    var files = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());
    return Results.Ok(files);
});

/// <summary>
/// Returns the memory consumption of the process in GB, rounded to 2 decimals.
/// </summary>
/// <returns>The memory consumption of the process in GB.</returns>
app.MapGet("/calculatememoryconsumption", () => 
{
    var memory = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0 / 1024.0;
    return Results.Ok(memory);
});

/// <summary>
/// Returns a random European country and its ISO code.
/// </summary>
/// <returns>A random European country and its ISO code.</returns>
app.MapGet("/randomeuropeancountry", () => 
{
    var countries = new Dictionary<string, string>
    {
        { "Spain", "ES" },
        { "France", "FR" },
        { "Germany", "DE" },
        { "Italy", "IT" },
        { "Portugal", "PT" },
        { "United Kingdom", "UK" },
        { "Netherlands", "NL" },
        { "Belgium", "BE" },
        { "Sweden", "SE" },
        { "Denmark", "DK" },
        { "Norway", "NO" },
        { "Finland", "FI" },
        { "Switzerland", "CH" },
        { "Austria", "AT" },
        { "Greece", "GR" },
        { "Poland", "PL" },
        { "Czech Republic", "CZ" },
        { "Slovakia", "SK" },
        { "Hungary", "HU" },
        { "Romania", "RO" },
        { "Bulgaria", "BG" },
        { "Croatia", "HR" },
        { "Slovenia", "SI" },
        { "Serbia", "RS" },
        { "Bosnia and Herzegovina", "BA" },
        { "Montenegro", "ME" },
        { "Albania", "AL" },
        { "North Macedonia", "MK" },
        { "Kosovo", "XK" },
        { "Ukraine", "UA" },
        { "Belarus", "BY" },
        { "Russia", "RU" },
        { "Iceland", "IS" },
        { "Ireland", "IE" },
        { "Luxembourg", "LU" },
        { "Liechtenstein", "LI" },
        { "Monaco", "MC" },
        { "Andorra", "AD" },
        { "San Marino", "SM" },
        { "Vatican City", "VA" },
        { "Malta", "MT" },
        { "Cyprus", "CY" },
        { "Estonia", "EE" },
        { "Latvia", "LV" },
        { "Lithuania", "LT" },
    };
    var random = new Random();
    var index = random.Next(countries.Count);
    var country = countries.ElementAt(index);
    return Results.Ok($"{country.Key} ({country.Value})");
});

app.Run();

// Needed to be able to access this type from the MinimalAPI.Tests project.
public partial class Program
{ }
