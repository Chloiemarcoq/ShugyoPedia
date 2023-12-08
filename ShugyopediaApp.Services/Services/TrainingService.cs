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
using System.IO.Compression;
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
        private readonly string _topicResourcesDirectory;
        private readonly string _topicResourcesUrl;
        public TrainingService(ITrainingRepository trainingRepository, ITrainingCategoryService trainingCategoryService, IRatingService ratingService)
        {
            _trainingRepository = trainingRepository;
            _trainingCategoryService = trainingCategoryService;
            _ratingService = ratingService;
            _trainingImagesUrl = PathManager.UrlPath.TrainingImagesUrl;
            _resourceFileUrl = PathManager.UrlPath.TopicResourcesUrl;
            _trainingImagesDirectory = PathManager.DirectoryPath.TrainingImagesDirectory;
            _topicResourcesUrl = PathManager.UrlPath.TopicResourcesUrl;
            _topicResourcesDirectory = PathManager.DirectoryPath.TopicResourcesDirectory;
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
                    CategoryName = s.CategoryName
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
                    TrainingId = training.TrainingId,
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
        //public Dictionary<string, string> DownloadResourceLogic(string fileUrl)
        //{
        //    string fileName = fileUrl.Replace(_topicResourcesUrl, "");
        //    string fileDirectory = Path.Combine(_topicResourcesDirectory, fileName);
        //    string contentType;

        //    switch (Path.GetExtension(fileName))
        //    {
        //        case ".pdf":
        //            contentType = "application/pdf";
        //            break;
        //        case ".mp4":
        //            contentType = "video/mp4";
        //            break;
        //        case ".ppt":
        //            contentType = "application/vnd.ms-powerpoint";
        //            break;
        //        default:
        //            contentType = "application/octet-stream";
        //            break;
        //    }
        //    return new Dictionary<string, string> {
        //        { "fileName", fileName },
        //        { "fileDirectory", fileDirectory },
        //        { "contentType", contentType }
        //    };
        //}
        //public byte[] DownloadMultipleResourcesLogic(List<string> fileUrls)
        //{
        //    using (MemoryStream zipStream = new MemoryStream())
        //    {
        //        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
        //        {
        //            foreach (string fileUrl in fileUrls)
        //            {
        //                string fileName = fileUrl.Replace(_topicResourcesUrl, "");
        //                string fileDirectory = Path.Combine(_topicResourcesDirectory, fileName);
        //                string contentType;

        //                switch (Path.GetExtension(fileName))
        //                {
        //                    case ".pdf":
        //                        contentType = "application/pdf";
        //                        break;
        //                    case ".mp4":
        //                        contentType = "video/mp4";
        //                        break;
        //                    case ".ppt":
        //                        contentType = "application/vnd.ms-powerpoint";
        //                        break;
        //                    default:
        //                        contentType = "application/octet-stream";
        //                        break;
        //                }

        //                // Download file content (assuming you have a method to retrieve the file content)
        //                byte[] fileBytes = DownloadFileContent(fileUrl);

        //                // Create a new entry in the zip archive
        //                ZipArchiveEntry entry = archive.CreateEntry(fileName, CompressionLevel.Optimal);

        //                // Write the file content to the zip entry
        //                using (Stream entryStream = entry.Open())
        //                {
        //                    entryStream.Write(fileBytes, 0, fileBytes.Length);
        //                }
        //            }
        //        }

        //        // Return the byte array of the zipped files
        //        return zipStream.ToArray();
        //    }
        //}
        public Dictionary<string, string> DownloadResourceLogic(string fileUrl)
        {
            string fileName = fileUrl.Replace(_topicResourcesUrl, "");
            string fileDirectory = Path.Combine(_topicResourcesDirectory, fileName);
            string contentType = GetContentType(Path.GetExtension(fileName));

            return new Dictionary<string, string>
        {
            { "fileName", fileName },
            { "fileDirectory", fileDirectory },
            { "contentType", contentType }
        };
        }


        private string GetContentType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".mp4":
                    return "video/mp4";
                case ".ppt":
                    return "application/vnd.ms-powerpoint";
                default:
                    return "application/octet-stream";
            }
        }
    }
    
}
