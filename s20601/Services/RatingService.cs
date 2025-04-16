﻿using Microsoft.EntityFrameworkCore;
using s20601.Data;
using s20601.Data.Models.DTOs;
using s20601.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace s20601.Services;

public class RatingService : IRatingService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public RatingService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<UserRatingSummary?> GetUserRatingSummaryAsync(string userId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var ratings = await context.MovieRates
            .Where(x => x.IdUser == userId)
            .ToListAsync();

        double avgRating = ratings.Count != 0 ? ratings.Average(x => x.Rating) : 0;

        var totalRatings = ratings.Count;

        var medianRating = ratings
            .OrderBy(x => x.Rating)
            .Select(x => x.Rating)
            .ToList();

        double median = 0;

        if (medianRating.Count != 0)
        {
            var count = medianRating.Count;
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

        var totalReviews = await context.Reviews
            .Where(x => x.IdAuthor == userId)
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

    public async Task RateMovieAsync(int movieId, string userId, int rating)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var currentRating = await context.MovieRates
            .FirstOrDefaultAsync(x => x.Movie_Id == movieId && x.IdUser == userId);

        if (rating == 0 && currentRating is not null)
        {
            context.MovieRates.Remove(currentRating);
        }
        else if (currentRating is not null)
        {
            currentRating.Rating = rating;
            currentRating.RatedAt = DateTime.UtcNow;
            context.Update(currentRating);
        }
        else
        {
            var newRating = new MovieRate
            {
                Movie_Id = movieId,
                Rating = rating,
                RatedAt = DateTime.UtcNow,
                IdUser = userId
            };
            context.MovieRates.Add(newRating);
        }
        await context.SaveChangesAsync();
    }

    public async Task<MovieRatingSummary?> GetMovieRatingSummaryByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var ratings = context.MovieRates
            .Where(x => x.Movie_Id == id);

        if (ratings.IsNullOrEmpty())
        {
            return new MovieRatingSummary();
        }

        return new MovieRatingSummary
        {
            AvgRating = await ratings.AverageAsync(x => x.Rating),
            RateCount = await ratings.CountAsync()
        };
    }

    public async Task<MovieRate> GetUserMovieRating(string userId, int movieId)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        return await context.MovieRates.FirstOrDefaultAsync(x => x.IdUser == userId && x.Movie_Id == movieId);
    }

    public async Task<List<MovieWithRating>> GetTopMoviesByRatingAsync(int n)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var top100 = await context.Movies
            .Join(context.MovieRates,
            x => x.Id,
            y => y.Movie_Id,
            (x, y) => new
            {
                x,
                y
            })
            .GroupBy(joined => joined.x.Id)
            .Select(movieWithRating => new MovieWithRating
            {
                Id = movieWithRating.Key,
                Title = movieWithRating.Select(x => x.x.Title).First(),
                StartYear = movieWithRating.Select(x => x.x.StartYear).First(),
                Runtime = movieWithRating.Select(x => x.x.RuntimeMinutes).First(),
                MovieRatingSummary = new()
                {
                    AvgRating = movieWithRating.Average(x => x.y.Rating),
                    RateCount = movieWithRating.Count(),
                }
            })
            .OrderByDescending(x => x.MovieRatingSummary.AvgRating)
            .Take(n)
            .ToListAsync();


        return top100;
    }
}