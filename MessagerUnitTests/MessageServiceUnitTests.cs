using messager;
using messager.models;
using messager.Services;
using messager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents.SystemFunctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MessagerUnitTests
{
    public class MessageServiceUnitTests
    {
        private Mock<UserManager<UserModel>> userManagerMock;
        private Mock<SignInManager<UserModel>> signInManagerMock;
        private MessageService messageServiceMock;
        private Mock<AppDbContext> ContextMock;
        private Mock<DbSet<MessageModel>> MockSet;
        [SetUp]
        public void Setup()
        {
            var UserStoreMock = new Mock<IUserStore<UserModel>>();

            userManagerMock = new Mock<UserManager<UserModel>>(UserStoreMock.Object, null, null, null, null, null, null, null, null);

            var contextAccessor = new Mock<IHttpContextAccessor>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            MockSet = new Mock<DbSet<MessageModel>>();

            ContextMock = new Mock<AppDbContext>(options);
            ContextMock.Setup(x => x.MessageList).Returns(MockSet.Object);

            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<UserModel>>();

            signInManagerMock = new Mock<SignInManager<UserModel>>(userManagerMock.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null, null);

            messageServiceMock = new MessageService(ContextMock.Object);
        }
        [Test]
        public async Task AddMessage_AddingMessage()
        {
            
            var Content = "Content";
            var CreatorId = "1";
            var ReciverId = "2";
        
            
            await messageServiceMock.AddMessage(Content, ReciverId, CreatorId);


            MockSet.Verify(m => m.Add(It.Is<MessageModel>(msg => msg.Content == Content && msg.CreatorId == CreatorId && msg.ReciverId == ReciverId)),
      Times.Once);

            ContextMock.Verify(m => m.SaveChangesAsync(default), Times.Once());

        }
        [Test]
        public async Task GetMessages_GettingMessages()
        {
            var MessageList = new List<MessageModel>
            {
                new MessageModel{Id=1,Content= "content",CreatorId = "1",ReciverId = "2" },
                new MessageModel{Id=2,Content= "content",CreatorId = "2",ReciverId = "1" },
                new MessageModel{Id=3,Content= "content",CreatorId = "1",ReciverId = "3" },
                new MessageModel{Id=4,Content= "content",CreatorId = "3",ReciverId = "1" }
            }.AsQueryable();

            
            MockSet.As<IQueryable<MessageModel>>().Setup(m => m.Provider).Returns(MessageList.Provider);
            MockSet.As<IQueryable<MessageModel>>().Setup(m => m.Expression).Returns(MessageList.Expression);
            MockSet.As<IQueryable<MessageModel>>().Setup(m => m.ElementType).Returns(MessageList.ElementType);
            MockSet.As<IQueryable<MessageModel>>().Setup(m => m.GetEnumerator()).Returns(MessageList.GetEnumerator());

            ContextMock.Setup(x => x.MessageList).Returns(MockSet.Object);

            var result = await messageServiceMock.GetMessages("1");

            Assert.NotNull(result);
            Assert.True(result.Count == 2);
          
        }

    }
}
