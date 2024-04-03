using Messenger;
using Messenger.models;
using Messenger.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace MessenerTests
{
	public class MessageServiceTest
	{
		[Fact]
		public async Task AddMessage()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			var DbContextMoq = new Mock<AppDbContext>(options);

			DbContextMoq.Setup(x => x.SaveChangesAsync(CancellationToken.None))
						.ReturnsAsync(1);
			var messageService = new MessageService(DbContextMoq.Object);

			var Content = "Content";
			var ReciverId = "ReciverId";
			var CreatorId = "CreatorId";

			var MessageId = await messageService.AddMessage(Content, ReciverId, CreatorId);

			Assert.NotNull(MessageId);
		
		}

	}
}