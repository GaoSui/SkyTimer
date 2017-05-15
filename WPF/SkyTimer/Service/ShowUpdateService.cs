using System.Windows;
using System.Diagnostics;
using SkyTimer.Properties;

namespace SkyTimer.Service
{
    public interface IShowUpdateService
    {
        void ShowUpdate();
    }

    public class ShowUpdateService : IShowUpdateService
    {
        public void ShowUpdate()
        {
            if (MessageBox.Show(Resources.Update, "UPDATE", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Process.Start("https://github.com/GaoSui/SkyTimer");
            }
        }
    }
}
