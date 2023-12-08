using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ShugyopediaApp.Site.Models
{
    public class ChatViewModel
    {
        public string Testing { get; set; }
        public List<Message> Messages { get; set; }
        // Other properties relevant to the chat component
    }
    public class Message
    {
        public bool IsQuestion { get; set; }
        public string Content { get; set; }
    }
}
