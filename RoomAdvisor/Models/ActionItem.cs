namespace RoomAdvisor.Models
{
    public class ActionItem
    {
        public string Room { get; set; }
        public string Action { get; set; }
        public string Duration { get; set; }
        public string Level { get; set; } // Optional, included only for heating
    }
}
