using System.Collections.Generic;
using SkyTimer.Model;

namespace SkyTimer.Service
{
    public class FakeRecordLists : IRecordListService
    {
        public List<RecordList> GetLists()
        {
            return new List<RecordList>
            {
                new RecordList
                {
                    Name="new1",
                    ScrambleType="333",
                    List=new List<Record>
                    {
                        new Record {Time=5000,DNF=true },
                        new Record {Time=4000 },
                        new Record {Time=3000 },
                        new Record {Time=2000 },
                        new Record {Time=1000 },
                        new Record {Time=500,PlusTwo=true }
                    }
                },
                new RecordList
                {
                    Name="new2",
                    ScrambleType="444",
                    List=new List<Record>
                    {
                        new Record {Time=70000 },
                        new Record {Time=70000 },
                        new Record {Time=70000 },
                        new Record {Time=70000 },
                    }
                }
            };
        }
    }
}
