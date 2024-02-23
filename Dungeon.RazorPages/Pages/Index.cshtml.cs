using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dungeon.Logic.Data;
using Dungeon.Logic.Model;
using Dungeon.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dungeon.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStoryRepository _storyRepository;
        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IStoryRepository storyRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _storyRepository = storyRepository;
            _logger.LogWarning("Hello dungeon logging!");
            DatabaseHost = _configuration.GetValue<string>("AppSettings:DatabaseHost");
        }

        public DungeonStoryViewModel DungeonStory { get; private set; }
        public string DatabaseHost {get;}
        public IActionResult OnGet(int roomId, string keyword)
        {
            Story dungeonStory = _storyRepository.GetStory("main");
            if (roomId > 0)
            {
                try
                {
                    dungeonStory.Resume(_storyRepository, roomId);
                    dungeonStory.Navigate(_storyRepository, keyword);
                }
                catch(NavigationException ex)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    //https://stackoverflow.com/questions/46772632/how-pass-objects-from-one-page-to-another-on-asp-net-core-with-razor-pages?rq=1
                    return new RedirectToPageResult("Mistake", new { message = ex.Message} );
                }
                catch (RoomNotFoundException ex)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return new RedirectToPageResult("Mistake", new { message = ex.Message});
                }
            }
            else
            {
                dungeonStory.Begin();
            }
            DungeonStory = new DungeonStoryViewModel(dungeonStory);

            return Page();
        }

    }
}
