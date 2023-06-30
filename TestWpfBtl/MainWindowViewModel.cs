
using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace VideoRecoder
{
    internal class MainWindowViewModel : ObservableObject
    {
        #region Private fields
        private FilterInfo _currentDevice;

        private BitmapImage _image;
        private String _ipCameraUrl;

        private bool _isDesktopSource;
        private bool _isIpCameraSource;
        private bool _isWebCamSource;

        private IVideoSource _videoSource;
        private VideoFileWriter _writer;
        private bool _recording;
        private DateTime? _firstframeTime;

        private WaveIn waveIn;


        #endregion Private fields

        #region public fields
        public ObservableCollection<FilterInfo> VideoDevices { get; set; }

        public BitmapImage Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }

        public bool IsDesktopSource
        {
            get { return _isDesktopSource; }
            set { Set(ref _isDesktopSource, value); }
        }
        public bool IsWebCamSource
        {
            get { return _isWebCamSource; }
            set { Set(ref _isWebCamSource, value); }
        }
        public bool IsIpCameraSource
        {
            get { return _isIpCameraSource; }
            set { Set(ref _isIpCameraSource, value); }
        }
        public string IpCameraUrl
        {
            get { return _ipCameraUrl; }
            set { Set(ref _ipCameraUrl, value); }
        }


        public FilterInfo CurrentDevice
        {
            get { return _currentDevice; }
            set { Set(ref _currentDevice, value); }
        }

        public ICommand StartRecordingCommand { get; set; }
        public ICommand StopRecordingCommand { get; set; }
        public ICommand StartSourceCommand { get; set; }
        public ICommand StopSourceCommand { get; set; }
        public ICommand SaveSnapCommand { get; private set; }
        #endregion public fields
        public MainWindowViewModel()
        {
            VideoDevices = new ObservableCollection<FilterInfo>();
            GetVideoDevices();
            StartSourceCommand = new RelayCommand(StartCamera);
            StopSourceCommand = new RelayCommand(StopCamera);
            StartRecordingCommand = new RelayCommand(StartRecording);
            StopRecordingCommand = new RelayCommand(StopRecording);
            SaveSnapCommand = new RelayCommand(SaveSnapShot);
            IsDesktopSource = true;
            IpCameraUrl = "http://88.53.197.250/axis-cgi/mjpg/video/cgi?resolution=320x240";

        }
        private void GetVideoDevices()
        // lấy ra tất cả device camera trong máy 
        {
            var devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo devive in devices)
            {
                VideoDevices.Add(devive);
            }

            if (VideoDevices.Any())
            {
                CurrentDevice = VideoDevices[0];
            }
            else
            {
                System.Windows.MessageBox.Show("ko tim thay camera trong may ban");
            }

        }
        private void StartCamera()
        {
            if (IsDesktopSource)
            {
                var rectangle = new Rectangle();
                foreach (var screen in System.Windows.Forms.Screen.AllScreens)
                {
                    rectangle = Rectangle.Union(rectangle, screen.Bounds);
                }
                _videoSource = new ScreenCaptureStream(rectangle);
                _videoSource.NewFrame += _videoSource_NewFrame;
                _videoSource.Start();
            }
            else if (IsWebCamSource)
                {
                    if (CurrentDevice != null)
                    {
                        _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                        _videoSource.NewFrame += _videoSource_NewFrame;
                        _videoSource.Start();
                    }
                    else
                    {
                        MessageBox.Show("ko co camera trong may ban !");
                    }
                }
            else if (IsIpCameraSource)
            {
                _videoSource = new VideoCaptureDevice(IpCameraUrl);
                _videoSource.NewFrame += _videoSource_NewFrame;
                _videoSource.Start();
            }
            
        }

        private void _videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if (_recording)
                {
                    using(var bitmap = (Bitmap)eventArgs.Frame.Clone())
                    {
                        if(_firstframeTime != null)
                        {
                            _writer.WriteVideoFrame(bitmap, DateTime.Now - _firstframeTime.Value);
                        }
                        else
                        {
                            _writer.WriteVideoFrame(bitmap);
                            _firstframeTime = DateTime.Now;
                        }
                    }
                }
                using(var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    var bi = bitmap.toBitmapImage();
                    bi.Freeze();
                    Dispatcher.CurrentDispatcher.Invoke(() => Image = bi);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Loi tao khung hinh\n" + ex.Message, "loi", MessageBoxButton.OK, MessageBoxImage.Error);
                StopCamera();
            }
        }

        private void StopCamera()
        {
            if(_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= _videoSource_NewFrame;
            }
            Image = null;
        }

        private void StopRecording()
        {

            if (MessageBox.Show("ban co muon dung man hinh ko", "", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {

                _recording = false;
                _writer.Close();
                _writer.Dispose();
                waveIn.StopRecording();
            }
            else
            {
                return;
            }
           
        }

      
        private void StartRecording()
        {

            if (MessageBox.Show("ban co muon ghi man hinh ko", "", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                var dialog = new SaveFileDialog();
                dialog.FileName = "VideoRec";
                dialog.DefaultExt = ".mp4";
                var dialogresult = dialog.ShowDialog();
                if (dialogresult != true)
                {
                    return;
                }
                _firstframeTime = null;
                _writer = new VideoFileWriter();
                _writer.Open(dialog.FileName, (int)Math.Round(Image.Width, 0), (int)Math.Round(Image.Height, 0));
                _recording = true;
                waveIn = new WaveIn();
                waveIn.WaveFormat = new WaveFormat(44100, 16, 2);
                waveIn.StartRecording();

            }
            else
            {
                return;
            }

            
        }

      

        private void SaveSnapShot()
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Snapshot";
            dialog.DefaultExt = ".png";
            var dialogresult = dialog.ShowDialog();
            if (dialogresult != true)
            {
                return;
            }
            var endcoder = new PngBitmapEncoder();
            endcoder.Frames.Add(BitmapFrame.Create(Image));
            using (var filestream = new FileStream(dialog.FileName, FileMode.Create))
            {
                endcoder.Save(filestream);
            }
        }
    }
}
