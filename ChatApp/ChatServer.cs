using ChatBD;
using ChatNetWork;
using CommonChat.DTO;
using CommonChat.Models;
using System.Net;

namespace ChatApp
{
    public class ChatServer
    {
        private IMessageSource _iMessageSource;

        private bool _isWork = true;

        public Dictionary<string, IPEndPoint> clients = new Dictionary<string, IPEndPoint>(); // словарь: имя и iPEndPoint

        public ChatServer(IMessageSource iMessageSource)
        {
            _iMessageSource = iMessageSource;
        }

        public async Task ProcessMessageAsync(ChatMessage chatMessage, IPEndPoint iPEndPoint)
        {
            switch (chatMessage.Command)
            {
                case Command.Message:
                    await ReplyMessageAsync(chatMessage);
                    break;
                case Command.Confirmation:
                    await ConfirmationAsync(chatMessage.Id);
                    break;
                case Command.Register:
                    await RegisterAsync(chatMessage, iPEndPoint);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task ReplyMessageAsync(ChatMessage chatMessage)
        {
            if (clients.TryGetValue(chatMessage.ToName!, out IPEndPoint? iPEndPoint))
            {
                using (var context = new ChatContext())
                {
                    var fromName = context.Users.FirstOrDefault(x => x.Name == chatMessage.FromName);
                    var toName = context.Users.FirstOrDefault(x => x.Name == chatMessage.ToName);
                    var message = new Message
                    {
                        Text = chatMessage.Text,
                        FromUser = fromName,
                        ToUser = toName,
                        IsReceived = false
                    };   
                    await context.Messages.AddAsync(message);
                    await context.SaveChangesAsync();

                    chatMessage.Id = message.Id;
                }

                _iMessageSource.SendMessage(chatMessage, iPEndPoint);

                Console.WriteLine($"Send messsage {chatMessage.FromName} to {chatMessage.ToName}");
            }
        }

        public async Task ConfirmationAsync(int? id)
        {
            Console.Out.WriteLine("Message id " + id);

            using (var context = new ChatContext())
            {
                var message = context.Messages.FirstOrDefault(x => x.Id == id);
                if (message != null)
                {
                    message.IsReceived = true;
                    await context.SaveChangesAsync();
                }
                
            }
        }

        public async Task RegisterAsync(ChatMessage chatMessage, IPEndPoint iPEndPoint)
        {
            Console.WriteLine($"Message register name {chatMessage.FromName}");
            clients.Add(chatMessage.FromName, iPEndPoint);      // добавление в Dictionary<string, IPEndPoint> clients

            using (var context = new ChatContext())
            {
                var conAny = context.Users.Any(x => x.Name == chatMessage.FromName); // сравнивает FromName с наличием в БД
                if (conAny) // если true, то такой пользов уже есть
                {
                    Console.WriteLine("User already exist in database");
                    return;
                }

                await context.Users.AddAsync(new User() { Name = chatMessage.FromName});    // добавляем User в БД
                await context.SaveChangesAsync();
            }
        }

        public void Stop()
        { 
            _isWork = false;
        }




        public async Task WorkAsync()
        {
            Console.WriteLine("Wait message from client");
            while (_isWork)
            {
                try
                {
                    var remoteIPEndPoint = _iMessageSource.CreateNewIPEndPoint(); // сервер должен контролировать действия клиентов
                                                                                  // на всех сетевых интерфейсах. По всем портам

                    var chatMessage = _iMessageSource.Receive(ref remoteIPEndPoint); // принимает сообщение с remoteIPEndPoint
                                                                                     // и сериализует его в объект ChatMessage

                    if (chatMessage != null) // если ChatMessage не пустой
                    {
                        await ProcessMessageAsync(chatMessage, remoteIPEndPoint);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace + " " + e.Message);
                }


            }
        }
    }
}
