using SkyTimer.Helper;
using System;

namespace SkyTimer.Model
{
    [Serializable]
    public class Record : IComparable<Record>
    {
        public int Time { get; set; }

        public string Scramble { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool PlusTwo { get; set; }

        public bool DNF { get; set; }

        public string Comment { get; set; }

        public int CompareTo(Record other)
        {
            if (DNF && !other.DNF) return 1;
            if (DNF && other.DNF) return 0;
            if (!DNF && other.DNF) return -1;
            return Time.CompareTo(other.Time);
        }

        public override string ToString()
        {
            return Time.ToStackmatFormat();
        }
    }
}
