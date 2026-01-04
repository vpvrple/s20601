using CsvHelper;
using CsvHelper.Configuration;
using ETL.Models;
using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;

namespace ETL;

internal class IMDBDataSeeder
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly CsvConfiguration _csvConfig;

    private readonly int batchSize = 5000;
    private readonly int maxRecords = 100000;

    public IMDBDataSeeder(
        IDbContextFactory<ApplicationDbContext> dbContextFactory,
        CsvConfiguration csvConfiguration)
    {
        _dbContextFactory = dbContextFactory;
        _csvConfig = csvConfiguration;
    }

    public async Task SeedMoviesWithGenres(TextReader reader)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        using var csv = new CsvReader(reader, _csvConfig);

        var moviesBatch = new List<Movie>();
        var records = csv.GetRecords<IMDBTitleBasics>();

        var genreCache = context.Genres
            .ToDictionary(g => g.Name, g => g, StringComparer.OrdinalIgnoreCase);

        var count = 0;

        foreach (var r in records)
        {
            if (r.titleType != "movie") continue;
            int? year = ParseNullableStringToInt(r.startYear);
            if (year == null || year < 1960) continue;
            if (r.isAdult) continue;

            var movieGenreList = new List<MovieGenre>();

            var genreNames = (r.genres ?? "")
                .Split(',')
                .Where(x => x != "\\N" && !string.IsNullOrWhiteSpace(x));

            foreach (var gName in genreNames)
            {
                if (!genreCache.TryGetValue(gName, out var genreEntity))
                {
                    genreEntity = new Genre { Name = gName };
                    genreCache[gName] = genreEntity;
                }
                movieGenreList.Add(new MovieGenre { Genre = genreEntity });
            }

            var movie = new Movie
            {
                IMDBId = r.tconst,
                Title = r.primaryTitle,
                OriginalTitle = r.originalTitle,
                TitleType = r.titleType,
                StartYear = ParseNullableStringToInt(r.startYear),
                EndYear = ParseNullableStringToInt(r.endYear) == 0 ? null : ParseNullableStringToInt(r.endYear),
                RuntimeMinutes = ParseNullableStringToInt(r.runtimeMinutes),
                MovieGenres = movieGenreList
            };

            moviesBatch.Add(movie);
            count++;

            if (count % batchSize == 0)
            {
                Console.WriteLine($"Saving batch... (Total processed: {count})");

                await context.Movies.AddRangeAsync(moviesBatch);
                await context.SaveChangesAsync();

                moviesBatch.Clear();

                context.ChangeTracker.Clear();

                foreach (var genre in genreCache.Values.Where(genre => genre.Id != 0))
                {
                    context.Genres.Attach(genre);
                }
            }

            if (count >= maxRecords)
            {
                Console.WriteLine($"Max limit reached. Stopping import.");
                break;
            }
        }

        if (moviesBatch.Count != 0)
        {
            await context.Movies.AddRangeAsync(moviesBatch);
            await context.SaveChangesAsync();
        }

        Console.WriteLine("Movies done!");
    }

    public async Task SeedCrew(TextReader reader)
    {
        int count = 0;

        using var context = await _dbContextFactory.CreateDbContextAsync();
        if (await context.Crews.AnyAsync())
        {
            Console.WriteLine("Crew data already exists. Skipping seeding.");
            return;
        }
        Console.WriteLine("Seeding Crew data...");
        var crewBatch = new List<Crew>();
        using var csv = new CsvReader(reader, _csvConfig);
        var records = csv.GetRecords<IMDBNameBasics>();
        foreach (var r in records)
        {
            var names = r.primaryName.Split(' ', 2);
            var firstName = names.Length > 0 ? names[0] : "";
            var lastName = names.Length > 1 ? names[1] : "";
            int birthYear = int.TryParse(r.birthYear, out var by) ? by : 0;
            int? deathYear = int.TryParse(r.deathYear, out var dy) ? dy : null;
            var crew = new Crew
            {
                IMDBId = r.nconst,
                FirstName = firstName,
                LastName = lastName,
                BirthYear = birthYear,
                DeathYear = deathYear
            };
            crewBatch.Add(crew);
            count++;

            if (count % batchSize == 0)
            {
                Console.WriteLine($"Saving batch... (Total processed: {count})");

                await context.Crews.AddRangeAsync(crewBatch);
                await context.SaveChangesAsync();

                crewBatch.Clear();

                context.ChangeTracker.Clear();
            }

            if (count >= maxRecords)
            {
                Console.WriteLine($"Max limit reached. Stopping import.");
                break;
            }
        }

        if (crewBatch.Count != 0)
        {
            await context.Crews.AddRangeAsync(crewBatch);
            await context.SaveChangesAsync();
        }
        await context.SaveChangesAsync();
        Console.WriteLine("Crew data seeding completed.");
    }


    public async Task SeedMovieCrew(TextReader reader)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        if (await context.MovieCrews.AnyAsync())
        {
            Console.WriteLine("MovieCrew data already exists. Skipping seeding.");
            return;
        }

        var movieMap = await context.Movies
            .AsNoTracking()
            .Select(m => new { m.IMDBId, m.Id })
            .ToDictionaryAsync(x => x.IMDBId, x => x.Id);

        var crewMap = await context.Crews
            .AsNoTracking()
            .Select(p => new { p.IMDBId, p.Id })
            .ToDictionaryAsync(x => x.IMDBId, x => x.Id);

        Console.WriteLine($"Maps loaded. Movies: {movieMap.Count}, Crew: {crewMap.Count}");
        Console.WriteLine("Reading principals file...");

        using var csv = new CsvReader(reader, _csvConfig);
        var principalsBatch = new List<MovieCrew>();
        var records = csv.GetRecords<IMDBTitlePrincipals>();

        int count = 0;

        foreach (var r in records)
        {
            if (!movieMap.TryGetValue(r.tconst, out int sqlMovieId))
            {
                continue;
            }

            if (!crewMap.TryGetValue(r.nconst, out int sqlPersonId))
            {
                continue;
            }

            var principal = new MovieCrew
            {
                Movie_Id = sqlMovieId,
                IdCrew = sqlPersonId,
                Job = r.category,

                CharacterName = (r.characters == "\\N" || string.IsNullOrWhiteSpace(r.characters))
                            ? null
                            : CleanCharacters(r.characters)
            };

            principalsBatch.Add(principal);
            count++;

            if (count % batchSize == 0)
            {
                Console.WriteLine($"Saving principals... {count}");
                await context.MovieCrews.AddRangeAsync(principalsBatch);
                await context.SaveChangesAsync();
                principalsBatch.Clear();
                context.ChangeTracker.Clear();
            }

            if (count >= maxRecords)
            {
                Console.WriteLine($"Max limit of {maxRecords} reached. Stopping import.");
                break;
            }
        }

        if (principalsBatch.Count != 0)
        {
            await context.MovieCrews.AddRangeAsync(principalsBatch);
            await context.SaveChangesAsync();
        }
    }

    private static int ParseNullableStringToInt(string? value)
    {
        if (string.IsNullOrEmpty(value) || value == "\\N") return 0;
        return int.TryParse(value, out var result) && result != 0 ? result : 0;
    }
    private static string? CleanCharacters(string? input)
    {
        if (string.IsNullOrEmpty(input) || input == "\\N") return null;
        return input.Replace("[", "").Replace("]", "").Replace("\"", "");
    }
}


