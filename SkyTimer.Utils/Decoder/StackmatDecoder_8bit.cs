using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyTimer.Utils.Decoder
{
    public partial class StackmatDecoder_8bit
    {
        public StackmatDecoder_8bit(int sampleRate)
        {
            zeroThreshold = (int)(sampleRate / 1000.0 * ZeroThresholdConst * 0.98);

            normalizeFactor = sampleRate / 1000.0 * StackmatBaseValue;
        }

        public const double ZeroThresholdConst = 50.84;//(ms)
        public const double StackmatBaseValue = 0.82;//(ms)

        private int zeroThreshold;
        private double normalizeFactor;

        private List<int> Buffer = new List<int>();
        private int counter0 = 0;
        private int counter1 = 0;
        private List<int> decodedValues = new List<int> { 0, 0, 0, 0, 0, 0 };
        private List<int> values = new List<int>();


        public void Decode(byte[] ssData)
        {
            if (Buffer.Count > 80 || counter0 > 3000 || counter1 > 3000)
            {
                Buffer.Clear();
                counter1 = 0;
                counter0 = 0;

                return;
            }

            int value = 0;
            var threshold = (int)((ssData.Max() - ssData.Min()) * 0.2);
            values.Clear();

            //Find the first switch to determine the value
            for (int i = 1; i < ssData.Length; i++)
            {
                if (ssData[i] - ssData[i - 1] > threshold)
                {
                    value = 0;
                    break;
                }
                else if (ssData[i - 1] - ssData[i] > threshold)
                {
                    value = 1;
                    break;
                }
            }

            values.Add(value);
            for (int i = 1; i < ssData.Length; i++)
            {
                if (ssData[i] - ssData[i - 1] > threshold)
                {
                    value = 1;
                }
                else if (ssData[i - 1] - ssData[i] > threshold)
                {
                    value = 0;
                }
                values.Add(value);
            }

            for (int i = 0; i < values.Count; i++)
            {
                //if (ssData[i] > 127)
                if (values[i] == 1)
                {
                    counter1++;
                    if (counter0 != 0)
                    {
                        if (counter0 >= zeroThreshold)
                        {
                            if (Buffer.Count > 40)
                            {
                                var frame = new StackmatFrame();
                                if (DecodeFrame(frame)) TimeUpdated(this, frame);
                            }
                            Buffer.Clear();
                        }
                        else
                        {
                            Buffer.Add(counter0);
                        }
                        counter0 = 0;
                    }
                }
                else
                {
                    counter0++;
                    if (counter1 != 0)
                    {
                        Buffer.Add(counter1);
                        counter1 = 0;
                    }
                }
            }
        }

        private bool DecodeFrame(StackmatFrame frame)
        {
            int index = 8, n1 = Normalize(Buffer[0]), n2 = Normalize(Buffer[1]), n3 = Normalize(Buffer[2]);

            switch (n1)
            {
                case 1:
                    switch (n2)
                    {
                        case 1:
                            switch (n3)
                            {
                                case 2:
                                    frame.Status = StackmatStatus.Zero;
                                    break;
                                case 5:
                                    frame.Status = StackmatStatus.Green;
                                    index = 6;
                                    break;
                                default:
                                    return false;
                            }
                            break;
                        case 2:
                            switch (n3)
                            {
                                case 2:
                                    frame.Status = StackmatStatus.Result;
                                    break;
                                case 4:
                                    frame.Status = StackmatStatus.Red;
                                    index = 6;
                                    break;
                                default:
                                    return false;
                            }
                            break;
                        default:
                            return false;
                    }
                    break;
                case 2:
                    frame.Status = StackmatStatus.RightHand;
                    break;
                case 3:
                    frame.Status = StackmatStatus.LeftHand;
                    index = 6;
                    break;
                case 6:
                    frame.Status = StackmatStatus.Timing;
                    index = 4;
                    break;
                default:
                    return false;
            }



            for (int i = 0; i < 6; i++)
            {
                n1 = Normalize(Buffer[index]);
                n2 = Normalize(Buffer[index + 1]);
                n3 = Normalize(Buffer[index + 2]);

                switch (n1)
                {
                    case 1:
                        switch (n2)
                        {
                            case 1:
                                switch (n3)
                                {
                                    case 1:
                                        decodedValues[i] = (5);
                                        index += 8;
                                        break;
                                    case 2:
                                        decodedValues[i] = (9);
                                        index += 6;
                                        break;
                                    case 3:
                                        decodedValues[i] = (1);
                                        index += 6;
                                        break;
                                    default:
                                        return false;
                                }
                                break;
                            case 2:
                                decodedValues[i] = (3);
                                index += 6;
                                break;
                            case 3:
                                decodedValues[i] = (7);
                                index += 6;
                                break;
                            default:
                                return false;
                        }
                        break;
                    case 2:
                        switch (n2)
                        {
                            case 1:
                                decodedValues[i] = (2);
                                index += 6;
                                break;
                            case 2:
                                decodedValues[i] = (6);
                                index += 6;
                                break;
                            default:
                                return false;
                        }
                        break;
                    case 3:
                        decodedValues[i] = (4);
                        index += 6;
                        break;
                    case 4:
                        decodedValues[i] = (8);
                        index += 4;
                        break;
                    case 5:
                        decodedValues[i] = (0);
                        index += 4;
                        break;
                    default:
                        return false;
                }
            }

            frame.Time = decodedValues[0] * 60000 + decodedValues[1] * 10000 + decodedValues[2] * 1000 + decodedValues[3] * 100 + decodedValues[4] * 10 + decodedValues[5];
            return true;
        }

        public event EventHandler<StackmatFrame> TimeUpdated;
        //public event EventHandler LostConnection;

        public int Normalize(int value)
        {
            return (int)(value / normalizeFactor + 0.5);
        }
    }
}
