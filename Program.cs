using messager;
using messager.models;
using messager.Services;
using messager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSignalR(options =>
{
	options.EnableDetailedErrors = true;
	options.KeepAliveInterval = TimeSpan.FromSeconds(20);
	options.ClientTimeoutInterval = TimeSpan.FromSeconds(20);
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IMessageGetter, MessageGetter>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUserGetter, UserGetter>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7097/") });


builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(@"Data Source=DESKTOP-R5C9EQ0\SQLEXPRESS;Initial Catalog=DbMessenger;Integrated Security=True"),
	ServiceLifetime.Scoped);
builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 2;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();
app.UseRouting();
app.MapHub<AppHub>("/testhub");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
