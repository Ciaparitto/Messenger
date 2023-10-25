using System.ComponentModel.DataAnnotations;

namespace messager.models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Creatorid { get; set; }
        [Required]
        public string Reciverid { get; set; }

    }
}
