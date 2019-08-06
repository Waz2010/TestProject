using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace JsonToJsonCompareEngine
{
    public class Engine
    {
        private JsonFile _previous;
        private JsonFile _current;
        private ComparisonOutput _output;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="current"></param>
        public Engine(string previous, string current)
        {
            //create a folder Compare
            _previous = new JsonFile(previous);
            _current = new JsonFile(current);
            _output = new ComparisonOutput();            
        }

        public ComparisonOutput Compare()
        {          
            CompareFolders();            
            return _output;
        }

        private void CompareFolders()
        {
            try
            {
                var table = new Hashtable();
                var result = CreateResult(table);
                _output.FileCompareResult = result;
            }
            catch(Exception ex)
            {
                _output.Errors.Add(ex.ToString());
            }
        }

        private FileCompareResult CreateResult(Hashtable table)
        {
            //create a Result Factory
            var result = Factory.Create<FileCompareResult>(new object[] {_previous,_current });
            result.Compile();
            return result;
        }
    }
    
}
