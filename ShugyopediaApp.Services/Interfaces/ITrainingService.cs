using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Interfaces
{
    public interface ITrainingService
    {
        //Dictionary<string, object> GetTrainingsFromCategory(string category);
        List<TrainingViewModel> GetTrainingsFromCategory(string categoryName);
        List<TrainingViewModel> GetTrainings();
        void AddTraining(AddTrainingViewModel training, string user);
        AddTrainingViewModel GetTrainingCategorySummary();//fetch only id and name
        void EditTraining(AddTrainingViewModel training, string user);
        void DeleteTraining(int trainingId);
        LearnTrainingViewModel GetTrainingTopicRatingDetails(string trainingName);
        Dictionary<string, string> DownloadResourceLogic(string fileUrl);
    }
}
