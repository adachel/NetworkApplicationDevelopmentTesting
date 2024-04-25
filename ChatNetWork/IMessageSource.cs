using CommonChat.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatNetWork
{
    public interface IMessageSource
    {
        void Send(ChatMessage chatMessage, IPEndPoint iPEndPoint);
        ChatMessage Receive(ref IPEndPoint iPEndPoint);   
    }
}
