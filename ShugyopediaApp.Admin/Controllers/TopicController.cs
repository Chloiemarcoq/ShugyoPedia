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

		[AllowAnonymous]
		public IActionResult Add()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult Edit()
		{
			return View();
		}

		public IActionResult DeleteTopic()
		{
			return View();
		}
	}
}
