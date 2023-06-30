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

using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;

namespace TestWpfBtl
{
    /// <summary>
    /// Interaction logic for ScreenCapture.xaml
    /// </summary>
    public partial class ScreenCapture : Window
    {
        public ScreenCapture()
        {
            InitializeComponent();
            captureWindows.Width = imgPreview.Width = SystemParameters.PrimaryScreenWidth;
            captureWindows.Height = imgPreview.Height = SystemParameters.PrimaryScreenHeight;
        }

        // ========================== phan ve hinh chu nhat khung ================

        System.Windows.Point pointS;
        System.Windows.Shapes.Rectangle rect;
        Int32 top, left, W, H;
        private bool isselect = false;
        private void select_Click(object sender, RoutedEventArgs e)
        {
            if (isselect)
            {
                isselect = false;
            }
            else isselect = true;
            isUsingSelect();
        }
        private void drawing_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (isselect)
            {
                pointS = e.GetPosition(drawing);

                if (rect != null)
                {
                    drawing.Children.Remove(rect);
                }

                rect = new System.Windows.Shapes.Rectangle
                {
                    Stroke = System.Windows.Media.Brushes.LightBlue,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(rect, pointS.X);
                Canvas.SetTop(rect, pointS.X);

                drawing.Children.Add(rect);

                top = (int)pointS.X;
                left = (int)pointS.Y;
            }

        }

        private void drawing_MouseMove(object sender, MouseEventArgs e)
        {
            if (isselect)
            {
                if (e.LeftButton == MouseButtonState.Released || rect == null)
                    return;

                var pos = e.GetPosition(drawing);

                var x = Math.Min(pos.X, pointS.X);
                var y = Math.Min(pos.Y, pointS.Y);

                var width = Math.Abs(pos.X - pointS.X);
                var height = Math.Abs(pos.Y - pointS.Y);
                //var w = Math.Max(pos.X, pointS.X) - x;
                //var h = Math.Max(pos.Y, pointS.Y) - y;

                rect.Height = height;
                rect.Width = width;

                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);

                W = (int)width;
                H = (int)height;
            }




            //========= cua phan paint ====
            if (isPainting)
            {
                if (isPaint)
                {
                    System.Windows.Point mouseposition = e.GetPosition(drawing);
                    paintCircle(burshColor, mouseposition);
                }
                if (isErase)
                {
                    System.Windows.Point mouseposition = e.GetPosition(drawing);
                    paintCircle(burshColor, mouseposition);
                }
            }

            // ======== cua phan paitn ===============
        }

        private void drawing_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
        }

        // ================== het phan tao khung chu nhat ===================






        //================================= phan chup anh =========================
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Ban muon chup man hinh?", "Notice", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Save_ScreenShoot_as_file();
            }
            else return;

        }

        private void Save_ScreenShoot_as_file()
        {
           // String filename = "ScreenCapture-" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".png";

            //<Size>
            int screenleft = left;
            int screentop = top;
            int screenwidth = W;
            int screenheight = H;
            //<Size

            if (screenwidth == 0 || screenheight == 0)
            {
                screenleft = (int)SystemParameters.VirtualScreenLeft;
                screentop = (int)SystemParameters.VirtualScreenTop;
                screenwidth = (int)SystemParameters.VirtualScreenWidth;
                screenheight = (int)SystemParameters.VirtualScreenHeight;
            }

            Bitmap bitmap_screen = new Bitmap(screenwidth, screenheight);

            Graphics g = Graphics.FromImage(bitmap_screen);

            g.CopyFromScreen(screentop, screenleft, 0, 0, bitmap_screen.Size);


           // bitmap_screen.Save("C:\\Users\\Admin\\Desktop\\Note\\" + filename);




            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "png";
            dlg.Filter = "Png Files|*.png";

            if (dlg.ShowDialog() == true) {
                bitmap_screen.Save(dlg.FileName, ImageFormat.Png);
                MessageBox.Show("ScreenShot Success!");
            }
                


            
        }

        // ========================== het phan chup anh =============================





        // ========================= phan tao nut bam ===============================
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // Application.Current.Shutdown();
            captureWindows.Close();
            MainScreen mainxx = new MainScreen();
            mainxx.Show();
        }

        private void tool_Click(object sender, RoutedEventArgs e)
        {
            toolBar.Visibility = toolBar.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            string more = "V";
            string less = "-";
            if ((string)tool.Content == more)
            {
                tool.Content = less;
            }
            else
            {
                tool.Content = more;
            }
        }

        public void isUsingDraw()
        {
            
            if (StartDraw.Background == System.Windows.Media.Brushes.LightGray)
            {
                StartDraw.Background = System.Windows.Media.Brushes.Aqua;
                undo.IsEnabled = false;
                RemoveAll.IsEnabled = false;
            }
            else
            {
                StartDraw.Background = System.Windows.Media.Brushes.LightGray;
                undo.IsEnabled = true;
                RemoveAll.IsEnabled = true;
            }
        }

        public void isUsingPaint()
        {

            if (paint.Background == System.Windows.Media.Brushes.LightGray)
            {
                paint.Background = System.Windows.Media.Brushes.Aqua;
                TextBox.IsEnabled = false;
                select.IsEnabled = false;
            }
            else
            {
                paint.Background = System.Windows.Media.Brushes.LightGray;
                TextBox.IsEnabled = true;
                select.IsEnabled = true;
            }
        }

        public void isUsingSelect()
        {
            if (select.Background == System.Windows.Media.Brushes.LightGray)
            {
                select.Background = System.Windows.Media.Brushes.Aqua;
                paint.IsEnabled = false;
                TextBox.IsEnabled = false;
            }
            else
            {
                select.Background = System.Windows.Media.Brushes.LightGray;
                paint.IsEnabled = true;
                TextBox.IsEnabled = true;
            }
        }


        // ======================== het phan tao nut =======================






        // ================== phan tao textbox =====================
        private void TextBox_Click(object sender, RoutedEventArgs e)
        {

            Canvas.SetZIndex(StackText, 99);

            TextBox txb = new TextBox
            {
                FontSize = 30,
                Background = System.Windows.Media.Brushes.Transparent,
                Foreground = System.Windows.Media.Brushes.White

            };
            StackText.Children.Add(txb);
        }

        // ===================== het phan tao texbox ===================







        // ================= phan tao paint =========================

        private int diameter = 5;
        private System.Windows.Media.Brush burshColor = System.Windows.Media.Brushes.White;
        private bool isPaint = false;
        private bool isErase = false;
        private bool isPainting = false;

        private void StartDraw_Click(object sender, RoutedEventArgs e)
        {
            if (isPainting)
            {
                isPainting = false;
            }
            else isPainting = true;

            isUsingDraw();
        }
        private void paitn_Click(object sender, RoutedEventArgs e)
        {
            toolPaint.Visibility = toolPaint.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            isUsingPaint();
        }

        private void paintCircle(System.Windows.Media.Brush CircleColor, System.Windows.Point position)
        {
            Ellipse newellipse = new Ellipse();
            newellipse.Fill = CircleColor;
            newellipse.Width = diameter;
            newellipse.Height = diameter;
            Canvas.SetTop(newellipse, position.Y);
            Canvas.SetLeft(newellipse, position.X);
            drawing.Children.Add(newellipse);
        }

        private void red_Checked_1(object sender, RoutedEventArgs e)
        {
            burshColor = System.Windows.Media.Brushes.Red;
        }

        private void blue_Checked_1(object sender, RoutedEventArgs e)
        {
            burshColor = System.Windows.Media.Brushes.Blue;
        }

        private void yellow_Checked_1(object sender, RoutedEventArgs e)
        {
            burshColor = System.Windows.Media.Brushes.Yellow;
        }

        private void line_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            diameter = (int)line.Value;
        }

        private void undo_Click_1(object sender, RoutedEventArgs e)
        {
            int count = drawing.Children.Count;
            if (count > 3)
            {
                drawing.Children.RemoveAt(count - 1);
            }

        }

        private void RemoveAll_Click(object sender, RoutedEventArgs e)
        {
            int count = drawing.Children.Count;
            if (count > 0)
            {
                drawing.Children.RemoveRange(4, count);
            }

        }

        private void drawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isPainting)
            {
                isPaint = true;
            }
        }

        private void drawing_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isPaint = false;
        }

        // ================= het phan tao paint =========================
    }
}
