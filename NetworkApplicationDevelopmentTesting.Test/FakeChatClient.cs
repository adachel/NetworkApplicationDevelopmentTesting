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
    public class FakeChatClient
    {
        private readonly string _name;
        private readonly IMessageSource _messageSource;
        private readonly IPEndPoint _serverEndPoint;


        public string FromName { get; set; } = "";
        public string ToName { get; set; } = "";
        public string FromMessage { get; set; } = "";
        public string ToMessage { get; set; } = "";
        public ChatMessage MessageConfirm { get; set; } 


        public FakeChatClient(string name, IMessageSource messageSource)
        {
            _name = name;
            _messageSource = messageSource;
            _serverEndPoint = messageSource.GetServerIPEndPoint();
        }

        public void SendMessage(ChatMessage chatMessage, IPEndPoint ip)
        {
            _messageSource.SendMessage(chatMessage, ip);
        }

        public void Register()
        {
            var registerChat = new ChatMessage() { Command = Command.Register, FromName = _name };
            _messageSource.SendMessage(registerChat, _serverEndPoint);
        }

        public void ProcessSendMessage()
        {
            int count = 0;

            while (count < 1)
            {
                Console.WriteLine("Input receiver's name");
                var receiver = ToName;
                Console.WriteLine("Input your message");
                var text = ToMessage;
                var chatMessage = new ChatMessage()
                { Command = Command.Message, FromName = _name, ToName = receiver, Text = text };

                SendMessage(chatMessage, _serverEndPoint);

                count++;
            }
        }

        public void Listen()
        {
            var ip = _messageSource.CreateNewIPEndPoint();
            Register();

            while (true)
            {
                var data = _messageSource.Receive(ref ip);
                Console.WriteLine($"Сообщение получено от {data.FromName}: \n{data.Text}");

                FromName = data.FromName;
                FromMessage = data.Text;

                Confirmation(data, ip);
            }
        }

        public void Confirmation(ChatMessage chatMessage, IPEndPoint ip)
        {
            var message = new ChatMessage() { Command = Command.Confirmation, Id = chatMessage.Id };
            MessageConfirm = message;
            SendMessage(message, ip);
        }

        public void Start()
        {
            new Thread(() => ProcessSendMessage()).Start();
            Listen();
        }
    }
}
