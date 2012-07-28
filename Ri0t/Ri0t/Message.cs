using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeolocateMe
{
    public static class Message
    {
        public const string SuccessMessage = "OK";

        public static List<Conversation> Conversations;

        static Message()
        {
            Conversations = new List<Conversation>();
        }

        public static string Receive(string message, string number)
        {
            //the logic of conversation goes here
            //continue having a conversation until you know:
            //1. The message
            //2. The location


            //note: this stuff is a bit hacky just to make tests pass right now
            var conversation = Conversations.FirstOrDefault(c => c.Identity == number);
            if (conversation == null)
            {
                conversation = new Conversation
                {
                    Identity = number
                };
                Conversations.Add(conversation);
            }

            conversation.Messages = conversation.Messages ?? new List<string>();
            conversation.Messages.Add(message);

            if (conversation.Messages.Count > 1)
            {
                return "Hello again";
            }
            return "hi";
        }

        public static void Reset()
        {
            Conversations = new List<Conversation>();
        }

    }
}
