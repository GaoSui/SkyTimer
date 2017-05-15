using GalaSoft.MvvmLight.Messaging;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkyTimer.View
{
    /// <summary>
    /// Interaction logic for RecordListView.xaml
    /// </summary>
    public partial class RecordListView : UserControl
    {
        public RecordListView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            Messenger.Default.Send(new UpdateScrambleInstruction(e.AddedItems[0] as string));
        }
    }
}
