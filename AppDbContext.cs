using messager.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace messager
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


       
        
        public DbSet<MessageModel> MessageList { get; set; }

    }
}
