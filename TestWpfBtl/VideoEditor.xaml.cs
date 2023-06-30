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

using System.Windows.Threading;

namespace TestWpfBtl
{
    /// <summary>
    /// Interaction logic for VideoEditor.xaml
    /// </summary>
    public partial class VideoEditor : Window
    {

        DispatcherTimer timer;
        public VideoEditor()
        {
            InitializeComponent();
            main.AllowDrop = true;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_tick);
        }

        private void timer_tick(object sender, EventArgs e)
        {
            seek.Value = mediaVideo.Position.TotalSeconds;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaVideo.Play();
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            mediaVideo.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaVideo.Stop();
        }

        private void Vol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaVideo.Volume = (Double)Vol.Value;
        }

        private void seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaVideo.Position = TimeSpan.FromSeconds(seek.Value);
        }



        private void main_Drop(object sender, DragEventArgs e)
        {
            String filename = (String)((DataObject)e.Data).GetFileDropList()[0];
            mediaVideo.Source = new Uri(filename);

            mediaVideo.LoadedBehavior = MediaState.Manual;
            mediaVideo.UnloadedBehavior = MediaState.Manual;
            mediaVideo.Volume = (double)Vol.Value;
            mediaVideo.Play();
        }

        private void mediaVideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mediaVideo.NaturalDuration.TimeSpan;
            seek.Maximum = ts.TotalSeconds;
            timer.Start();
        }

    }
}
