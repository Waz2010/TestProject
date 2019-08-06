using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace JsonToJsonCompareEngine
{
    public class JsonFile
    {
        public JsonFile(string fileName)
        {
            if(File.Exists(fileName))
            {
                FileInfo = new FileInfo(fileName);
                FileContent = File.ReadAllBytes(fileName);
            }
        }
        public FileInfo FileInfo {get; set;}
        public byte[] FileContent { get; set; }
        public JObject JsonContent { get; set; }
        public int OccurrenceInOld { get; set; }
        public int OccurrenceInNew { get; set; }

        public  JObject GetJson()
        {
            JsonContent = JObject.Parse(File.ReadAllText(FileInfo.FullName));
            return JsonContent;
        }
    }
}
