using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dungeon.Web.Models;
using Dungeon.Logic.Data;
using Dungeon.Logic.Model;
using Dungeon.EntityFramework.Data;

namespace Dungeon.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDungeonContext _dungeonContext;
        private readonly IConfiguration _configuration;
        private IStoryRepository _storyRepo;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IDungeonContext dungeonContext)
        {
            _logger = logger;
            _dungeonContext = dungeonContext;
            _configuration = configuration;
            //TODO: fix the dependency injects when you get basic EF operations working!
            _storyRepo = new StoryContextRepository((DungeonContext)dungeonContext);
        }

        public IActionResult Index()
        {
            Story dungeonStory = GetStory();
            dungeonStory.Begin();
            DungeonStoryViewModel viewModel = new DungeonStoryViewModel(dungeonStory);

            return View(viewModel);
        }

        private Story GetStory()
        {
            return _dungeonContext.Stories?.Single(story => story.Name.Equals("main"));

            // StoryXmlRepository repository = new StoryXmlRepository();
            // return repository.GetStory(Path.Combine(_configuration["Dungeon:StoryPath"]!, "MainDungeon.xml"));
        }

        [Route("navigate/{roomName}/{keyword}")]
        public IActionResult Navigate(string roomName, string keyword)
        {
            try
            {
                Story dungeonStory = GetStory();
                Room resumeRoom = _dungeonContext.Rooms.Single(r => r.Name == roomName);
                //dungeonStory.Resume(resumeRoom);
                dungeonStory.Navigate(_storyRepo, keyword);

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
