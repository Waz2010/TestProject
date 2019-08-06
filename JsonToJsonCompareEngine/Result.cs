using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace JsonToJsonCompareEngine
{
    public class FileCompareResult
    {
        public FileCompareResult(JsonFile previous, JsonFile current)
        {
            _matchedElementsList = new List<FileContentCompareResult>();
            _mismatchedElementsList = new List<FileContentCompareResult>();
            _removedElementsInNewList = new List<FileContentCompareResult>();
            _addedElementsInNewList = new List<FileContentCompareResult>();
            _exclusionList = new List<string>();

            _previous = previous;
            _current = current;
        }

        public FileCompareResult(JsonFile previous, JsonFile current, List<string> exclusionList)
        {
            _matchedElementsList = new List<FileContentCompareResult>();
            _mismatchedElementsList = new List<FileContentCompareResult>();
            _removedElementsInNewList = new List<FileContentCompareResult>();
            _addedElementsInNewList = new List<FileContentCompareResult>();
            _previous = previous;
            _current = current;
            _exclusionList = exclusionList;           
        }

        public string FileName { get; set; }
        public bool FileExistInOld { get; set; }
        public bool FileExistInNew { get; set; }
        public int TotalElementsCountInOld { get; set; }
        public int TotalElementsCountInNew { get; set; }
        public int MatchedElementsCount { get; set; }
        public int MismatchedCounts { get; set; }
        public int RemovedElementsInNew { get; set; }
        public int AddedElementsInNew { get; set; }
        public int ExclusionCount { get; set; }
        public string StateInPrevious { get; set; }
        public string StateInCurrent { get; set; }

        private JsonFile _previous;
        private JsonFile _current;

        //create list of matched elements
        public  List<FileContentCompareResult> _matchedElementsList { get; }
        //new/removed elements
        public List<FileContentCompareResult> _mismatchedElementsList { get; }
        //mismatched elements list
        public List<FileContentCompareResult> _removedElementsInNewList { get; }

        public List<FileContentCompareResult> _addedElementsInNewList { get; }
        //exclusion elements list
        private List<string> _exclusionList;

        public FileCompareResult Compile()
        {
            _previous.JsonContent = JObject.Parse(File.ReadAllText(_previous.FileInfo.FullName));
            _current.JsonContent = JObject.Parse(File.ReadAllText(_current.FileInfo.FullName));
            FindRemovedElements();
            FindMatchedElements();

            this.FileName = _current.FileInfo.FullName;
            this.FileExistInOld = _previous != null;
            this.FileExistInNew = _current != null;
            this.TotalElementsCountInNew = _current.JsonContent.Descendants().OfType<JProperty>().Count();
            this.TotalElementsCountInOld = _previous.JsonContent.Descendants().OfType<JProperty>().Count();
            this.MatchedElementsCount = _matchedElementsList.Count;
            this.RemovedElementsInNew = _removedElementsInNewList.Count;
            this.AddedElementsInNew = _addedElementsInNewList.Count;
            this.ExclusionCount = _exclusionList.Count;

            this.StateInCurrent = _current.JsonContent["State"]?.Value<string>();
            this.StateInPrevious = _previous.JsonContent["State"]?.Value<string>();

            return this;
        }

        private void FindRemovedElements()
        {
            var removed = _current.JsonContent.Descendants().OfType<JProperty>().Where(x => !_previous.JsonContent.Descendants().OfType<JProperty>().Any(g => x.Name == g.Name));
            var added = _previous.JsonContent.Descendants().OfType<JProperty>().Where(x => !_current.JsonContent.Descendants().OfType<JProperty>().Any(g => x.Name == g.Name));

            foreach (var item in removed)
            {
                var result = new FileContentCompareResult
                {
                    JsonElementPath = item.Path,
                    PreviousValue = item.Value.ToString(),
                    CurrentValue = ""
                };

                _removedElementsInNewList.Add(result);
            }

            foreach (var item in added)
            {
                var result = new FileContentCompareResult
                {
                    JsonElementPath = item.Path,
                    PreviousValue = "",
                    CurrentValue = item.Value.ToString()
                };

                _addedElementsInNewList.Add(result);
            }
        }

        private void FindMatchedElements()
        {
            var matchedElements = _current.JsonContent.Descendants().OfType<JProperty>()
                .Where(x => _previous.JsonContent.Descendants().OfType<JProperty>()
                .Any(y => y.Name == x.Name && x.Value.ToString() == y.Value.ToString())).ToList();

            var misMatchedElements = _current.JsonContent.Descendants().OfType<JProperty>()
                .Where(x => !_previous.JsonContent.Descendants().OfType<JProperty>()
                .Any(y => y.Name == x.Name && x.Value.ToString() == y.Value.ToString())).ToList();

            foreach (var item in matchedElements)
            {
                var result = new FileContentCompareResult
                {
                    JsonElementPath = item.Path,
                    CurrentValue = item.Value?.ToString(),
                    PreviousValue = _previous.JsonContent.Descendants().OfType<JProperty>().FirstOrDefault(x => x.Name == item.Name)?.Value.ToString() ?? ""
                };
                _matchedElementsList.Add(result);
            }

            foreach (var item in misMatchedElements)
            {
                var result = new FileContentCompareResult
                {
                    JsonElementPath = item.Path,
                    CurrentValue = item.Value?.ToString(),
                    PreviousValue = _previous.JsonContent.Descendants().OfType<JProperty>().FirstOrDefault(x => x.Name == item.Name)?.Value.ToString() ?? ""
                };
                _mismatchedElementsList.Add(result);
            }
        }
    }

    public class FileContentCompareResult
    {
        public string JsonElementPath { get; set; }
        public string PreviousValue { get; set; }
        public string CurrentValue { get; set; }
    }

}
