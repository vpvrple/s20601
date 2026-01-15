using MediatR;

namespace s20601.Events.Queries;

public record GetTMDBMovieImageQuery(string ImdbId) : IRequest<string?>;
