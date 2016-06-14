using GalaSoft.MvvmLight.Messaging;
using SkyTimer.Helper;
using SkyTimer.Model;
using SkyTimer.MVVM;
using System.Collections.Generic;
using System.Linq;

namespace SkyTimer.ViewModel
{
    public class StatisticViewModel : ObservableObject
    {
        public StatisticViewModel()
        {
            Messenger.Default.Register<List<Record>>(this, UpdateStatistic);
        }

        private string best;
        public string Best
        {
            get { return best; }
            set { Set(ref best, "Best : " + value); }
        }

        private string worst;
        public string Worst
        {
            get { return worst; }
            set { Set(ref worst, "Worst : " + value); }
        }

        private string all;
        public string All { get { return all; } set { Set(ref all, "Avg of all : " + value); } }

        private string current5;
        public string Current5 { get { return current5; } set { Set(ref current5, "Current 5 : " + value); } }

        private string current12;
        public string Current12 { get { return current12; } set { Set(ref current12, "Current 12 : " + value); } }

        private string current100;
        public string Current100 { get { return current100; } set { Set(ref current100, "Current 100 : " + value); } }

        private string best5;
        public string Best5 { get { return best5; } set { Set(ref best5, "Best 5 : " + value); } }

        private string best12;
        public string Best12 { get { return best12; } set { Set(ref best12, "Best 12 : " + value); } }

        private string best100;
        public string Best100 { get { return best100; } set { Set(ref best100, "Best 100 : " + value); } }

        public void UpdateStatistic(List<Record> data)
        {
            if (data.Count != 0)
            {
                Best = data.Min().ToString();
                Worst = data.Max().ToString();
            }
            else
            {
                Best = null;
                Worst = null;
            }

            if (data.Count >= 3)
            {
                var candidate = new List<Record>(data);
                All = CubingAvg(candidate, 2).ToStackmatFormat();
            }
            else
            {
                All = null;
            }

            //if (data.Count >= 5)
            //{
            //    var b5 = 0;
            //    for (int i = 0; i <= data.Count - 5; i++)
            //    {
            //        var candidate = data.GetRange(i, 5);
            //        var res = CubingAvg(candidate, 2);
            //        if (i == 0) b5 = res;
            //        if (i == data.Count - 5) Current5 = res.ToStackmatFormat();
            //        if (res < b5) b5 = res;
            //    }
            //    Best5 = b5.ToStackmatFormat();
            //}
            //else
            //{
            //    Current5 = null;
            //    Best5 = null;
            //}

            //if (data.Count >= 12)
            //{
            //    var b12 = 0;
            //    for (int i = 0; i <= data.Count - 12; i++)
            //    {
            //        var candidate = data.GetRange(i, 12);
            //        var res = CubingAvg(candidate, 2);
            //        if (i == 0) b12 = res;
            //        if (i == data.Count - 12) Current12 = res.ToStackmatFormat();
            //        if (res < b12) b12 = res;
            //    }
            //    Best12 = b12.ToStackmatFormat();
            //}
            //else
            //{
            //    Current12 = null;
            //    Best12 = null;
            //}

            //if (data.Count >= 100)
            //{
            //    var b100 = 0;
            //    for (int i = 0; i <= data.Count - 100; i++)
            //    {
            //        var candidate = data.GetRange(i, 100);
            //        var res = CubingAvg(candidate, 2);
            //        if (i == 0) b100 = res;
            //        if (i == data.Count - 100) Current100 = res.ToStackmatFormat();
            //        if (res < b100) b100 = res;
            //    }
            //    Best100 = b100.ToStackmatFormat();
            //}
            //else
            //{
            //    Current100 = null;
            //    Best100 = null;
            //}
            CalculateAvg(data, 5);
            CalculateAvg(data, 12);
            CalculateAvg(data, 100);
        }

        private void CalculateAvg(List<Record> data, int param)
        {
            var best = typeof(StatisticViewModel).GetProperty($"Best{param}");
            var current = typeof(StatisticViewModel).GetProperty($"Current{param}");

            if (data.Count >= param)
            {
                var b = 0;
                for (int i = 0; i <= data.Count - param; i++)
                {
                    var candidate = data.GetRange(i, param);
                    var res = CubingAvg(candidate, 2);
                    if (i == 0) b = res;
                    if (i == data.Count - param) current.SetValue(this, res.ToStackmatFormat());
                    if (res < b) b = res;
                }
                best.SetValue(this, b.ToStackmatFormat());
            }
            else
            {
                best.SetValue(this, null);
                current.SetValue(this, null);
                //Current100 = null;
                //Best100 = null;
            }
        }

        public int CubingAvg(List<Record> data, int dnf)
        {
            if (data.Where(r => r.DNF).Count() >= dnf) return -1;
            else
            {
                data.Remove(data.Max());
                data.Remove(data.Min());
                return (int)data.Select(r => r.Time).Average();
            }
        }
    }
}
