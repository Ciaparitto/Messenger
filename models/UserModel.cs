using Microsoft.AspNetCore.Identity;
namespace messager.models
{
	public class UserModel : IdentityUser
	{

		public int? ProfileImageId { get; set; }
		public bool IsOnline { get; set; }


	}
}
