using MediatR;

namespace s20601.Events.Queries;

public record GetTMDBMovieOverviewQuery(string ImdbId) : IRequest<string?>;
