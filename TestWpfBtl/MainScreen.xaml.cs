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

namespace TestWpfBtl
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow VideoScreen = new MainWindow();
            VideoScreen.Show();
        }

        private void BtnSound_Click(object sender, RoutedEventArgs e)
        {
            SoundRecord RecordSound = new SoundRecord();
            RecordSound.Show();
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            ScreenCapture CaptureScreen = new ScreenCapture();
            CaptureScreen.Show();
            ScreenWin.Hide();
        }

        private void BtnPictureEditor_Click(object sender, RoutedEventArgs e)
        {
            PictureEditor pictureEditor = new PictureEditor();
            pictureEditor.Show();
        }

        private void BtnVideoEditor_Click(object sender, RoutedEventArgs e)
        {
            VideoEditor videoEditor = new VideoEditor();
            videoEditor.Show();
        }
    }
}
