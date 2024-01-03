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
            var chatViewModel = new ChatViewModel(); 
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://api.openai.com/v1/completions");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
            }
            else
            {
            }

            return View("_Chatbox", chatViewModel);
        }
    }
}
