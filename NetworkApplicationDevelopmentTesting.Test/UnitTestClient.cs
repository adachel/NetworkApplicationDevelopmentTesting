using Castle.DynamicProxy;
using ChatApp;
using ChatBD;
using ChatNetWork;
using CommonChat.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentTesting.Test
{
    public class UnitTestClient
    {
        private ChatMessage chatMessage;
        private IPEndPoint ipEndPoint;

        [SetUp]
        public void Setup()
        {
            chatMessage = new ChatMessage();
            ipEndPoint = new IPEndPoint(IPAddress.Parse("123.123.123.123"), 11111);
        }

        [Test] // Register
        public void Test1()
        {
            var mock = new Mock<IMessageSource>();

            mock.Setup(x => x.GetServerIPEndPoint()).Returns(ipEndPoint);

            var client = new ChatClient("name", mock.Object);

            client.Register();

            mock.Verify(x => x.SendMessage(It.IsAny<ChatMessage>(), ipEndPoint));
        }

        [Test] // Confirmation
        public void Test2() 
        {
            var mock = new Mock<IMessageSource>();

            var client = new ChatClient("name", mock.Object);

            client.Confirmation(chatMessage, ipEndPoint);

            mock.Verify(x => x.SendMessage(It.IsAny<ChatMessage>(), ipEndPoint));
        }

        [Test] // SendMessage
        public void Test3()
        {
            var mock = new Mock<IMessageSource>();

            var client = new ChatClient("name", mock.Object);

            client.SendMessage(chatMessage, ipEndPoint);

            mock.Verify(x => x.SendMessage(chatMessage, ipEndPoint));
        }

        [Test] // 
        public void Test4()
        {
            var mock = new Mock<IMessageSource>();

            mock.Setup(x => x.CreateNewIPEndPoint()).Returns(ipEndPoint);
            mock.Setup(x => x.Receive(ref ipEndPoint)).Returns(chatMessage);

            var client = new ChatClient("name", mock.Object);

            Task.Run(() => client.Listen()).Wait(1);

            mock.Verify(x => x.SendMessage(It.IsAny<ChatMessage>(), ipEndPoint));
        }



        [TearDown]
        public void Teardown()
        {
        }
    }
}

