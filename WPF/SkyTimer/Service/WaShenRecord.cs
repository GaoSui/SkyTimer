using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyTimer.Model;

namespace SkyTimer.Service
{
    public class WaShenRecord : IRecordListService
    {
        public List<RecordList> GetLists()
        {
            var list = new List<RecordList>();
            var rList = new RecordList { Name = "Wa" };
            var rnd = new Random();

            for (int i = 0; i < 15000; i++)
            {
                rList.List.Add(new Record { Time = rnd.Next(600000), TimeCreated = DateTime.Now - TimeSpan.FromDays(rnd.Next(180)) });
            }

            list.Add(rList);
            return list;
        }
    }
}
