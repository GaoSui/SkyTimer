using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyTimer.Utils.Decoder
{
    public partial class StackmatDecoder_8bit
    {
        public List<List<int>> GetGroups(byte[] ssData, bool normalize = true)
        {
            var list = new List<List<int>>();
            var buffer = new List<int>();

            for (int i = 0; i < ssData.Length; i++)
            {
                if (ssData[i] > 127)
                {
                    counter1++;
                    if (counter0 != 0)
                    {
                        if (counter0 >= zeroThreshold)
                        {
                            if (normalize) list.Add(buffer.Select(value => Normalize(value)).ToList());
                            else list.Add(buffer);
                            buffer = new List<int>();
                        }
                        else
                        {
                            buffer.Add(counter0);
                        }
                        counter0 = 0;
                    }
                }
                else
                {
                    counter0++;
                    if (counter1 != 0)
                    {
                        buffer.Add(counter1);
                        counter1 = 0;
                    }
                }
            }

            return list;
        }

        public List<int> GetSegments(byte[] ssData)
        {
            var list = new List<int>();

            for (int i = 0; i < ssData.Length; i++)
            {
                if (ssData[i] > 127)
                {
                    counter1++;
                    if (counter0 != 0)
                    {
                        list.Add(counter0);
                        counter0 = 0;
                    }
                }
                else
                {
                    counter0++;
                    if (counter1 != 0)
                    {
                        list.Add(counter1);
                        counter1 = 0;
                    }
                }
            }

            return list;
        }

        public List<double> GetSegmentAverage(byte[] ssData)
        {
            var res = new List<double>();
            var raw = GetSegments(ssData);
            var notRaw = raw.Select(value => Normalize(value)).ToList();
            var c1 = new List<double>();
            var c2 = new List<double>();
            var c3 = new List<double>();
            var c4 = new List<double>();
            var c5 = new List<double>();
            var c6 = new List<double>();
            var c7 = new List<double>();
            var c8 = new List<double>();

            for (int i = 0; i < notRaw.Count; i++)
            {
                var n = notRaw[i];
                if (n == 1) c1.Add(raw[i] / 48000.0 * 1000);
                else if (n == 2) c2.Add(raw[i] / 48000.0 * 1000);
                else if (n == 3) c3.Add(raw[i] / 48000.0 * 1000);
                else if (n == 4) c4.Add(raw[i] / 48000.0 * 1000);
                else if (n == 5) c5.Add(raw[i] / 48000.0 * 1000);
                else if (n == 6) c6.Add(raw[i] / 48000.0 * 1000);
                else if (n == 7) c7.Add(raw[i] / 48000.0 * 1000);
                else if (raw[i] > 2400) c8.Add(raw[i] / 48000.0 * 1000);
            }

            res.Add(c1.Average());
            res.Add(c2.Average());
            res.Add(c3.Average());
            res.Add(c4.Average());
            res.Add(c5.Average());
            res.Add(c6.Average());
            res.Add(c7.Average());
            res.Add(c8.Average());

            return res;
        }
    }
}
