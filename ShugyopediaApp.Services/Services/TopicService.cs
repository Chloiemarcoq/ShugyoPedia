using Microsoft.VisualBasic.FileIO;
using ShugyopediaApp.Data;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Data.Repositories;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ITrainingService _trainingService;
        private readonly string _topicResourceDirectory;
        public TopicService(ITopicRepository topicRepository
            , ITrainingService trainingService)
        {
            _topicRepository = topicRepository;
            _trainingService = trainingService;
            _topicResourceDirectory = PathManager.DirectoryPath.TopicResourcesDirectory;
        }
        public List<TopicViewModel> GetTopics()
        {
            List<TopicViewModel> topics = _topicRepository
                .GetTopics()
                .Select(s => new TopicViewModel
                {
                    TopicId = s.TopicId,
                    TrainingId = s.TrainingId,
                    TrainingName = s.Training.TrainingName,
                    TopicName = s.TopicName,
                    ResourceFile = s.ResourceFile
                })
                .OrderBy(s => s.TrainingId)
                .ToList();
            return topics;
        }
        public AddTopicViewModel GetTrainingSummary()//fetch only id and name
        {
            var trainings = _trainingService.GetTrainings()
                .Select(s => new TrainingViewModel
                {
                    TrainingId = s.TrainingId,
                    TrainingName = s.TrainingName
                })
                .OrderBy(s => s.CategoryId)
                .ToList();
            return new AddTopicViewModel { Trainings = trainings };
        }
        public void AddTopic(AddTopicViewModel topic, string user)
        {
            string fileExtension = Path.GetExtension(topic.ResourceFileUpload.FileName)?.ToLower();
            var model = new Topic();
            model.TrainingId = topic.TrainingId;
            model.TopicName = topic.TopicName;
            model.ResourceFile = Regex.Replace(PathManager.MakeValidFileName(topic.TopicName) + fileExtension, @"\s", "");
            model.CreatedBy = user;
            model.CreatedTime = DateTime.Now;
            model.UpdatedBy = user;
            model.UpdatedTime = DateTime.Now;
            string topicResourceFullPath = Path.Combine(_topicResourceDirectory, model.ResourceFile);
            using (var fileStream = new FileStream(topicResourceFullPath, FileMode.Create))
            {
                topic.ResourceFileUpload.CopyTo(fileStream);
            }
            _topicRepository.AddTopic(model);
        }
        public void EditTopic(AddTopicViewModel topic, string user)
        {
            string fileExtension = Path.GetExtension(topic.ResourceFileUpload.FileName)?.ToLower();
            Topic model = new Topic();
            model.TrainingId = topic.TrainingId;
            model.TopicId = topic.TopicId;
            model.TopicName = topic.TopicName;
            model.ResourceFile = Regex.Replace(PathManager.MakeValidFileName(topic.TopicName) + fileExtension, @"\s", "");
            model.UpdatedBy = user;
            model.UpdatedTime = DateTime.Now;

            if (topic.ResourceFileUpload != null)
            {
                string topicResourceFullPath = Path.Combine(_topicResourceDirectory, model.ResourceFile);
                using (var fileStream = new FileStream(topicResourceFullPath, FileMode.Create))
                {
                    topic.ResourceFileUpload.CopyTo(fileStream);
                }
            }
            _topicRepository.EditTopic(model);
        }
        public void DeleteTopic(int TopicId)
        {
            var model = new Topic();
            model.TopicId = TopicId;
            _topicRepository.DeleteTopic(model);
        }
    }
}
