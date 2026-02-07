using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Queries;

public record GetAzureReviewsSentimentQuery(IEnumerable<string> reviews) : IRequest<SentimentType>;
