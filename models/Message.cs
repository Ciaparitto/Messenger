using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace messager.models
{
    public class Message
    {

        //zrob migracje
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("Creatorid")]
        public UserModel Creator { get; set; }

        [Required]
        public string Creatorid { get; set; }

        [ForeignKey("Reciverid")]
        public UserModel Reciver { get; set; }

        [Required]
        public string Reciverid { get; set; }
    }

    

}
