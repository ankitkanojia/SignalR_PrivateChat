using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignarRChat.Models
{
    public class MessageVm
    {
        public int FromContactId { get; set; }
        public string FromName { get; set; }
        public int ToContactId { get; set; }
        public string ToName { get; set; }
        public string Message { get; set; }
    }
}