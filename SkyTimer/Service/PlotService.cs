using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyTimer.Model;
using SkyTimer.View;

namespace SkyTimer.Service
{
    public class PlotService : IPlotService
    {
        public void Plot(List<Record> data)
        {
            var plot = new PlotWindow(data);
            plot.Show();
        }
    }
}
