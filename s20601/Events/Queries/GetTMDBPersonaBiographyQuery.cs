using MediatR;

namespace s20601.Events.Queries;

public record GetTMDBPersonaBiographyQuery(string ImdbId) : IRequest<string?>;
