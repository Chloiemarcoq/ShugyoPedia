using System;
using System.Collections.Generic;

namespace ShugyopediaApp.Admin.Models
{
    public partial class TopicResource
    {
        public int ResourceId { get; set; }
        public int TopicId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public string ResourcePath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
