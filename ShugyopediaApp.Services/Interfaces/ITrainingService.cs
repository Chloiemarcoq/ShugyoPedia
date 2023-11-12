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
        Dictionary<string, object> GetTrainingsFromCategory(string category);
    }
}
