using System;
using System.Collections.Generic;

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
        private List<int> values = new List<int> { 0, 0, 0, 0, 0, 0 };


        public void Decode(byte[] ssData)
        {
            if (Buffer.Count > 80 || counter0 > 3000 || counter1 > 3000)
            {
                Buffer.Clear();
                counter1 = 0;
                counter0 = 0;
                //LostConnection(this, null);

                return;
            }

            for (int i = 0; i < ssData.Length; i++)
            {
                if (ssData[i] > 127)
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
                                        values[i] = (5);
                                        index += 8;
                                        break;
                                    case 2:
                                        values[i] = (9);
                                        index += 6;
                                        break;
                                    case 3:
                                        values[i] = (1);
                                        index += 6;
                                        break;
                                    default:
                                        return false;
                                }
                                break;
                            case 2:
                                values[i] = (3);
                                index += 6;
                                break;
                            case 3:
                                values[i] = (7);
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
                                values[i] = (2);
                                index += 6;
                                break;
                            case 2:
                                values[i] = (6);
                                index += 6;
                                break;
                            default:
                                return false;
                        }
                        break;
                    case 3:
                        values[i] = (4);
                        index += 6;
                        break;
                    case 4:
                        values[i] = (8);
                        index += 4;
                        break;
                    case 5:
                        values[i] = (0);
                        index += 4;
                        break;
                    default:
                        return false;
                }
            }

            frame.Time = values[0] * 60000 + values[1] * 10000 + values[2] * 1000 + values[3] * 100 + values[4] * 10 + values[5];
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
