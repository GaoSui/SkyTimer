using GalaSoft.MvvmLight.Messaging;
using SkyTimer.Helper;
using SkyTimer.Model;
using SkyTimer.MVVM;
using SkyTimer.Properties;
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
        public string Best { get { return best; } set { Set(ref best, Resources.Best + value); } }

        private string worst;
        public string Worst { get { return worst; } set { Set(ref worst, Resources.Worst + value); } }

        private string all;
        public string All { get { return all; } set { Set(ref all, Resources.All + value); } }

        private string current5;
        public string Current5 { get { return current5; } set { Set(ref current5, Resources.Current5 + value); } }

        private string current12;
        public string Current12 { get { return current12; } set { Set(ref current12, Resources.Current12 + value); } }

        private string current100;
        public string Current100 { get { return current100; } set { Set(ref current100, Resources.Current100 + value); } }

        private string best5;
        public string Best5 { get { return best5; } set { Set(ref best5, Resources.Best5 + value); } }

        private string best12;
        public string Best12 { get { return best12; } set { Set(ref best12, Resources.Best12 + value); } }

        private string best100;
        public string Best100 { get { return best100; } set { Set(ref best100, Resources.Best100 + value); } }

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
