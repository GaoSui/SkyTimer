using SkyTimer.Model;
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
using System.Windows.Shapes;

namespace SkyTimer.View
{
    /// <summary>
    /// Interaction logic for PlotWindow.xaml
    /// </summary>
    public partial class PlotWindow : Window
    {
        public PlotWindow(List<Record> data)
        {
            InitializeComponent();

            var origin = data.Count;

            data.RemoveAll(r => r.DNF);
            this.data = data;
            candidate = data;

            lblCount.Content = $"{data.Count} / {origin} solves";

            sliRound.Minimum = 2;
            sliRound.Maximum = candidate.Count;
            sliRound.Value = candidate.Count;
        }

        private List<Record> data;
        private List<Record> candidate;
        private bool loaded;

        private const int xResolution = 1280;
        private const int yResolution = 720;

        private WriteableBitmap bmp = new WriteableBitmap(xResolution, yResolution, 0, 0, PixelFormats.Pbgra32, null);



        private void Plot()
        {
            if (candidate.Count < 2) return;

            var factorY = (double)yResolution / candidate.Max().Time;
            var points = new List<int>(candidate.Count * 2);

            var factorX = (double)xResolution / (candidate.Count - 1);

            for (int i = 0; i < candidate.Count; i++)
            {
                points.Add((int)(i * factorX));
                points.Add((int)(yResolution - candidate[i].Time * factorY));
            }

            bmp.Clear();
            bmp.DrawPolyline(points.ToArray(), Colors.White);
            img.Source = bmp;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Plot();
            loaded = true;
        }

        private void btnDay_Click(object sender, RoutedEventArgs e)
        {
            var lastDay = DateTime.Today;
            candidate = data.Where(r => r.TimeCreated > lastDay).ToList();
            Plot();
        }

        private void sliRound_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!loaded) return;
            var count = (int)e.NewValue;
            candidate = data.GetRange(data.Count - count, count);
            Plot();
        }

        private void sliDay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!loaded) return;
            var day = DateTime.Now - TimeSpan.FromDays(e.NewValue);
            candidate = data.Where(r => r.TimeCreated > day).ToList();
            Plot();
        }
    }
}
