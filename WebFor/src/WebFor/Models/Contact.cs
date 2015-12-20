using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public DateTime ContactDate { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactBody { get; set; }
        public string ContactPhone { get; set; }
    }
}
