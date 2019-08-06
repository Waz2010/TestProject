using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JsonToJsonCompareEngine.UI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;

namespace JsonToJsonCompareEngine.UI.Controllers
{
    public class HomeController : Controller
    {
        private List<State> stateDb = new List<State>()
        {
            new State()
            {
                    StateName = "CA",
                    Id = 1
            },
            new State()
            {
                    StateName = "TX",
                    Id = 2
            },
            new State()
            {
                    StateName = "VA",
                    Id = 3
            },
        };

        private List<County> countyDb = new List<County>()
        {
            new County()
            {
                CountyName = "Placer",
                Id = 1,
                StateId = 1,
                Files = new List<string>(){"/Users/newtonacho/Documents/Folder1/Test.json",
                 "/Users/newtonacho/Documents/Folder1/Test1.json" }
            },
            new County()
            {
                CountyName = "Orange County",
                StateId = 1,
                Id = 2,
                Files = new List<string>(){"/Users/newtonacho/Documents/Folder1/Test.json",
                 "/Users/newtonacho/Documents/Folder1/Test1.json" }
            },
            new County()
            {
                CountyName = "Sacramento",
                StateId = 1,
                Id = 3,
                Files = new List<string>(){"/Users/newtonacho/Documents/Folder1/Test.json",
                 "/Users/newtonacho/Documents/Folder1/Test1.json" }
            }
        };

        private Engine _engine;

        public IActionResult Index()
        {
            var model = new ViewModel();
            var list = stateDb.ToList();
            model.States = list;
            return View(model);
        }
          
        [HttpPost]
        public ActionResult GetCountyByStateId(int stateid)
        {
            var counties = (from county in countyDb
                            where county.StateId == stateid
                            select county).ToList();

            var list = new SelectList(counties, "Id", "CountyName");
            return Json(list);
        }

        [HttpPost]
        public ActionResult GetFilesByCountyId(int countyid)
        {
            var matchedCounty = (from county in countyDb
                            where county.Id == countyid
                                 select county).SingleOrDefault();
            if(matchedCounty.Files.Count == 0)
            {
                return Json(null);
            }
            var list = new SelectList(matchedCounty.Files);
            return Json(list);
        }

        [HttpGet]
        public IActionResult OnGetCompareResults(string _previousFile, string _currentFile)
        {
            return ViewComponent("CompareResult", new { previousFile = _previousFile, currentFile = _currentFile });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
