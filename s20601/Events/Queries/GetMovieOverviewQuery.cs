using MediatR;

namespace s20601.Events.Queries;

public record GetMovieOverviewQuery(string ImdbId) : IRequest<string?>;
