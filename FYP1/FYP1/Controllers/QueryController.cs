using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Microsoft.AspNetCore.Http;
using FYP1.Models;
using Microsoft.EntityFrameworkCore;

namespace FYP1.Controllers
{
    public class QueryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public QueryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool checkUser()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return false;
            }
            return true;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.query.ToListAsync());
        }
        [HttpPost]
        public IActionResult SendEmailToWebTiqs(string ContactUserName, string ContactUserEmail,string ContactUserMessage,string ContactUserPassword)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            try
            {
                CreateContactQuery(ContactUserName, ContactUserEmail, ContactUserMessage);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(ContactUserEmail));
                message.To.Add(new MailboxAddress("webtiqs@gmail.com"));
                message.Subject = "Webtiqs";
                message.Body = new TextPart("plain")
                {
                    Text = ContactUserMessage
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(ContactUserEmail, ContactUserPassword);
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();

                }
                ResponseMessage(ContactUserEmail, ContactUserName);
            }
            catch (SmtpCommandException)
            {
                ViewBag.errorMsg = "Invalid credentials";
            }
            return RedirectToAction("index", "home");
        }

        private void ResponseMessage(string receiverEmail, string receiverName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("webtiqs@gmail.com"));
            message.To.Add(new MailboxAddress(receiverEmail));
            message.Subject = "From Webtiqs";
            message.Body = new TextPart("plain")
            {
                Text = "Thanks for contacting us " + receiverName + ". We will get back to you as soon as possible"
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("webtiqs@gmail.com", "Webtiqs_1");
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

            }
        }

        private void CreateContactQuery(string ContactUserName, string ContactUserEmail, string UserMsg)
        {
            Query query = new Query();
            query.queryUserName = ContactUserName;
            query.queryUserEmail = ContactUserEmail;
            query.userMessage = UserMsg;
            string userID = "userID";
            query.Id = HttpContext.Session.GetString(userID);

            _context.query.Add(query);
            _context.SaveChangesAsync();
        }

        [HttpPost]
        public IActionResult ReplyEmail(string ContactUserName,string ContactUserEmail, string ReplyMsg)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("webtiqs@gmail.com"));
            message.To.Add(new MailboxAddress(ContactUserEmail));
            message.Subject = "From Webtiqs";
            message.Body = new TextPart("plain")
            {
                Text = "Thanks for contacting us " + ContactUserName + ". Here is the answer of your query :" + ReplyMsg
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("webtiqs@gmail.com", "Webtiqs_1");
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

            }
            return RedirectToAction("index", "WebAdmin");
        }

        [HttpPost]
        public IActionResult SendEmailToWebTiqsUserPortal(string ContactUserName, string ContactUserEmail, string ContactUserMessage, string ContactUserPassword)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }

            CreateContactQuery(ContactUserName, ContactUserEmail, ContactUserMessage);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(ContactUserEmail));
            message.To.Add(new MailboxAddress("webtiqs@gmail.com"));
            message.Subject = "Webtiqs";
            message.Body = new TextPart("plain")
            {
                Text = ContactUserMessage
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(ContactUserEmail, ContactUserPassword);
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

            }
            ResponseMessage(ContactUserEmail, ContactUserName);

            return RedirectToAction("index", "user");
        }
    }
}