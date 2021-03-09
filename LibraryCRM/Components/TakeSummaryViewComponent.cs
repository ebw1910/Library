using LibraryCRM.Data.Models;
using LibraryCRM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCRM.Components
{
    public class TakeSummaryViewComponent : ViewComponent
    {
        private Take take;

        public TakeSummaryViewComponent(Take takeService)
        {
            take = takeService;
        }

        public IViewComponentResult Invoke()
        {
            return View(take);
        }
    }
}
