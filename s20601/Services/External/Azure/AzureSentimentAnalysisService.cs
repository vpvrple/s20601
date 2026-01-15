using Azure.AI.TextAnalytics;

namespace s20601.Services.External.Azure;

public class AzureSentimentAnalysisService : IAzureSentimentAnalysisService
{
    private readonly TextAnalyticsClient _textAnalyticsClient;

    public AzureSentimentAnalysisService(TextAnalyticsClient textAnalyticsClient)
    {
        _textAnalyticsClient = textAnalyticsClient;
    }

    public async Task<SentimentType> AnalyzeSentiment(IEnumerable<string> strings)
    {

        if (strings.Any())
        {
            var response = await _textAnalyticsClient.AnalyzeSentimentBatchAsync(strings);

            foreach (var res in response.Value)
            {
                // 3. CHECK FOR ERRORS first!
                if (res.HasError)
                {
                    Console.WriteLine($"Document {res.Id} failed: {res.Error.Message}");
                    continue;
                }

                DocumentSentiment sentiment = res.DocumentSentiment;

                if (sentiment.Sentiment == TextSentiment.Positive)
                    return SentimentType.Positive;
                if (sentiment.Sentiment == TextSentiment.Negative)
                    return SentimentType.Negative;
                if (sentiment.Sentiment == TextSentiment.Neutral)
                    return SentimentType.Neutral;
            }
        }
        return SentimentType.Undefined;
    }
}

public enum SentimentType
{
    Undefined,
    Positive,
    Neutral,
    Negative,
}