using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface Identifiable
    {
        long ID { get; set; }
    }
}
