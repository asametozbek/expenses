using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Message
    {
        public int ID { get; set; }
        public int SenderID { get; set; }
        public int RecipientID { get; set; }

        public string Content { get; set; }
        public DateTime SendingDate { get; set; }

    }
}
