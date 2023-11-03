using messager.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace messager
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
      
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Creator)
                .WithMany()
                .HasForeignKey(m => m.Creatorid)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Reciver)
                .WithMany()
                .HasForeignKey(m => m.Reciverid)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            
        }
    }
}
