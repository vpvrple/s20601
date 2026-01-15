namespace s20601.Services.External.Azure;

public interface IAzureSentimentAnalysisService
{
    Task<SentimentType> AnalyzeSentiment(IEnumerable<string> strings);
}
