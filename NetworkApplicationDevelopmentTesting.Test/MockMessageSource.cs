using ChatApp;
using ChatNetWork;
using CommonChat.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentTesting.Test
{
    public class MockMessageSource : IMessageSource
    {
        private Queue<ChatMessage> _messages = new Queue<ChatMessage>();

        private ChatServer _chatServer;

        private IPEndPoint _iPEndPoint = new IPEndPoint(IPAddress.Any, 0);


        public ChatServer ChatServer { get => _chatServer; set => _chatServer = value; }


        public void InitializeServer(ChatServer chatServer)
        {
            ChatServer = chatServer;
        }

        public MockMessageSource()
        {
            _messages.Enqueue(new ChatMessage() { Command = Command.Register, FromName = "Alex" });
            _messages.Enqueue(new ChatMessage() { Command = Command.Register, FromName = "Ivan" });
            _messages.Enqueue(new ChatMessage() { Command = Command.Message, FromName = "Alex", ToName = "Ivan", Text = "Hello Ivan" });
            _messages.Enqueue(new ChatMessage() { Command = Command.Message, FromName = "Ivan", ToName = "Alex", Text = "Hello Alex" });
        }

        public IPEndPoint CreateNewIPEndPoint()
        {
            return null!;
        }

        public ChatMessage Receive(ref IPEndPoint iPEndPoint)
        {
            _iPEndPoint = iPEndPoint;
            if (_messages.Count == 0)
            {
                ChatServer.Stop();
                return null!;
            }
            return _messages.Dequeue();
        }








        public void Send(ChatMessage chatMessage, IPEndPoint iPEndPoint)
        {
            // throw new NotImplementedException();
        }

        public void SendMessage(ChatMessage chatMessage, IPEndPoint iPEndPoint)
        {
            throw new NotImplementedException();
        }

        public IPEndPoint GetServerIPEndPoint()
        {
            throw new NotImplementedException();
        }
    }
}
