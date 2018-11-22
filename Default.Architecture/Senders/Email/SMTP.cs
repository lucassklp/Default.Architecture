using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Default.Architecture.Senders.Email
{
    public class SMTP
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
