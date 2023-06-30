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


using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace TestWpfBtl
{
    /// <summary>
    /// Interaction logic for SoundRecord.xaml
    /// </summary>
    public partial class SoundRecord : Window
    {
        public SoundRecord()
        {
            InitializeComponent();
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        private void StartRecord_Click(object sender, RoutedEventArgs e)
        {
            StartEnd.Visibility = Visibility.Visible;
            record("open new type waveaudio alias recsound", "", 0, 0);
            record("record recsound", "", 0, 0);
        }

        private void StopRecord_Click(object sender, RoutedEventArgs e)
        {
            StartEnd.Visibility = Visibility.Collapsed;
            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == true)
            {
                record("save recsound " + dlg.FileName + ".wav", "", 0, 0);
                record("close recsound", "", 0, 0);
                MessageBox.Show("Save file success!");
            }

        }
    }
}
