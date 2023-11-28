using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Data.Repositories;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
		public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }
        public List<TopicViewModel> GetTopics()
        {
			var data = _topicRepository
                .GetTopics()
				.Select(topic => new TopicViewModel{
					TopicName = topic.TopicName,
					TrainingId = topic.TrainingId,
				})
				.OrderBy(topic => topic.TopicId)
				.ToList();
			return data;
		}
    }
}
