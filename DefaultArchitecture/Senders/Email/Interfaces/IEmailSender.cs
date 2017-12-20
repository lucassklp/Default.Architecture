using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Senders.Email.Interfaces
{
    public interface IEmailSender : ISender, ISenderAsynchronous
    {
        string To { get; set; }
        List<string> Bcc { get; set; }
        List<string> CC { get; set; }
        string Body { get; set; }
        bool IsBodyHtml { get; set; }
        string Subject { get; set; }
    }
}
