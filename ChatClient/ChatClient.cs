using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TChatClient
{
    internal class ChatClient
    {
        private ChatMessage? message;
        private byte[]? sendData;
        private byte[]? receiveData;
        private string? messageText;

        public string Name { get; set; }
        public string ToName { get; set; }



        public void SentMessage(string From, string ip)
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

                message = new ChatMessage() { FromName = Name, ToName = ToName, Text = messageText };

                string json = message.ToJson();
                sendData = Encoding.UTF8.GetBytes(json);
                udpClient.Send(sendData, sendData.Length, iPEndPoint);


                var t = new Task(() =>
                {
                    receiveData = udpClient.Receive(ref iPEndPoint);
                });
                t.Start();

                try
                {
                    t.Wait(1000);
                    messageText = Encoding.UTF8.GetString(receiveData!);
                    Console.WriteLine(messageText);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Сервер не отвечает");
                }
            }
        }
    }
}
