using System.ComponentModel.DataAnnotations;

namespace messager.models
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
