using ChatApp;
using ChatNetWork;
using CommonChat.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentTesting.Test
{
    public class StubsChatClient : ChatClient
    {
        private ChatMessage _chatMessage = new ChatMessage();
        public StubsChatClient(string name, IMessageSource messageSource) : base(name, messageSource)
        {
            _chatMessage.FromName = name;
            //_chatMessage.ToName = 
        }







    }
}
