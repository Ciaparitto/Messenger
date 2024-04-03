using Messenger;
using Messenger.models;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace MessenerTests
{
	public class MessageServiceTest
	{
		[Fact]
		public async Task AddMessageTest()
		{
			var DbContextMock = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
			var Messages = new List<MessageModel>();
			var DbSetMock = new Mock<DbSet<MessageModel>>();

			DbSetMock.Setup(m => m.Add(It.IsAny<MessageModel>()))
				.Callback<MessageModel>(Messages.Add);
			
			DbContextMock.Setup(x => x.MessageList).Returns(DbSetMock.Object);

			DbContextMock.Setup(x => x.SaveChangesAsync(CancellationToken.None))
						.ReturnsAsync(1);

			var messageService = new MessageService(DbContextMock.Object);
			Assert.NotNull(DbContextMock.Object);
			Assert.NotNull(messageService);
			var Content = "Content";
			var ReciverId = "ReciverId";
			var CreatorId = "CreatorId";

			var MessageId = await messageService.AddMessage(Content, ReciverId, CreatorId);

			Assert.NotNull(MessageId);
			Assert.Equal(1,Messages.Count);
		} 

	}
}