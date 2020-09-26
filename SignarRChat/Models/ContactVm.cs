using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignarRChat.Models
{
    public class ContactVm
    {
        public int ContactId { get; set; }
        public string ConnectionId { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsGroup { get; set; }
    }
}