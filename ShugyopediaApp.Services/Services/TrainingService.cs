using ShugyopediaApp.Data;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Data.Repositories;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITrainingCategoryService _trainingCategoryService;
        private readonly IRatingService _ratingService;
        private readonly string _trainingImagesUrl;
        private readonly string _resourceFileUrl;
        private readonly string _trainingImagesDirectory;
        public TrainingService(ITrainingRepository trainingRepository, ITrainingCategoryService trainingCategoryService, IRatingService ratingService)
        {
            _trainingRepository = trainingRepository;
            _trainingCategoryService = trainingCategoryService;
            _ratingService = ratingService;
            _trainingImagesUrl = PathManager.UrlPath.TrainingImagesUrl;
            _resourceFileUrl = PathManager.UrlPath.TopicResources;
            _trainingImagesDirectory = PathManager.DirectoryPath.TrainingImagesDirectory;
        }

        public List<TrainingViewModel> GetTrainingsFromCategory(string categoryName)
        {
            List<TrainingViewModel> result = _trainingRepository
                .GetTrainings()
                .Where(s => s.Category.CategoryName == categoryName)
                .Select(s => new TrainingViewModel
                {
                    TrainingName = s.TrainingName,
                    TrainingDescription = s.TrainingDescription,
                    TrainingImage = _trainingImagesUrl + s.TrainingImage,
                    RateAverage = _ratingService.GetRatingAverageFromTraining(s.TrainingId)
                })
                .ToList();
            return result;
        }
        public List<TrainingViewModel> GetTrainings()
        {
            List<TrainingViewModel> trainings = _trainingRepository
                .GetTrainings()
                .Select(s => new TrainingViewModel
                {
                    TrainingId = s.TrainingId,
                    TrainingName = s.TrainingName,
                    CategoryName = s.Category.CategoryName,
                    CategoryId = s.CategoryId,
                    TrainingDescription = s.TrainingDescription,
                    TrainingImage = _trainingImagesUrl + s.TrainingImage,
                    RateAverage = _ratingService.GetRatingAverageFromTraining(s.TrainingId)
                })
                .OrderBy(s => s.TrainingId)
                .ToList();
            return trainings;
        }
        public AddTrainingViewModel GetTrainingCategorySummary()//fetch only id and name
        {
            var categories = _trainingCategoryService.GetTrainingCategories()
                .Select(s => new TrainingCategoryViewModel
                {
                     CategoryId = s.CategoryId,
                     CategoryName= s.CategoryName
                })
                .OrderBy(s => s.CategoryId)
                .ToList();
            return new AddTrainingViewModel { Categories = categories };
        }
        public void AddTraining(AddTrainingViewModel training, string user)
        {
            var model = new Training();
            model.TrainingName = training.TrainingName;
            model.CategoryId = training.CategoryId;
            model.TrainingDescription = training.TrainingDescription;
            model.TrainingImage = Regex.Replace(PathManager.MakeValidFileName(training.TrainingName) + ".png", @"\s", "");
            model.CreatedBy = user;
            model.CreatedTime = DateTime.Now;
            model.UpdatedBy = user;
            model.UpdatedTime = DateTime.Now;

            string trainingImageFullPath = Path.Combine(_trainingImagesDirectory, model.TrainingImage);
            using (var fileStream = new FileStream(trainingImageFullPath, FileMode.Create))
            {
                training.TrainingImageFile.CopyTo(fileStream);
            }

            _trainingRepository.AddTraining(model);
        }
        public void EditTraining(AddTrainingViewModel training, string user)
        {
            var model = new Training();
            model.TrainingId = training.TrainingId;
            model.TrainingName = training.TrainingName;
            model.CategoryId = training.CategoryId;
            model.TrainingDescription = training.TrainingDescription;
            model.TrainingImage = Regex.Replace(PathManager.MakeValidFileName(training.TrainingName) + ".png", @"\s", "");
            model.UpdatedBy = user;
            model.UpdatedTime = DateTime.Now;

            if (training.TrainingImageFile != null)
            {
                string trainingImageFullPath = Path.Combine(_trainingImagesDirectory, model.TrainingImage);
                using (var fileStream = new FileStream(trainingImageFullPath, FileMode.Create))
                {
                    training.TrainingImageFile.CopyTo(fileStream);
                }
            }
            _trainingRepository.EditTraining(model);
        }
        public void DeleteTraining(int trainingId) 
        {
            var model = new Training();
            model.TrainingId = trainingId;
            _trainingRepository.DeleteTraining(model);
        }
        public LearnTrainingViewModel GetTrainingTopicRatingDetails(string trainingName)
        {
            LearnTrainingViewModel data = new LearnTrainingViewModel();
            Training training = _trainingRepository
                    .GetTrainings()
                    .FirstOrDefault(t => t.TrainingName == trainingName);
            if (training != null)
            {
                data = new LearnTrainingViewModel
                {
                    CategoryName = training.Category.CategoryName,
                    TrainingName = training.TrainingName,
                    TrainingDescription = training.TrainingDescription,
                    UpdatedTime = training.UpdatedTime,
                    CreatedBy = training.CreatedBy,
                    RateAverage = _ratingService.GetRatingAverageFromTraining(training.TrainingId),
                    Ratings = training.Ratings.Select(r => new LearnRatingViewModel
                    {
                        RatingReview = r.RatingReview,
                        Rate = r.Rate,
                        RaterName = r.RaterName,
                        RaterEmail = r.RaterEmail
                    }).ToList(),
                    Topics = training.Topics.Select(t => new LearnTopicViewModel
                    {
                        TopicId = t.TopicId,
                        TopicName = t.TopicName,
                        ResourceFile = _resourceFileUrl + t.ResourceFile,
                        ResourceFileType = Path.GetExtension(t.ResourceFile),
                        IsChecked = false
                    }).ToList()                    
                };
            }                    
            return data;
        }
    }
    
}
