using Messenger;
using Messenger.models;
using Messenger.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessenerTests
{
	public class MessageGetterTest
	{
		[Fact]
		public void GetMessagesTest()
		{
			var ContextMock = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
			var Messages = new List<MessageModel>();
			var DbSetMock = new Mock<DbSet<MessageModel>>();
			ContextMock.Setup(x => x.MessageList).Returns(DbSetMock.Object);
			var _MessageGetter = new MessageGetter(ContextMock.Object);
			
			var ExampleMessage = new MessageModel{
				Content = "ExampleContent",
				ReciverId = "ReceiverId",
				CreatorId = "CreatorId"
			};

			var ExampleMessage2 = new MessageModel
			{
				Content = "ExampleContent2",
				ReciverId = "CreatorId",
				CreatorId = "ReceiverId"
			};
			Messages.Add(ExampleMessage);
			Messages.Add(ExampleMessage2);

			var Message = _MessageGetter.GetMessages("CreatorId", "ReceiverId").Result;
		
			Assert.Single(Messages);
			Assert.Equal("ExmapleContent",Message.FirstOrDefault().Content);
		}

	}

}
