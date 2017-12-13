using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DefaultArchitecture.Senders.Email
{
    public abstract class EmailSender : ISender
    {
        public EmailConfiguration EmailConfiguration { get; private set; }
        public string To { get; set; }
        public List<string> Bcc { get; set; }
        public List<string> CC { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; } = true;
        public string Subject { get; set; }

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            this.EmailConfiguration = emailConfiguration;
            this.Bcc = new List<string>();
            this.CC = new List<string>();
        }


        public async Task Send()
        {
            var smtpClient = new SmtpClient("my.smtp.exampleserver.net");
            smtpClient.Credentials = new NetworkCredential("username", "password");

            var from = new MailAddress("test@example.com", "TestFromName");
            var to = new MailAddress(this.To);
            var mail = new MailMessage(from, to);

            foreach(var bcc in this.Bcc)
            {
                mail.Bcc.Add(bcc);
            }

            foreach(var cc in this.CC)
            {
                mail.CC.Add(cc);
            }


            // set subject and encoding
            mail.Subject = this.Subject;
            mail.SubjectEncoding = Encoding.UTF8;

            // set body-message and encoding
            mail.Body = this.Body;
            mail.BodyEncoding = Encoding.UTF8;
            // text or html
            mail.IsBodyHtml = this.IsBodyHtml;

            smtpClient.Send(mail);
        }
    }
}
