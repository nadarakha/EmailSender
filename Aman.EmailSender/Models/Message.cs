using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aman.EmailSender.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }
    }
}