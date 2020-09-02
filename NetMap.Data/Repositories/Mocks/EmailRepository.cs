using MailKit.Net.Smtp;
using MimeKit;
using NetMap.Data.Data;
using NetMap.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetMap.Data.Repositories.Mocks
{
    public class EmailRepository : IEmailRepository
    {
        private readonly NetMapContext _db;
        public EmailRepository(NetMapContext db)
        {
            _db = db;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("NetMap", "netmap.noreply@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 25, false);
                await client.AuthenticateAsync("netmap.noreply@gmail.com", "netmapmail");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
