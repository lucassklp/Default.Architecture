using DefaultArchitecture.Senders.Email.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace DefaultArchitecture.Senders.Email
{
    public class EmailSender : IEmailSender
    {
        public EmailConfiguration EmailConfiguration { get; set; }
        public string To { get; set; }
        public List<string> Bcc { get; set; }
        public List<string> CC { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Subject { get; set; }
        public Action<Exception> OnError { get; set; }

        public EmailSender()
        {
            this.Bcc = new List<string>();
            this.CC = new List<string>();
        }

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            this.EmailConfiguration = emailConfiguration;
            this.Bcc = new List<string>();
            this.CC = new List<string>();
        }

        public void Send()
        {
            try
            {
                var smtpClient = new SmtpClient(EmailConfiguration.SMTP.Server);
                smtpClient.Credentials = new NetworkCredential(EmailConfiguration.Sender, EmailConfiguration.Password);
                smtpClient.EnableSsl = EmailConfiguration.SMTP.EnableSsl;

                var from = new MailAddress(EmailConfiguration.Sender, EmailConfiguration.Name);
                var to = new MailAddress(this.To);
                var mail = new MailMessage(from, to);

                foreach (var bcc in this.Bcc)
                    mail.Bcc.Add(bcc);

                foreach (var cc in this.CC)
                    mail.CC.Add(cc);


                mail.Subject = this.Subject;
                mail.SubjectEncoding = Encoding.UTF8;

                mail.Body = this.Body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = this.IsBodyHtml;
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                this.OnError?.Invoke(ex);
            }
        }

        public void SendAsynchronous()
        {
            new Thread(this.Send).Start();
        }
    }
}