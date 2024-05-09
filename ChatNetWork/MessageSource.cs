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

        
        private IPEndPoint _udpServerEndPoint; //

        public MessageSource(int port, string address, int portServer = 12345)           //
        {                                                                               //
            _udpClient = new UdpClient(port);                                           //
            _udpServerEndPoint = new IPEndPoint(IPAddress.Parse(address), portServer);   //
        }                                                                               //

        public MessageSource(int portServer = 12345)    //
        {                                               //
            _udpClient = new UdpClient(portServer);     //
        }                                               //





        //public MessageSource(int port)
        //{
        //    _udpClient = new UdpClient(port);
        //}

        public IPEndPoint CreateNewIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Any, 0);
        }


        public IPEndPoint GetServerIPEndPoint()                                            //
        {                                                                                  //
            return new IPEndPoint(_udpServerEndPoint.Address, _udpServerEndPoint.Port);    //
        }                                                                                  //





        public void SendMessage(ChatMessage chatMessage, IPEndPoint iPEndPoint)
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










    }
}
