using CsvHelper.Configuration;
using ETL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using s20601.Data;
using System.Globalization;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddUserSecrets<Program>();
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException(
                           "Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    Delimiter = "\t",
    BadDataFound = null,
    MissingFieldFound = null,
    PrepareHeaderForMatch = args => args.Header.ToLower(),
};
builder.Services.AddSingleton(csvConfig);

builder.Services.AddTransient<IMDBDataSeeder>();

var host = builder.Build();

Console.WriteLine("Starting Seeder...");

using (var scope = host.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IMDBDataSeeder>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

    try
    {
        string basePath = config["IMDBData:BasePath"] ?? "imdb-datasets";
        
        // If BasePath is relative, combine it with ContentRootPath
        if (!Path.IsPathRooted(basePath))
        {
            basePath = Path.Combine(env.ContentRootPath, basePath);
        }

        string moviesFile = config["IMDBData:MoviesFile"] ?? "";
        string nameFile = config["IMDBData:NamesFile"] ?? "";
        string principalsFile = config["IMDBData:PrincipalsFile"] ?? "";

        string fullMoviesPath = Path.Combine(basePath, moviesFile);
        string fullNameBasicsPath = Path.Combine(basePath, nameFile);
        string fullPrincipalsPath = Path.Combine(basePath, principalsFile);


        if (File.Exists(fullMoviesPath))
        {
            using var movieReader = new StreamReader(fullMoviesPath);
            await seeder.SeedMoviesWithGenres(movieReader);
            using var nameBasicsReader = new StreamReader(fullNameBasicsPath);
            await seeder.SeedCrew(nameBasicsReader);
            using var principalsReader = new StreamReader(fullPrincipalsPath);
            await seeder.SeedMovieCrew(principalsReader);
        }
        else
        {
            Console.WriteLine($"File not found: {fullMoviesPath}");
            Console.WriteLine($"Searched in: {basePath}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
    }
}

Console.WriteLine("Seeding finished.");