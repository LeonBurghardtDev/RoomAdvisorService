using Microsoft.AspNetCore.Mvc.Rendering;
using Mscc.GenerativeAI;
using Newtonsoft.Json;
using RoomAdvisor.Helper;
using RoomAdvisor.Models;

namespace RoomAdvisor.Services
{
    public class GeminiRecommendationService
    {
        private readonly string _apiKey;
        private readonly GenerativeModel _geminiModel;
        

        public GeminiRecommendationService(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _geminiModel = InitializeGeminiModel();
        }

        private GenerativeModel InitializeGeminiModel()
        {
            GoogleAI googleAI = new GoogleAI(_apiKey);
            return googleAI.GenerativeModel(Model.Gemini15Pro);
        }

        public AiRecommendationResponse GetAiRecommendation(string query)
        {
            try
            {
                return ProcessAiRequest(query).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<AiRecommendationResponse> ProcessAiRequest(string query)
        {
            try
            {
                GenerateContentResponse response = await _geminiModel.GenerateContent(query);

                if (response?.Text == null)
                {
                    throw new Exception("Failed to generate content: Model Response is null or empty");
                }
                string cleanedResponseText = JsonHelper.CleanJsonString(response.Text);

                AiRecommendationResponse? aiResponse = JsonConvert.DeserializeObject<AiRecommendationResponse>(cleanedResponseText);
                if (aiResponse == null)
                {
                    throw new Exception("Failed to deserialize AI recommendation response");
                }

                return aiResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
