using MediatR;
using s20601.Services;

namespace s20601.Events.Commands;

public class MovieRatingsEventsHandler
    : INotificationHandler<MovieRatedCommand>,
        INotificationHandler<MovieUnratedCommand>

{
    private readonly IMovieCollectionService _movieCollectionService;

    public MovieRatingsEventsHandler(IMovieCollectionService movieCollectionService)
    {
        _movieCollectionService = movieCollectionService;
    }

    public async Task Handle(MovieRatedCommand command, CancellationToken cancellationToken)
    {
        await _movieCollectionService.AddMovieToMyRatingsCollection(command.movieId, command.userId);
    }

    public async Task Handle(MovieUnratedCommand command, CancellationToken cancellationToken)
    {
        await _movieCollectionService.RemoveMovieFromMyRatingsCollection(command.movieId, command.userId);
    }
}