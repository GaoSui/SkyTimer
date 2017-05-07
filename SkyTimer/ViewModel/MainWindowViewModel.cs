using GalaSoft.MvvmLight.CommandWpf;
using Octokit;
using SkyTimer.Helper;
using SkyTimer.MVVM;
using SkyTimer.Properties;
using SkyTimer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTimer.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel(IGetRemoteVersionService getRemoteVersion,
                                   IShowUpdateService showUpdate)
        {
            getRemoteVersionService = getRemoteVersion;
            showUpdateService = showUpdate;
        }

        private IGetRemoteVersionService getRemoteVersionService;
        private IShowUpdateService showUpdateService;

        private RelayCommand update;
        public RelayCommand Update => update ?? (update = new RelayCommand(async () =>
        {
            try
            {
                var remoteVersionString = await getRemoteVersionService.GetRemoteVersion();
                var remoteVersions = remoteVersionString.Split('.');
                var localVersions = Settings.Default.Version.Split('.');
                var lv = int.Parse(localVersions[0]);
                var rv = int.Parse(remoteVersions[0]);

                var showUpdate = false;
                if (rv > lv) showUpdate = true;
                else if (rv == lv)
                {
                    lv = int.Parse(localVersions[1]);
                    rv = int.Parse(remoteVersions[1]);
                    if (rv > lv) showUpdate = true;
                    else if (rv == lv)
                    {
                        lv = int.Parse(localVersions[2]);
                        rv = int.Parse(remoteVersions[2]);
                        if (rv > lv) showUpdate = true;
                        else showUpdate = false;
                    }
                    else showUpdate = false;
                }
                else showUpdate = false;

                if (showUpdate) showUpdateService.ShowUpdate();
            }
            catch
            {
                return;
            }
        }));
    }
}

