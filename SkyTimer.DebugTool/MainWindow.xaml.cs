using Microsoft.Win32;
using NAudio.Wave;
using SkyTimer.Utils.Decoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkyTimer.DebugTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private WaveFileReader reader;
        private StackmatDecoder_8bit decoder;
        private const int xResolution = 1280;
        private const int yResolution = 720;
        private WriteableBitmap bmp = new WriteableBitmap(1280, 720, 0, 0, PixelFormats.Pbgra32, null);

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                reader = new WaveFileReader(dialog.FileName);
                decoder = new StackmatDecoder_8bit(reader.WaveFormat.SampleRate);

                decoder.TimeUpdated += Decoder_TimeUpdated;
                //decoder.ValuePushed += Decoder_ValuePushed;
                //decoder.BufferCleared += Decoder_BufferCleared;
            }
        }

        private void Decoder_BufferCleared()
        {
            output.Text = "";
        }

        private void Decoder_ValuePushed(object sender, int e)
        {
            output.Text += $" {decoder.Normalize(e)}";
        }

        private void Decoder_TimeUpdated(object sender, StackmatFrame e)
        {
            Title = $"{e.Status} {TimeSpan.FromMilliseconds(e.Time).ToString("m\\:ss\\.fff")}";
        }

        private void btnDecode_Click(object sender, RoutedEventArgs e)
        {
            if (reader == null || decoder == null) return;
            bmp.Clear();
            var buffer = new byte[reader.WaveFormat.SampleRate / 10];
            var points = new List<int>();
            reader.Read(buffer, 0, reader.WaveFormat.SampleRate / 10);

            var factorX = (double)xResolution / (buffer.Length - 1);
            var factorY = (double)yResolution / buffer.Max();

            for (int i = 0; i < buffer.Length; i++)
            {
                points.Add((int)(i * factorX));
                points.Add((int)(yResolution - buffer[i] * factorY));
            }

            bmp.DrawPolyline(points.ToArray(), Colors.Black);
            img.Source = bmp;

            decoder.Decode(buffer);

            //var sb = new StringBuilder();
            //foreach (var item in decoder.Buffer)
            //{
            //    sb.Append($"{item} ");
            //}

            //txtGroup.Text = sb.ToString();
            //sb.Clear();
            //foreach (var item in decoder.Buffer)
            //{
            //    sb.Append($"{decoder.Normalize(item)} ");
            //}
            //txtnGroup.Text = sb.ToString();
        }

        private void btnOut_Click(object sender, RoutedEventArgs e)
        {
            if (reader == null || decoder == null) return;
            reader.Position = 0;
            var buffer = new byte[reader.Length];
            reader.Read(buffer, 0, (int)reader.Length);
            var res = decoder.GetGroups(buffer, true);
            var sb = new StringBuilder();
            foreach (var item in res)
            {
                foreach (var i in item)
                {
                    sb.Append($"{i} ");
                }
                sb.Append("\n\n");
            }
            output.Text = sb.ToString();
        }
    }
}
