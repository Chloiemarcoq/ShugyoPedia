using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Interfaces
{
    public interface ITopicRepository
    {
        void DeleteTopic(Topic topic);
    }
}
