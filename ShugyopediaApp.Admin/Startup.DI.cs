using ShugyopediaApp.Data;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Repositories;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using ShugyopediaApp.Services.Services;
using ShugyopediaApp.Admin.Authentication;
using ShugyopediaApp.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ShugyopediaApp.Admin
{
    // Other services configuration
    internal partial class StartupConfigurer
    {
        /// <summary>
        /// Configures the other services.
        /// </summary>
        private void ConfigureOtherServices()
        {
            // Framework
            this._services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            this._services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Common
            this._services.AddScoped<TokenProvider>();
            this._services.TryAddSingleton<TokenProviderOptionsFactory>();
            this._services.TryAddSingleton<TokenValidationParametersFactory>();
            this._services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            this._services.AddScoped<IUserService, UserService>();
            this._services.AddScoped<ITrainingCategoryService, TrainingCategoryService>();
            this._services.AddScoped<ITrainingService, TrainingService>();
            this._services.AddScoped<IRatingService, RatingService>();
            this._services.AddScoped<ITopicService, TopicService>();


            // Repositories
            this._services.AddScoped<IUserRepository, UserRepository>();
            this._services.AddScoped<ITrainingCategoryRepository, TrainingCategoryRepository>();
            this._services.AddScoped<ITrainingRepository, TrainingRepository>();
            this._services.AddScoped<IRatingRepository, RatingRepository>();
            this._services.AddScoped<ITopicRepository, TopicRepository>();
            this._services.AddScoped<IAccountRecoveryRequestRepository, AccountRecoveryRequestRepository>();

            // Manager Class
            this._services.AddScoped<SignInManager>();

            this._services.AddHttpClient();
        }
    }
}
