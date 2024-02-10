﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Documents;
using messager.models;

namespace messager.Models
{
	public class Image
	{
		[Key]
		public int id { get; set; }
		public Byte[] image { get; set; }
		public string ContentType { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

		public UserModel User { get; set; }
	}
}