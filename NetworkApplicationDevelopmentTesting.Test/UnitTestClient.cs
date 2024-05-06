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
        private ChatServer _server = new ChatServer(new MessageSource());
        private int _port = 0;
        private string _address = "127.0.0.1";

        [SetUp]
        public void Setup()
        {
            _server.clients.Clear();
        }

        [Test] // регистрация на сервере
        public void Test1()
        {
            Task.Run(() => _server.WorkAsync() ).Wait(1000);

            var сlient = new ChatClient("Alex", new MessageSource(_port, _address));
            Task.Run(() => сlient.Start()).Wait(1000);

            Task.Run(() => Assert.IsTrue(_server.clients.Count == 1)).Wait(1000);
        }


        [Test] // 
        public void Test2()
        {
            Task.Run(() => _server.WorkAsync()).Wait(1000);


        }


        [TearDown]
        public void Teardown()
        {
            _server.clients.Clear();
        }
    }
}
