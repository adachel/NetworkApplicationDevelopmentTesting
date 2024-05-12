using ChatApp;
using ChatBD;
using ChatNetWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentTesting.Test
{
    public class UnitTestClient
    {
        //private ChatServer _server = new ChatServer(new MessageSource());
        [SetUp]
        public void Setup()
        {
            //Task.Run(() => _server.WorkAsync()).Wait(1000);
        }


        [Test] // регистрация
        public void TestRegistration()
        {
            var _server = new ChatServer(new MessageSource(2222));
            Task.Run(() => _server.WorkAsync());

            var client = new FakeChatClient("Den", new MessageSource(0, "127.0.0.1", 2222));
            Task.Run(() => client.Start()).Wait(1000);

            Assert.IsTrue(_server.clients.Count == 1);
            Assert.IsTrue(_server.clients.Keys.First() == "Den");
        }


        [Test] // 
        public void TestMessage()
        {

            var server = new ChatServer(new MessageSource(1111));
            Task.Run(() => server.WorkAsync());

            var first = new FakeChatClient("Tom", new MessageSource(0, "127.0.0.1", 1111));
            Task.Run(() => first.Start()).Wait(1000);

            var second = new FakeChatClient("Rex", new MessageSource(0, "127.0.0.1", 1111));
            second.ToName = "Tom";
            second.ToMessage = "Hi Tom";
            Task.Run(() => second.Start()).Wait(1000);

            Assert.IsTrue(first.FromName == "Rex");
            Assert.IsTrue(first.FromMessage == "Hi Tom");
        }

        [Test] // 
        public void TestConfirmation()
        {
            var server = new ChatServer(new MessageSource(5757));
            Task.Run(() => server.WorkAsync());

            var first = new FakeChatClient("Tom", new MessageSource(0, "127.0.0.1", 5757));
            Task.Run(() => first.Start()).Wait(1000);

            var second = new FakeChatClient("Rex", new MessageSource(0, "127.0.0.1", 5757));
            second.ToName = "Tom";
            second.ToMessage = "Hi Tom";
            Task.Run(() => second.Start()).Wait(2000);

            Assert.IsNotNull(first.MessageConfirm);
        }

        [TearDown]
        public void Teardown()
        {
            //_server.clients.Clear();
        }
    }
}

