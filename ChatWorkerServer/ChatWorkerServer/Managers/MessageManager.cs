using System.Collections.Generic;
using ChatWorkerServer.Utils;
using Shared.Models;

namespace ChatWorkerServer.Managers
{
    public static class MessageManager
    {
        private static readonly FixedSizedQueue<Message> MessageList = new FixedSizedQueue<Message>(20);

        public static List<Message> GetLastMessageList => MessageList.ToList();

        public static void AddMessage(Message message)
        {
            MessageList.Enqueue(message);
        }
    }
}
