using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Practices.ServiceLocation;
using SkyTimer.Model;
using SkyTimer.MVVM;
using SkyTimer.Service;
using System.Collections.ObjectModel;
using System.Linq;

namespace SkyTimer.ModelWrapper
{
    public class RecordListWrapper : SimpleModelWrapperBase<RecordList>
    {
        public RecordListWrapper(RecordList model) : base(model)
        {
            List = new ObservableCollection<RecordWrapper>(model.List.Select(m => new RecordWrapper(m)));
            RegisterCollection(List, model.List);
        }

        public string ScrambleType
        {
            get { return Model.ScrambleType; }
            set { Set(value); }
        }

        public string Name
        {
            get { return Model.Name; }
            set { Set(value); }
        }

        public bool IncludeScramble
        {
            get { return Model.IncludeScramble; }
            set { Set(value); }
        }

        public ObservableCollection<RecordWrapper> List { get; }


        //Extension
        private RelayCommand renameList;
        public RelayCommand RenameList
        {
            get
            {
                return renameList ?? (renameList = new RelayCommand(() =>
                {
                    var dialog = ServiceLocator.Current.GetInstance<IRenameDialogService>();
                    if (dialog.Rename())
                    {
                        Name = dialog.NewName;
                    }
                }));
            }
        }

        private RelayCommand clearList;
        public RelayCommand ClearList
        {
            get
            {
                return clearList ?? (clearList = new RelayCommand(() =>
                {
                    List.Clear();
                }));
            }
        }

        private RelayCommand plot;
        public RelayCommand Plot
        {
            get
            {
                return plot ?? (plot = new RelayCommand(() =>
                {
                    ServiceLocator.Current.GetInstance<IPlotService>().Plot(List.Select(wrapper => wrapper.Model).ToList());
                }));
            }
        }
    }
}
