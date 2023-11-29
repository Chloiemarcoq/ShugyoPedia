using Basecode.Data.Repositories;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Repositories
{
    public class TopicRepository : BaseRepository, ITopicRepository
    {
        public TopicRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void DeleteTopic(Topic topic)
        {
            var topicToDelete = this.GetDbSet<Topic>().Find(topic.TopicId);

            if (topicToDelete != null)
            {
                this.GetDbSet<Topic>().Remove(topicToDelete);
                UnitOfWork.SaveChanges();
            }
        }
    }
}
