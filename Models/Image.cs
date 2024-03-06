using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using messager.models;

namespace messager.Models
{
	public class Image
	{
		[Key]
		public int Id { get; set; }
		public Byte[] image { get; set; }
		public string ContentType { get; set; }
		[ForeignKey("UserId")]
		public string UserId { get; set; }
		public UserModel User { get; set; }

		[ForeignKey("MessageId")]
		public string? MessageId { get; set; }

		public MessageModel? Message { get; set; }
		[NotMapped]
		public string Tag { get; set; }
	}
}