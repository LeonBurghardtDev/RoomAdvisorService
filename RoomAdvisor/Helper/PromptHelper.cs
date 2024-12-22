using RoomAdvisor.Constants;
using RoomAdvisor.Models;
using System.Text;

namespace RoomAdvisor.Helper
{
    public class PromptHelper
    {
        public static string GetPromptWithData(List<Room> rooms, double weatherTemperature, double weatherHumidity)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Constants.Prompt.PrefixPrompt);
            builder.Append("\r\nCurrent data:\r\n");
            builder.Append("{\r\n");
            builder.Append($"  \"weather\": {{ \"temperature\": {weatherTemperature}, \"humidity\": {weatherHumidity} }},\r\n");
            builder.Append("  \"rooms\": [\r\n");

            for (int i = 0; i < rooms.Count; i++)
            {
                Room room = rooms[i];
                builder.Append("    {\r\n");
                builder.Append($"      \"name\": \"{room.Name}\",\r\n");
                builder.Append($"      \"temperature\": {room.Temperature},\r\n");
                builder.Append($"      \"humidity\": {room.Humidity}\r\n");
                builder.Append("    }");
                if (i < rooms.Count - 1)
                {
                    builder.Append(",\r\n");
                }
                else
                {
                    builder.Append("\r\n");
                }
            }

            builder.Append("  ]\r\n");
            builder.Append("}\r\n");

            return builder.ToString();
        }
    }
}
