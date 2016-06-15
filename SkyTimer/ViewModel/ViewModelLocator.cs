using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using SkyTimer.Utils.Scramble;
using SkyTimer.Service;
using GalaSoft.MvvmLight;

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

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IRecordListService, FakeRecordLists>();
            }
            else
            {
                SimpleIoc.Default.Register<IRecordListService, RecordListReader>();
                //SimpleIoc.Default.Register<IRecordListService, WaShenRecord>();
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
    }
}
