using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using messager.models;
namespace messager.models
{
    public class UserModel : IdentityUser
    {

      
        public int? ProfileImageId { get; set; }


    }
}
