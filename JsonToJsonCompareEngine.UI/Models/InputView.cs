using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JsonToJsonCompareEngine.UI.Models
{
    [ViewComponent(Name = "CompareResult")]
    public class ResultView : ViewComponent
    {
        private Engine _engine;

        public async Task<IViewComponentResult> InvokeAsync(string previousFile, string currentFile)
        {
            var items = await GetCompareResultsAsync(previousFile, currentFile);
            return View("Default", items);
        }
        private Task<ComparisonOutput> GetCompareResultsAsync(string previousFile, string currentFile)
        {
            _engine = new Engine(previousFile, currentFile);
            var comparisonOutput = _engine.Compare();
            return Task.FromResult(comparisonOutput);
        }
    }
}
