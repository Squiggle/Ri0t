using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ri0t.Geocoder.Mapquest;

namespace Ri0t
{
    public class Service
    {
        private static MessageManager _messageManager;
        private MessageManager MessageManager
        {
            get
            {
                if (_messageManager == null)
                {
                    _messageManager = new MessageManager(new MapquestGeocoder());
                }
                return _messageManager;
            }
        }

        public static void Reset()
        {
            _messageManager = null;
        }


        public void Inbound(string message, string number)
        {
            //load the conversation
            var conversation = MessageManager.GetConversation(number);

            conversation.Add(message);

            var nextPrompt = conversation.GetNext();

            //continue the conversation
        }

        //this is where we plug all the bits together
    }
}
