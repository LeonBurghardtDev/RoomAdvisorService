using System.Text.Json;
using System.Text.RegularExpressions;

namespace RoomAdvisor.Helper
{
    public static class JsonHelper
    {
        public static string CleanJsonString(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                return string.Empty;

            try
            {
                JsonDocument.Parse(jsonString);
                return jsonString; 
            }
            catch
            { }

            try
            {
                Dictionary<string, string>? wrapper = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

                if (wrapper != null && wrapper.TryGetValue("text", out string? innerJson))
                {
                    jsonString = innerJson;
                }
            }
            catch
            { }

            jsonString = jsonString.Replace("\\\"", "\"")
                                   .Replace("\\n", "")
                                   .Replace("\\r", "")
                                   .Replace("\\t", "")
                                   .Replace("\\\\", "\\");

            jsonString = Regex.Replace(jsonString, @"```json|```", "", RegexOptions.IgnoreCase).Trim();

            if (jsonString.StartsWith("\"") && jsonString.EndsWith("\""))
            {
                jsonString = jsonString.Substring(1, jsonString.Length - 2);
            }

            if (!jsonString.StartsWith("{") && !jsonString.StartsWith("["))
            {
                jsonString = "{" + jsonString;
            }

            if (!jsonString.EndsWith("}") && !jsonString.EndsWith("]"))
            {
                jsonString += "}";
            }

            return jsonString.Trim();
        }
    }
}
