using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Admin.Mvc;
using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using ShugyopediaApp.Services.Services;
using System;
using System.Collections.Generic;

namespace ShugyopediaApp.Admin.Controllers
{
    public class TopicController : ControllerBase<TopicController>
    {
        private readonly ITopicService _topicService;
        public TopicController(
            ITopicService topicService,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMapper mapper = null)
            : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _topicService = topicService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<TopicViewModel> topic = _topicService.GetTopics();
            return View(topic);
        }
        [HttpGet]
        public IActionResult AddTopic()
        {
            AddTopicViewModel trainings = _topicService.GetTrainingSummary();
            return View(trainings);
        }
        [HttpPost]
        public IActionResult AddTopic(AddTopicViewModel topic)
        {
            try
            {
                _topicService.AddTopic(topic, this.UserId);
                TempData["SuccessMessage"] = "Successfully Added";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = $"Error Adding: File too large";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RedirectEditTopic(TopicViewModel topic)
        {
            AddTopicViewModel editTopicData = _topicService.GetTrainingSummary();
            editTopicData.TrainingId = topic.TrainingId;
            editTopicData.TrainingName = topic.TrainingName;
            editTopicData.TopicId = topic.TopicId;
            editTopicData.TopicName = topic.TopicName;
            editTopicData.ResourceFile = topic.ResourceFile;
            return View("EditTopic", editTopicData);
        }
        [HttpPost]
        public IActionResult EditTopic(AddTopicViewModel topic)
        {
            try
            {
                _topicService.EditTopic(topic, this.UserId);
                TempData["SuccessMessage"] = "Successfully Edited";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error Editing: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteTopic(Topic topic)
		{
            try
            {
                _topicService.DeleteTopic(topic.TopicId);
                TempData["SuccessMessage"] = "Successfully Deleted";
            } catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error Deleting: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
    }
}
