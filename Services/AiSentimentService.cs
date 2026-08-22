namespace GordonApi.Services
{
    /// <summary>
    /// Analyzes ticket sentiment using Hugging Face AI models
    /// </summary>
    public class AiSentimentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AiSentimentService> _logger;

        public AiSentimentService(IHttpClientFactory httpClientFactory, ILogger<AiSentimentService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Analyzes ticket text for sentiment (positive, negative, neutral)
        /// </summary>
        public async Task<(string sentiment, double confidence)> AnalyzeSentimentAsync(string ticketText)
        {
            try
            {
                _logger.LogInformation("Analyzing sentiment for ticket text");
                
                // Placeholder implementation
                // In production, this would call Hugging Face API
                var sentiment = "neutral";
                var confidence = 0.85;
                
                if (ticketText.Contains("angry", StringComparison.OrdinalIgnoreCase) ||
                    ticketText.Contains("frustrated", StringComparison.OrdinalIgnoreCase))
                {
                    sentiment = "negative";
                    confidence = 0.9;
                }
                else if (ticketText.Contains("thank", StringComparison.OrdinalIgnoreCase) ||
                         ticketText.Contains("great", StringComparison.OrdinalIgnoreCase))
                {
                    sentiment = "positive";
                    confidence = 0.88;
                }

                return (sentiment, confidence);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing sentiment");
                return ("unknown", 0.0);
            }
        }
    }
}
