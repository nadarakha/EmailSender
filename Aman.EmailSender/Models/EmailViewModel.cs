using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aman.EmailSender.Models
{
    public class EmailViewModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        public string SenderPassword { get; set; }
    }
}