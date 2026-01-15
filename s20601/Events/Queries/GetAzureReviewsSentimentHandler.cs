using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Queries;

public class GetAzureReviewsSentimentHandler(IAzureSentimentAnalysisService azureSentimentAnalysis) : IRequestHandler<GetAzureReviewsSentimentQuery, SentimentType>
{
    public async Task<SentimentType> Handle(GetAzureReviewsSentimentQuery request, CancellationToken cancellationToken)
    {
        var sentiment = await azureSentimentAnalysis.AnalyzeSentiment(request.reviews);

        return sentiment;
    }
}
