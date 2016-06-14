using System;
using System.Collections.Generic;

namespace SkyTimer.Model
{
    [Serializable]
    public class RecordList
    {
        public string Name { get; set; }

        public string ScrambleType { get; set; } = "333";

        public List<Record> List { get; set; } = new List<Record>();
    }
}
