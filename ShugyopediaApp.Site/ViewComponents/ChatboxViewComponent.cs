using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShugyopediaApp.Services.ServiceModels; // Add your appropriate namespace
using ShugyopediaApp.Site.Models;

namespace ShugyopediaApp.Site.ViewComponents
{
    public class ChatboxViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public ChatboxViewComponent(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var chatViewModel = new ChatViewModel(); // Create an instance of your ChatViewModel

            // Make HTTP requests here to fetch messages or perform actions

            // Example: Fetching messages
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://api.openai.com/v1/completions");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Populate chatViewModel.Messages with fetched messages
            }
            else
            {
                // Handle failure or errors
            }

            return View("_Chatbox", chatViewModel); // Pass the view model to the view "_Chatbox.cshtml"
        }
    }
}
