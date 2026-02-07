using MediatR;

namespace s20601.Events.Queries;

public record GetTMDBPersonaImageQuery(string ImdbId) : IRequest<string?>;
