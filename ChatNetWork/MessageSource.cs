using CommonChat.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatNetWork
{
    public class MessageSource : IMessageSource
    {
        private UdpClient _udpClient;

        public MessageSource(int port)
        {
            _udpClient = new UdpClient(port);
        }

        public void Send(ChatMessage chatMessage, IPEndPoint iPEndPoint)
        {
            byte[] data = Encoding.UTF8.GetBytes(chatMessage.ToJson());
            _udpClient.Send(data, data.Length, iPEndPoint);
        }

        public ChatMessage Receive(ref IPEndPoint iPEndPoint)
        {
            byte[] data = _udpClient.Receive(ref iPEndPoint);
            string jsonMessage = Encoding.UTF8.GetString(data);
            return ChatMessage.FromJson(jsonMessage);
        }

        public IPEndPoint CreateNewIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Any, 0); // сервер должен контролировать действия клиентов на всех сетевых интерфейсах.
        }

    }
}
