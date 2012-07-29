using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ri0t.Interfaces;

namespace Ri0t
{
    public class MessageManager
    {
        public const string SuccessMessage = "OK";

        private static List<Conversation> Conversations;

        private IGeocoder _geocoder;

        public MessageManager(IGeocoder geocoder)
        {
            _geocoder = geocoder;
            Conversations = new List<Conversation>();
        }


        /// <summary>
        /// Gets an existing conversation thread or starts a new one
        /// </summary>
        /// <param name="number">the mobile number (or other ID) of the conversee</param>
        /// <returns>A conversation</returns>
        public Conversation GetConversation(string number)
        {
            var existingConversation = Conversations.FirstOrDefault(c => c.Identity == number);

            var conversation = Conversations.FirstOrDefault(c => c.Identity == number);
            if (conversation == null)
            {
                conversation = new Conversation(_geocoder);
                conversation.Identity = number;
                Conversations.Add(conversation);
            }

            conversation.Messages = conversation.Messages ?? new List<string>();

            return conversation;
        }

        public static void Reset()
        {
            Conversations = new List<Conversation>();
        }

    }
}
