using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace messager.models
{
    public class MessageModel
    {


        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [Required]
        public string ReciverId { get; set; }

        [ForeignKey("ReciverId")]
        public UserModel Reciver { get; set; }
        public bool IsRead { get; set; }
    }



}
