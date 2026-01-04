using MediatR;

namespace s20601.Events.Queries;

public record GetMoviePosterQuery(string ImdbId) : IRequest<string?>;
