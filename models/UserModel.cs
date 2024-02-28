using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using messager.models;
using System.ComponentModel.DataAnnotations;
namespace messager.models
{
    public class UserModel : IdentityUser
    {

        [Required]
        public int? ProfileImageId { get; set; }
        public bool IsOnline { get; set; }


    }
}
