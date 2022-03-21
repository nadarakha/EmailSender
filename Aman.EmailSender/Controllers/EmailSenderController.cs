using Aman.EmailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Aman.EmailSender.Controllers
{
    public class EmailSenderController : Controller
    {
        Model1 DbContext = new Model1();
        // GET: EmailSender
        [HttpPost]
        public ActionResult SaveMessage(string subject, string text)
        {
            if (ModelState.IsValid)
            {
                DbContext.Messages.Add(new Message { Id = Guid.NewGuid(), Subject = subject, Text = text });
                DbContext.SaveChanges();
            }

            return View("CreateMessage");
        }

        // GET: EmailSender/Create
        public ActionResult CreateMessage()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult GetMessages()
        {
            return PartialView(DbContext.Messages.ToList());
        }

        public ActionResult SendMessages( List<Guid>selectedMessagesId)
        {
            var messageViewModel=new MessagesViewModel{MessagesId=selectedMessagesId};

            return View("SendEmail",messageViewModel);
        }
        [HttpPost]
        public ActionResult SendEmail(MessagesViewModel messagesViewModel)
        {
            var emails = messagesViewModel.Emails.Split(',');
            var messages = DbContext.Messages.Where(m=> messagesViewModel.MessagesId.Contains(m.Id)).ToList();
            var emailViewModel = new EmailViewModel{SenderEmail= "nadarakha19@gmail.com",SenderName="Aman",SenderPassword="" };

            foreach(var email in emails)
            {foreach( var msg in messages)
                {
                    emailViewModel.Subject = msg.Subject;
                    emailViewModel.ReceiverEmail = email;
                    emailViewModel.Body = msg.Text;
                    SendEmails(emailViewModel);
                }
            }

            return View();
        }


        [HttpPost]
        public ActionResult SendEmails(EmailViewModel email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress(email.SenderEmail, email.SenderName);
                    var receiverEmail = new MailAddress(email.ReceiverEmail, email.ReceiverName);
                    var password = email.SenderPassword;
                    var sub = email.Subject;
                    var body = email.Body;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    
                }
            }
            catch (Exception)
            {
                return View("SendEmail");
            }
            return View("CreateMessage");
        }

    }
}
