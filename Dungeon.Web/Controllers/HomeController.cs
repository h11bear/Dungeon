using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            StoryXmlRepository repository = new StoryXmlRepository();
            //todo: handle path to XML more gracefully than this
            RoomCatalog roomCatalog = repository.GetCatalog(@"Bin\Debug\netcoreapp3.1\Story\MainDungeon.xml");
            DungeonStory dungeonStory = new DungeonStory(roomCatalog);
            dungeonStory.Begin();

            return View(dungeonStory);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
