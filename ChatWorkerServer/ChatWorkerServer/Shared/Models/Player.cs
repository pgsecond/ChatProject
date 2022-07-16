namespace Shared.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public Player(string id, string name, string color)
        {
            Id = id;
            Name = name;
            Color = color;
        }
    }
}
