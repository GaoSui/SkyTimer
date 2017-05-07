/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:SkyTimer"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SkyTimer.Service;

namespace SkyTimer.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IScrambleService, TNoodleScramble>();
            SimpleIoc.Default.Register<IRenameDialogService, RenameDialogService>();
            SimpleIoc.Default.Register<IPlotService, PlotService>();
            SimpleIoc.Default.Register<IShowTextService, ShowTextService>();
            SimpleIoc.Default.Register<IGetRemoteVersionService, GetRemoteVersionService>();
            SimpleIoc.Default.Register<IShowUpdateService, ShowUpdateService>();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IRecordListService, FakeRecordLists>();
            }
            else
            {
                SimpleIoc.Default.Register<IRecordListService, RecordListReader>();
            }

            SimpleIoc.Default.Register<TimerViewModel>();
            SimpleIoc.Default.Register<ScrambleViewModel>();
            SimpleIoc.Default.Register<RecordListViewModel>();
            SimpleIoc.Default.Register<StatisticViewModel>(true);
            SimpleIoc.Default.Register<MainWindowViewModel>();
        }

        public TimerViewModel Timer => ServiceLocator.Current.GetInstance<TimerViewModel>();
        public ScrambleViewModel Scramble => ServiceLocator.Current.GetInstance<ScrambleViewModel>();
        public RecordListViewModel RecordList => ServiceLocator.Current.GetInstance<RecordListViewModel>();
        public StatisticViewModel Statistic => ServiceLocator.Current.GetInstance<StatisticViewModel>();
        public MainWindowViewModel Main => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}