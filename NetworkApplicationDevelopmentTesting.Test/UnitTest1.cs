using ChatApp;
using ChatBD;
using CommonChat.DTO;

namespace NetworkApplicationDevelopmentTesting.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //using (var context = new ChatContext())
            //{
            //    context.Messages.RemoveRange(context.Messages);
            //    context.Users.RemoveRange(context.Users);
            //    context.SaveChanges();
            //}
        }

        [Test]
        public async Task Test1()
        {
            var mock = new MockMessageSource();
            var server = new ChatServer(mock);  
            mock.InitializeServer(server);
            await server.WorkAsync();
            using (var context = new ChatContext())
            {
                Assert.IsTrue(context.Users.Count() == 2);

                var first = context.Users.FirstOrDefault(x => x.Name == "Alex");
                var second = context.Users.FirstOrDefault(x => x.Name == "Ivan");

                Assert.IsNotNull(first);
                Assert.IsNotNull(second);

                Assert.IsTrue(first.FromMessages!.Count == 1);
                Assert.IsTrue(second.FromMessages!.Count == 1);

                Assert.IsTrue(first.ToMessages!.Count == 1);
                Assert.IsTrue(second.ToMessages!.Count == 1);

                var messageOne = context.Messages.FirstOrDefault(x => x.FromUser!.Id == first.Id && x.ToUser!.Id == second.Id);
                var messageTwo = context.Messages.FirstOrDefault(x => x.FromUser!.Id == second.Id && x.ToUser!.Id == first.Id);
                
                Assert.AreEqual("Hello Ivan", messageOne.Text);
                Assert.AreEqual("Hello Alex", messageTwo.Text);
            }
        }

        [TearDown]
        public void Teardown() 
        {
            using (var context = new ChatContext())
            {
                context.Messages.RemoveRange(context.Messages);
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
            }
        }
    }      
}