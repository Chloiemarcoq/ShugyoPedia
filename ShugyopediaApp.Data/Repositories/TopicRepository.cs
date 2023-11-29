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

        public IQueryable<Topic> GetTopics()
        {
            return this.GetDbSet<Topic>();
        }
        public void AddTopic(Topic topic)
        {
            this.GetDbSet<Topic>().Add(topic);
            UnitOfWork.SaveChanges();
        }
        public void EditTopic(Topic topic)
        {
            var recordFound = this.GetDbSet<Topic>().Find(topic.TopicId);
            if (recordFound != null)
            {
                recordFound.TopicName = topic.TopicName;
                recordFound.TrainingId = topic.TrainingId;
                recordFound.ResourceFile = (string.IsNullOrEmpty(topic.ResourceFile)) ? recordFound.ResourceFile : topic.ResourceFile;
                recordFound.UpdatedBy = topic.UpdatedBy;
                recordFound.UpdatedTime = topic.UpdatedTime;
                UnitOfWork.SaveChanges();
            }
        }
        public void DeleteTopic(Topic topic)
        {
            var topicToDelete = this.GetDbSet<Topic>().Find(topic.TopicId);

            if (topicToDelete != null)
            {
                this.GetDbSet<Topic>().Remove(topicToDelete);
            }
        }
    }
}
