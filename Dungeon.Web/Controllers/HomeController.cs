using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dungeon.Web.Models;
using Dungeon.Logic.Data;
using Dungeon.Logic.Model;

namespace Dungeon.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            DungeonStory dungeonStory = GetStory();
            dungeonStory.Begin();
            DungeonStoryViewModel viewModel = new DungeonStoryViewModel(dungeonStory);

            return View(viewModel);
        }

        private DungeonStory GetStory()
        {
            //! is the "null forgiving" operator that can be used on strings
            StoryXmlRepository repository = new StoryXmlRepository();
            RoomCatalog roomCatalog = repository.GetCatalog(Path.Combine(_configuration["Dungeon:StoryPath"]!, "MainDungeon.xml"));
            DungeonStory dungeonStory = new DungeonStory(roomCatalog);
            return dungeonStory;
        }

        [Route("navigate/{roomName}/{keyword}")]
        public IActionResult Navigate(string roomName, string keyword)
        {
            try
            {
                DungeonStory dungeonStory = GetStory();
                dungeonStory.Resume(roomName);
                dungeonStory.Navigate(keyword);

                DungeonStoryViewModel viewModel = new DungeonStoryViewModel(dungeonStory);

                return View("Index", viewModel);
            }
            catch (RoomNotFoundException ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View("Mistake");
            }

        }


        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
