using SkyTimer.Model;
using System.Collections.Generic;

namespace SkyTimer.Service
{
    public interface IRecordListService
    {
        List<RecordList> GetLists();
    }
}
