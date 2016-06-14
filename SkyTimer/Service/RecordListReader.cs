using System.Collections.Generic;
using SkyTimer.Model;
using System.IO;
using SkyTimer.Properties;
using System.Runtime.Serialization.Formatters.Binary;

namespace SkyTimer.Service
{
    public class RecordListReader : IRecordListService
    {
        public List<RecordList> GetLists()
        {
            if (File.Exists(Settings.Default.DataPath))
            {
                var formatter = new BinaryFormatter();
                using (var file = File.Open(Settings.Default.DataPath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return (List<RecordList>)formatter.Deserialize(file);
                }
            }
            else
            {
                return new List<RecordList>
                {
                    new RecordList
                    {
                        Name = "Default"
                    }
                };
            }
        }
    }
}
