using SkyTimer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTimer.Service
{
    public class ShowTextService : IShowTextService
    {
        public void Show(string text)
        {
            var window = new TextWindow(text);
            window.ShowDialog();
        }
    }
}
