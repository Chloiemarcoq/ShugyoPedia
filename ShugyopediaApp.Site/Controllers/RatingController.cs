using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using ShugyopediaApp.Services.Services;
using ShugyopediaApp.Site.Mvc;
using System;

namespace ShugyopediaApp.Site.Controllers
{
    public class RatingController : ControllerBase<RatingController>
    {
        private readonly IRatingService _ratingService;
        public RatingController(
            IRatingService ratingService,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
        IConfiguration configuration,
            IMapper mapper = null)
            : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _ratingService = ratingService;
        }
        [HttpPost]
        public IActionResult AddRating(LearnTrainingViewModel model, string rate, string email, string name, string review)
        {
            int rating = Convert.ToInt32(rate);

            // Create a new LearnRatingViewModel object
            var newRating = new LearnRatingViewModel
            {
                RatingReview = review,
                Rate = rating,
                RaterEmail = email,
                RaterName = name
            };

            // Call the service to add the rating
            _ratingService.AddRating(model.TrainingId, newRating);

            // Redirect to a success page or back to the view
            return RedirectToAction("Learn", "Training", new { trainingName = model.TrainingName }); // Replace with your desired action and controller names
        }

    }
}
