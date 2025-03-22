using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models;
using s20601.Data.Models.DTOs;

namespace s20601.Services;

public class UserService : IUserService
{
    private readonly IDbContextFactory<S20601Context> _dbContextFactory;
    public UserService(IDbContextFactory<S20601Context> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<User>> GetTopUsersByPoints()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Users
               .OrderByDescending(x => x.ReputationPoints)
               .ToListAsync();
    }

    public async Task<UserRatingSummary?> GetUserRatingSummaryAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var ratings = await context.MovieRates
            .Where(x => x.IdUser == id)
            .ToListAsync();

        double avgRating = ratings.Any() ? ratings.Average(x => x.Rating) : 0;

        var totalRatings = ratings.Count();

        var medianRating = ratings
            .OrderBy(x => x.Rating)
            .Select(x => x.Rating)
            .ToList();

        double median = 0;

        if (medianRating.Count != 0)
        {
            var count = medianRating.Count();
            var middleValue = count / 2;

            if (count % 2 == 0)
            {
                median = (medianRating[middleValue - 1] + medianRating[middleValue]) / 2.0;
            }
            else
            {
                median = medianRating[middleValue];
            }
        }

        var totalReviews = await context.ReviewRates
            .Where(x => x.IdUser == id)
            .CountAsync();

        var ratingDistribution = ratings
            .GroupBy(x => x.Rating)
            .Select(distribution => new RatingDistribution
            {
                RatingValue = distribution.Key,
                Frequency = distribution.Count()
            })
            .OrderBy(x => x.RatingValue)
            .ToList();

        return new UserRatingSummary
        {
            AvgMovieRating = avgRating,
            MedMovieRating = median,
            TotalRatings = totalRatings,
            TotalReviews = totalReviews,
            RatingDistribution = ratingDistribution
        };
    }


}
