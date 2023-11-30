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
using System.Collections.Generic;

namespace ShugyopediaApp.Admin.Controllers
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
        public IActionResult Index()
        {
            List<RatingViewModel> ratings = _ratingService.GetRatings();
            return View(ratings);
        }

        public IActionResult DeleteRating(Rating rating)
        {
            _ratingService.DeleteRating(rating.RaterName);
            return RedirectToAction("Index");
        }
    }
}
