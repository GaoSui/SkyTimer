using Microsoft.Practices.ServiceLocation;
using SkyTimer.Properties;
using SkyTimer.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SkyTimer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Process.Start("control.exe", "mmsys.cpl,,1");

        protected override void OnStartup(StartupEventArgs e)
        {
            Process.Start("javaw.exe", "-jar TNoodle-WCA-0.11.1.jar -n");
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Default.Save();
            ServiceLocator.Current.GetInstance<RecordListViewModel>().Save();

            base.OnExit(e);
        }
    }
}
