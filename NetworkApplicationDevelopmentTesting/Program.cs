using Castle.Components.DictionaryAdapter.Xml;
using ChatApp;
using ChatNetWork;

if (args.Length == 0)
{
    //var messageSource = new MessageSource(12345);
    var server = new ChatServer(new MessageSource());
    await server.WorkAsync();

}
else if (args.Length == 2)
{
    var client1 = new ChatClient(args[0], new MessageSource(int.Parse(args[1]), "127.0.0.1"));
    client1.Start();
}
else
{
    Console.WriteLine("Error. Input name and port.");
}