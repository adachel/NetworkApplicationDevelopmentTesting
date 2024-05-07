using ChatApp;
using ChatBD;
using ChatNetWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentTesting.Test
{
    public class UnitTestClient
    {
        //private ChatServer _server = new ChatServer(new MessageSource());
        //private FakeChatClient first;
        //private int _port = 0;
        //private string _address = "127.0.0.1";

        [SetUp]
        public void Setup()
        {
            //var server = new ChatServer(new MessageSource());
            //Task.Run(() => server.WorkAsync()).Wait(1000);
            //first = new FakeChatClient("Alex", new MessageSource(_port, _address));
            //Task.Run(() => first.Start()).Wait(1000);
        }

        [Test] // регистрация на сервере
        public void Test1()
        {
            var server = new ChatServer(new MessageSource());
            Task.Run(() => server.WorkAsync()).Wait(1000);
            var first = new FakeChatClient("Alex", new MessageSource(0, "127.0.0.1"));
            Task.Run(() => first.Start()).Wait(1000);
            Assert.IsTrue(server.clients.Count == 1);
        }


        [Test] // 
        public void Test2()
        {
            var server1 = new ChatServer(new MessageSource());
            Task.Run(() => server1.WorkAsync()).Wait(1000);

            var first = new FakeChatClient("Alex", new MessageSource(0, "127.0.0.1"));
            Task.Run(() => first.Start()).Wait(1000);

            var second = new FakeChatClient("Ivan", new MessageSource(0, "127.0.0.1"));
            second.ToName = "Alex";
            second.Text = "Hello Alex";
            Task.Run(() => second.Start()).Wait(1000);

            


        }


        [TearDown]
        public void Teardown()
        {
         
        }
    }
}
