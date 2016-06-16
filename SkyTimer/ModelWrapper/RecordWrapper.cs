using GalaSoft.MvvmLight.CommandWpf;
using SkyTimer.Helper;
using SkyTimer.Model;
using SkyTimer.MVVM;
using System;
using System.Windows;

namespace SkyTimer.ModelWrapper
{
    public class RecordWrapper : SimpleModelWrapperBase<Record>, IComparable<RecordWrapper>
    {
        public RecordWrapper(Record model) : base(model)
        {

        }

        public int Time { get { return Model.Time; } set { Set(value); } }

        public string Scramble { get { return Model.Scramble; } }

        public DateTime TimeCreated { get { return Model.TimeCreated; } }

        public bool PlusTwo
        {
            get { return Model.PlusTwo; }
            set
            {
                if (value) Time += 2000;
                else Time -= 2000;
                Set(value);
            }
        }

        public bool DNF
        {
            get { return Model.DNF; }
            set
            {
                Time = -Time;
                Set(value);
            }
        }

        public string Comment { get { return Model.Comment; } set { Set(value); } }


        public override string ToString()
        {
            return Time.ToStackmatFormat();
        }

        public int CompareTo(RecordWrapper other)
        {
            return Model.CompareTo(other.Model);
        }

        private RelayCommand copyScramble;
        public RelayCommand CopyScramble
        {
            get
            {
                return copyScramble ?? (copyScramble = new RelayCommand(() =>
                {
                    Clipboard.SetText(Model.Scramble);
                }));
            }
        }
    }
}
