using SkyTimer.Model;
using System.Collections.Generic;

namespace SkyTimer.Service
{
    public interface IPlotService
    {
        void Plot(List<Record> data);
    }
}
