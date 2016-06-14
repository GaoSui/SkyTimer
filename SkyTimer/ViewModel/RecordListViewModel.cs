using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SkyTimer.Model;
using GalaSoft.MvvmLight.Messaging;
using SkyTimer.Service;
using GalaSoft.MvvmLight.CommandWpf;
using SkyTimer.MVVM;
using SkyTimer.ModelWrapper;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using SkyTimer.Properties;
using System.ComponentModel;

namespace SkyTimer.ViewModel
{
    public class RecordListViewModel : ObservableObject
    {
        public RecordListViewModel(IRecordListService list, IRenameDialogService rename)
        {
            renameService = rename;
            Lists = new ObservableCollection<RecordListWrapper>(list.GetLists().Select(model => new RecordListWrapper(model)));
            SelectedList = Lists[0];

            foreach (var item in Lists)
            {
                foreach (var record in item.List)
                {
                    record.PropertyChanged += Record_PropertyChanged;
                }
            }


            Messenger.Default.Register<int>(this, PushTime);
            Messenger.Default.Register<string>(this, (msg) =>
            {
                lastScramble = msg;
            });
        }


        private string lastScramble;
        private IRenameDialogService renameService;

        public ObservableCollection<RecordListWrapper> Lists { get; set; }

        private RecordListWrapper selectedList;
        public RecordListWrapper SelectedList
        {
            get { return selectedList; }
            set
            {
                Set(ref selectedList, value);

                value.List.CollectionChanged -= List_CollectionChanged;
                value.List.CollectionChanged += List_CollectionChanged;

                Messenger.Default.Send(SelectedList.List.Select(wrapper => wrapper.Model).ToList());
            }
        }



        private RecordWrapper selectedRecord;
        public RecordWrapper SelectedRecord
        {
            get { return selectedRecord; }
            set { Set(ref selectedRecord, value); }
        }

        public void PushTime(int time)
        {
            var r = new RecordWrapper(new Record { Time = time, Scramble = lastScramble, TimeCreated = DateTime.Now });
            r.PropertyChanged += Record_PropertyChanged;
            SelectedList.List.Add(r);
            RequestScramble();
        }

        public void RequestScramble()
        {
            Messenger.Default.Send(new UpdateScrambleInstruction(SelectedList.ScrambleType));
        }

        private RelayCommand addList;
        public RelayCommand AddList
        {
            get
            {
                return addList ?? (addList = new RelayCommand(() =>
                {
                    if (renameService.Rename())
                    {
                        var newList = new RecordListWrapper(new RecordList { Name = renameService.NewName });

                        Lists.Add(newList);
                        SelectedList = newList;
                    }
                }));
            }
        }

        private RelayCommand removeList;
        public RelayCommand RemoveList
        {
            get
            {
                return removeList ?? (removeList = new RelayCommand(() =>
                {
                    if (Lists.Count == 1) return;
                    var i = Lists.IndexOf(SelectedList);
                    if (i == Lists.Count - 1) i--;

                    var copy = SelectedList;
                    SelectedList = Lists[i];
                    Lists.Remove(copy);
                }));
            }
        }

        public void DNFLast()
        {
            var op = SelectedList.List.LastOrDefault();
            if (op != null) op.DNF = op.DNF ? false : true;
        }


        public void PlusTwoLast()
        {
            var op = SelectedList.List.LastOrDefault();
            if (op != null) op.PlusTwo = op.PlusTwo ? false : true;
        }

        public void RemoveLast()
        {
            SelectedList.List.Remove(SelectedList.List.LastOrDefault());
        }

        private RelayCommand removeRecord;
        public RelayCommand RemoveRecord
        {
            get
            {
                return removeRecord ?? (removeRecord = new RelayCommand(() =>
                {
                    SelectedList.List.Remove(SelectedRecord);
                }));
            }
        }


        public void Save()
        {
            var data = Lists.Select(wrapper => wrapper.Model).ToList();
            var formatter = new BinaryFormatter();
            using (var file = File.Open(Settings.Default.DataPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(file, data);
            }
        }




        private void Record_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DNF" || e.PropertyName == "PlusTwo")
            {
                Messenger.Default.Send(SelectedList.List.Select(wrapper => wrapper.Model).ToList());
            }
        }

        private void List_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Messenger.Default.Send(SelectedList.List.Select(wrapper => wrapper.Model).ToList());
        }

        public List<string> ScrambleType { get; } = new List<string>
        {
            "222",
            "333",
            "333fm",
            "333ni",
            "444",
            "444ni",
            "555",
            "555ni",
            "666",
            "777",
            "clock",
            "minx",
            "pyram",
            "skewb",
            "sq1"
        };
    }
}
