using Messenger.models;
using Messenger.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Messenger
{
	public class AppDbContext : IdentityDbContext<UserModel>
	{


		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}


		public DbSet<Image> ImageList { get; set; }

		public virtual DbSet<MessageModel> MessageList { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Image>()
			.Ignore(e => e.Tag);

			modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
			modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
			modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

		}



	}
}
