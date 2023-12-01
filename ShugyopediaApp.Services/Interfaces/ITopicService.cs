using ShugyopediaApp.Data.Interfaces;
﻿using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Interfaces
{
    public interface ITopicService
    {
        List<TopicViewModel> GetTopics();
        AddTopicViewModel GetTrainingSummary();
        void AddTopic(AddTopicViewModel topic, string user);
        void EditTopic(AddTopicViewModel topic, string user);
        void DeleteTopic(int TopicId);
    }
}
