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


using AForge;
using AForge.Imaging.Filters;

using System.Drawing;
using System.Drawing.Imaging;

using Microsoft.Win32;
using System.IO;


namespace TestWpfBtl
{
    /// <summary>
    /// Interaction logic for PictureEditor.xaml
    /// </summary>
    public partial class PictureEditor : Window
    {
        public PictureEditor()
        {
            InitializeComponent();
        }

        Bitmap newbit, fake;
        private void btnSource_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openDialog.FilterIndex = 1;

            if (openDialog.ShowDialog() == true)
            {
                imageSource.Source = new BitmapImage(new Uri(openDialog.FileName));
                newbit = new Bitmap(openDialog.FileName);
            }
        }

        private void btnFake_Click(object sender, RoutedEventArgs e)
        {

            if (rbGray.IsChecked == true)
            {
                GrayscaleBT709 sp = new GrayscaleBT709();
                fake = sp.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rbSepia.IsChecked == true)
            {
                Sepia sepia = new Sepia();
                fake = sepia.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rbHue.IsChecked == true)
            {
                HueModifier hue = new HueModifier();
                fake = hue.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rbChanel.IsChecked == true)
            {
                RotateChannels rotate = new RotateChannels();
                fake = rotate.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rbInvert.IsChecked == true)
            {
                Invert invert = new Invert();
                fake = invert.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb1RGB.IsChecked == true)
            {
                LevelsLinear filter = new LevelsLinear();
                // set ranges
                filter.InRed = new IntRange(30, 230);
                filter.InGreen = new IntRange(50, 240);
                filter.InBlue = new IntRange(10, 210);
                // apply the filter
                fake = filter.Apply(newbit);

                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb1Bri.IsChecked == true)
            {
                BrightnessCorrection filterBri = new BrightnessCorrection(-50);
                // apply the filter

                fake = filterBri.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb1Con.IsChecked == true)
            {
                ContrastCorrection filterContrast = new ContrastCorrection(15);
                // apply the filter

                fake = filterContrast.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb1Satu.IsChecked == true)
            {
                SaturationCorrection filterSatura = new SaturationCorrection((float)-0.5);
                //// apply the filter

                fake = filterSatura.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb2Color.IsChecked == true)
            {
                //------ Color remapping
                // create map
                byte[] map = new byte[256];
                for (int i = 0; i < 256; i++)
                {
                    map[i] = (byte)Math.Min(255, Math.Pow(2, (double)i / 32));
                }
                // create filter
                ColorRemapping filterColorRemap = new ColorRemapping(map, map, map);
                // apply the filter

                fake = filterColorRemap.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb2Gamma.IsChecked == true)
            {
                GammaCorrection filterGamma = new GammaCorrection(0.5);
                // apply the filter

                fake = filterGamma.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb3Color.IsChecked == true)
            {
                //---Color Filtering
                // create filter
                ColorFiltering filterColor = new ColorFiltering();
                // set color ranges to keep
                filterColor.Red = new IntRange(100, 255);
                filterColor.Green = new IntRange(0, 75);
                filterColor.Blue = new IntRange(0, 75);
                // apply the filter

                fake = filterColor.Apply(newbit);
                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb3Chanel.IsChecked == true)
            {
                //// create filter
                ChannelFiltering filterChanelFFF = new ChannelFiltering();
                //// set channels' ranges to keep
                filterChanelFFF.Red = new IntRange(0, 255);
                filterChanelFFF.Green = new IntRange(100, 255);
                filterChanelFFF.Blue = new IntRange(100, 255);
                // apply the filter
                fake = filterChanelFFF.Apply(newbit);

                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb3HSL.IsChecked == true)
            {
                //---------- HSL Filtering
                //// create filter
                HSLFiltering filterHSL = new HSLFiltering();
                // set color ranges to keep
                filterHSL.Hue = new IntRange(335, 0);
                filterHSL.Saturation = new Range(0.6f, 1);
                filterHSL.Luminance = new Range(0.1f, 1);
                // apply the filter
                fake = filterHSL.Apply(newbit);

                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb3YCB.IsChecked == true)
            {
                // create filter
                YCbCrFiltering filterYCB = new YCbCrFiltering();
                // set color ranges to keep
                filterYCB.Cb = new Range(-0.2f, 0);
                filterYCB.Cr = new Range(0.26f, (float)0.5);
                // apply the filter
                fake = filterYCB.Apply(newbit);

                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb4Convolution.IsChecked == true)
            {
                //----- convolution
                // define emboss kernel
                int[,] kernel = {
                { -2, -1,  0 },
                { -1,  1,  1 },
                {  0,  1,  2 } };
                // create filter
                Convolution filterConvo = new Convolution(kernel);
                // apply the filter
                fake = filterConvo.Apply(newbit);

                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb4Blur.IsChecked == true)
            {
                Blur filterBlur = new Blur();
                fake = filterBlur.Apply(newbit);

                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb4Shapen.IsChecked == true)
            {
                Sharpen filterShapen = new Sharpen();
                fake = filterShapen.Apply(newbit);

                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rb4Edges.IsChecked == true)
            {
                Edges filterEdge = new Edges();
                fake = filterEdge.Apply(newbit);

                imageFake.Source = BitmapToImageSource(fake);
            }
            else if (rbNone.IsChecked == true)
            {
                imageFake.Source = BitmapToImageSource(newbit);
            }

        }

        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "png";
            dlg.Filter = "Png Files|*.png";

            if (dlg.ShowDialog() == true)
            {
                if (rbSepia.IsChecked == true || rbInvert.IsChecked == true || rbGray.IsChecked == true || rbHue.IsChecked == true || rbChanel.IsChecked == true)
                {
                    fake.Save(dlg.FileName, ImageFormat.Png);
                }
                else newbit.Save(dlg.FileName, ImageFormat.Png);

                MessageBox.Show("Saved Success!");
            }
        }
    }
}
