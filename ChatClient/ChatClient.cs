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
        private string _host = "127.0.0.1";
        private int _port = 7000;
        private UdpClient _udpClient;
        private IPEndPoint _iPEndPoint;

        private string _messageText;


        public ChatClient(string host, int port)
        {
            _host = host;
            _port = port;
            _udpClient = new UdpClient();
            _iPEndPoint = new IPEndPoint(IPAddress.Parse(_host), _port);
            _chatMessage = new ChatMessage();
        }

        public ChatClient()
        {
            _udpClient = new UdpClient();
            _iPEndPoint = new IPEndPoint(IPAddress.Parse(_host), _port);
            _chatMessage = new ChatMessage();
        }


        public async Task RunClient()
        {
            Console.WriteLine("Ваше имя");
            _chatMessage.FromName = Console.ReadLine();
            _chatMessage.Command = Command.Register;
            Send();

            await SendClientAsync();
            await ReceiveClientAsync();
        }

        public void StopClient()
        {

        }

        public bool ReadKey()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                Console.WriteLine(key.Key + " клавиша была нажата");
                return true;
            }
            while (key.Key != ConsoleKey.Escape); // по нажатию на Escape завершаем цикл
        }


        public async Task ReceiveClientAsync()
        {
            ChatMessage mes = new ChatMessage();

            mes = await ReceiveAsync();

            if (mes.FromName != null)
            {
                Console.WriteLine($"Получено сообщение от {mes.FromName}: {mes.Text}");
            }
        }


        public async Task SendClientAsync()
        {
            await Task.Run(() =>
            {
                while (ReadKey())
                {

                    Console.WriteLine("Кому сообщение");
                    _chatMessage.ToName = Console.ReadLine();


                    do
                    {
                        Console.WriteLine("Введите сообщение");
                        _messageText = Console.ReadLine();
                        if (_messageText.ToLower().Equals("exit"))
                        {
                            return;
                        }
                    }
                    while (string.IsNullOrEmpty(_messageText));


                    _chatMessage.Text = _messageText;
                    _chatMessage.Command = Command.Message;
                    Send();
                }
            });

        }

        public void Send()
        {
            byte[] data = Encoding.UTF8.GetBytes(_chatMessage.ToJson());
            _udpClient.SendAsync(data, data.Length, _iPEndPoint);
        }


        public async Task<ChatMessage> ReceiveAsync()
        {
            string jsonMessage = "";
            await Task.Run(() =>
            {
                byte[] data = _udpClient.Receive(ref _iPEndPoint);
                jsonMessage = Encoding.UTF8.GetString(data);

            });
            return ChatMessage.FromJson(jsonMessage);
        }

    }







}
