using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using JsonToJsonCompareEngine.MVC.Models;

namespace JsonToJsonCompareEngine.MVC.Controllers
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
                StateId = 1,
                PreviousFileName = "/Users/newtonacho/Documents/Folder1/Test.json",
                CurrentFileName = "/Users/newtonacho/Documents/Folder1/Test1.json"
            },
            new County()
            {
                CountyName = "Orange County",
                StateId = 1,
                PreviousFileName = "/Users/newtonacho/Documents/Folder1/Test.json",
                CurrentFileName = "/Users/newtonacho/Documents/Folder1/Test1.json"
            },
            new County()
            {
                CountyName = "Sacramento",
                StateId = 1,
                PreviousFileName = "/Users/newtonacho/Documents/Folder1/Test.json",
                CurrentFileName = "/Users/newtonacho/Documents/Folder1/Test1.json"
            }
        };

        public ActionResult Index()
        {
            var model = new ViewModel();
            var list = stateDb.ToList();
            //foreach(var item in list)
            //{
            //    model.States.Add(new SelectListItem { Text = item.StateName, Value = item.Id.ToString() });
            //}
            model.States = list;
            return View(model);
        }

        [HttpPost]
        public ActionResult GetCounties(int stateId)
        {
            var model = new ViewModel();
            SelectList obgcity = null;
            
            var counties = (from county in countyDb
                            where county.StateId == stateId
                            select county).ToList();
            obgcity = new SelectList(counties, "Id", "CountyName", 0);

            //foreach (var county in counties)
            //{
            //    model.Counties.Add(new SelectListItem { Text = county.CountyName, Value = county.Id.ToString() });
            //}
            
            return Json(obgcity);
        }
    }
}
