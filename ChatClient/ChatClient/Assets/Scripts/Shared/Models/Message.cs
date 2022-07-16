using System;

namespace Shared.Models
{
    public class Message
    {
        public string PlayerId { get; set; }
        public string Text { get; set; }
        public DateTime DateCreate { get; set; }

        public Message(string playerId, string text)
        {
            PlayerId = playerId;
            Text = text;
            DateCreate = DateTime.Now;
        }
    }
}
