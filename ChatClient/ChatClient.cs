using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TChatClient
{
    public class ChatClient
    {
        private ChatMessage _chatMessage;
        private byte[]? sendData;
        private byte[]? receiveData;
        private string? messageText;

        public void SentMessage(string from, string ip)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 7000);

            while (true)
            {
                do
                {
                    Console.WriteLine("Введите сообщение");

                    messageText = Console.ReadLine()!.ToLower();
                    if (messageText.Equals("exit"))
                    {
                        return;
                    }
                }
                while (string.IsNullOrEmpty(messageText));

                _chatMessage = new ChatMessage() { Text = messageText, FromName = from, ToName = "Server", Command = Command.Message };

                string json = _chatMessage.ToJson();
                sendData = Encoding.UTF8.GetBytes(json);
                udpClient.Send(sendData, sendData.Length, iPEndPoint);


                receiveData = udpClient.Receive(ref iPEndPoint);

                messageText = Encoding.UTF8.GetString(receiveData);
                












                //var t = new Task(() =>
                //{
                //    receiveData = udpClient.Receive(ref iPEndPoint);
                //});
                //t.Start();

                //try
                //{
                //    t.Wait(1000);
                //    messageText = Encoding.UTF8.GetString(receiveData!);
                //    Console.WriteLine(messageText);
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("Сервер не отвечает");
                //}
            }
        }

    }
}
