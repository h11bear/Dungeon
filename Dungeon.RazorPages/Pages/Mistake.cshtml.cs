using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dungeon.RazorPages.Pages
{
    public class MistakeModel : PageModel
    {

        public string Message { get; set; }
        public void OnGet(string message)
        {
            Message = message;
        }
    }
}
