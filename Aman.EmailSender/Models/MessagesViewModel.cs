using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aman.EmailSender.Models
{
    public class MessagesViewModel
    {
        public List<Guid> MessagesId { get; set; }
        public string Emails { get; set; }
    }
}