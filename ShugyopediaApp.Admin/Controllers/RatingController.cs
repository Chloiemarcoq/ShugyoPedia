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
using System;
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

        public IActionResult DeleteRating(string ratingId)
        {
            try
            {
                _ratingService.DeleteRating(ratingId);
                TempData["SuccessMessage"] = "Successfully Deleted";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error Deleting: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
    }
}
