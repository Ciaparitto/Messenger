using System.ComponentModel.DataAnnotations;

namespace Messenger.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string RecoveryCode { get; set; }

    }
}
