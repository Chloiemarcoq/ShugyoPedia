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
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITrainingCategoryService _trainingCategoryService;
        private readonly IRatingService _ratingService;
        private readonly string _trainingImagesUrl;
        private readonly string _trainingImagesDirectory;
        public TrainingService(ITrainingRepository trainingRepository, ITrainingCategoryService trainingCategoryService, IRatingService ratingService)
        {
            _trainingRepository = trainingRepository;
            _trainingCategoryService = trainingCategoryService;
            _ratingService = ratingService;
            _trainingImagesUrl = PathManager.UrlPath.TrainingImagesUrl;
            _trainingImagesDirectory = PathManager.DirectoryPath.TrainingImagesDirectory;
        }

        public Dictionary<string, object> GetTrainingsFromCategory(string category)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            var allTrainings = _trainingRepository.GetTrainings().ToList();
            result["CategoryName"] = category;
            var data = allTrainings.Where(s => _trainingCategoryService.GetCategoryNameById(s.CategoryId) == category).Select(s => new TrainingViewModel
            {
                TrainingId = s.TrainingId,
                TrainingImage = _trainingImagesUrl + s.TrainingImage,
                TrainingName = s.TrainingName,
                RateAverage = _ratingService.GetRatingAverageFromTraining(s.TrainingId).ToString()
            }).ToList();
            result["Trainings"] = data;
            return result;
        }
        public List<TrainingViewModel> GetTrainings()
        {           
            var trainings = _trainingRepository
                .GetTrainings()
                .Select(s => new TrainingViewModel
                {
                    TrainingId = s.TrainingId,
                    TrainingName = s.TrainingName,
                    CategoryName = s.Category.CategoryName,
                    TrainingDescription = s.TrainingDescription,
                    TrainingImage = _trainingImagesUrl + s.TrainingImage,
                    RateAverage = _ratingService.GetRatingAverageFromTraining(s.TrainingId).ToString()
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
            model.TrainingImage = PathManager.MakeValidFileName(training.TrainingName) + ".png";
            model.CreatedBy = user;
            model.CreatedTime = DateTime.Now;
            model.UpdatedBy = user;
            model.UpdatedTime = DateTime.Now;

            string trainingImageFullPath = Path.Combine(_trainingImagesDirectory, model.TrainingImage);
            using (var fileStream = new FileStream(trainingImageFullPath, FileMode.Create))
            {
                training.TrainingImage.CopyTo(fileStream);
            }

            _trainingRepository.AddTraining(model);
        }
    }
}
