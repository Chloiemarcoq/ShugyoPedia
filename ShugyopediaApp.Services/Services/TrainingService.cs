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
        public TrainingService(ITrainingRepository trainingRepository, ITrainingCategoryService trainingCategoryService, IRatingService ratingService)
        {
            _trainingRepository = trainingRepository;
            _trainingCategoryService = trainingCategoryService;
            _ratingService = ratingService;
        }

        public Dictionary<string, object> GetTrainingsFromCategory(string category)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            var allTrainings = _trainingRepository.GetTrainings().ToList();
            result["CategoryName"] = category;
            var url = PathManager.UrlPath.TrainingImagesUrl;
            var data = allTrainings.Where(s => _trainingCategoryService.GetCategoryNameById(s.CategoryId) == category).Select(s => new TrainingViewModel
            {
                TrainingId = s.TrainingId,
                TrainingImage = url + s.TrainingImage,
                TrainingName = s.TrainingName,
                RateAverage = _ratingService.GetRatingAverageFromTraining(s.TrainingId).ToString()
            }).ToList();
            result["Trainings"] = data;
            return result;
        }
    }
}
