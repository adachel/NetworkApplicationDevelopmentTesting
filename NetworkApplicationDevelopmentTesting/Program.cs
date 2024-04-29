using Castle.Components.DictionaryAdapter.Xml;
using ChatApp;
using ChatNetWork;

var messageSource = new MessageSource(0);
ChatServer chatServer = new ChatServer(messageSource);
Task task = chatServer.WorkAsync();
task.Wait();