using Microsoft.Win32;
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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double cpos = 110.0;
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            //textBoxIncomingMessage.Text = "Now is the time for all good men to come to the aid of their country.";
            //textBoxOutgoingMessage.Text = "Whoever said that learning WPF is easy, is out of their effing minds.";
        }

        private void btnSetName_Click(object sender, RoutedEventArgs e)
        {
            textBlockName.Text = textBoxName.Text;

            textBoxName.Clear();
        }

        private void btnSetClock_Click(object sender, RoutedEventArgs e)
        {
            textBlockClock.Text = textBoxClock.Text;
            textBoxClock.Clear();
        }

        private void btnSetIncomingMessage_Click(object sender, RoutedEventArgs e)
        {
            TextBubbleRight tbr = new TextBubbleRight();
            tbr.SetMessage(textBoxIncomingMessage.Text);
            
            ListViewItem lvi = new ListViewItem();
            lvi.Content = tbr;
            lvi.Height = tbr.BubbleHeight;
            lvi.Margin = new Thickness(130, 0, 0, 0);
            //lvi.Background = new SolidColorBrush(Colors.Red);
            lvi.VerticalContentAlignment = VerticalAlignment.Top;
            listView.Items.Add(lvi);
            listView.UpdateLayout();
            //appCanvas.Children.Add(tbr);
            //Canvas.SetTop(tbr, cpos);
            //Canvas.SetLeft(tbr, 150);
            //cpos += tbr.BubbleHeight;
            AddImageToList();
        }

        private void btnSetOutgoingMessage_Click(object sender, RoutedEventArgs e)
        {
            TextBubbleLeft tbl = new TextBubbleLeft();
            tbl.SetMessage(textBoxOutgoingMessage.Text);

            ListViewItem lvi = new ListViewItem();
            lvi.Content = tbl;
            lvi.Height = tbl.BubbleHeight;
            lvi.Width = 400;
            lvi.Margin = new Thickness(0, 0, 0, 0);
            //lvi.Background = new SolidColorBrush(Colors.Blue);
            lvi.VerticalContentAlignment = VerticalAlignment.Top;
            listView.Items.Add(lvi);
            listView.UpdateLayout();
            //appCanvas.Children.Add(tbl);
            //Canvas.SetTop(tbl, cpos);
            //Canvas.SetLeft(tbl, 10);
            //cpos += tbl.BubbleHeight;
            AddImageToList();
        }


        private void btnSetIncomingImage_Click(object sender, RoutedEventArgs e)
        {
            string fileName;
            double ibHeight = 0.0;

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                fileName = ofd.FileName;
                ImageBubble ib = new ImageBubble();
                ibHeight = ib.SetImage(fileName, ImageBubble.Orientation.Incoming);

                ListViewItem lvi = new ListViewItem();
                lvi.Content = ib;
                lvi.VerticalContentAlignment = VerticalAlignment.Top;
                lvi.Margin = new Thickness(205, 0, 0, 0);
                lvi.Height = ibHeight;
                listView.Items.Add(lvi);
                listView.UpdateLayout();
                AddImageToList();
            }
        }

        private void btnSetOutgoingImage_Click(object sender, RoutedEventArgs e)
        {
            string fileName;
            double ibHeight = 0.0;

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                fileName = ofd.FileName;
                ImageBubble ib = new ImageBubble();
                ibHeight = ib.SetImage(fileName, ImageBubble.Orientation.Outgoing);

                ListViewItem lvi = new ListViewItem();
                lvi.Content = ib;
                lvi.VerticalContentAlignment = VerticalAlignment.Top;
                lvi.Margin = new Thickness(5, 0, 0, 0);
                lvi.Height = ibHeight;
                listView.Items.Add(lvi);
                listView.UpdateLayout();
                AddImageToList();
            }
        }

        private void btnClearScreen_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.Clear();
        }

        private void btnClearLastItem_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.MoveCurrentToLast();
            listView.Items.Remove(listView.Items.CurrentItem);
        }

        private void btnSaveScreen_Click(object sender, RoutedEventArgs e)
        {
            SaveScreen();
        }

        private void SaveScreen()
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)appCanvas.RenderSize.Width,
                (int)appCanvas.RenderSize.Height,
                96,
                96,
                System.Windows.Media.PixelFormats.Default);

            rtb.Render(appCanvas);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();

            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            string timey = DateTime.Now.ToString();
            string fileName = String.Format("image.{0}.{1:yyyy.MM.dd.hh.mm.ss}.png", textBlockName.Text, DateTime.Now);
            using (var fs = System.IO.File.OpenWrite(fileName))
            {
                pngEncoder.Save(fs);
                fs.Close();
            }
        }

        private void SaveNamedImage(string fileName)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)appCanvas.RenderSize.Width,
                (int)appCanvas.RenderSize.Height,
                96,
                96,
                System.Windows.Media.PixelFormats.Default);

            rtb.Render(appCanvas);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();

            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            string timey = DateTime.Now.ToString();
            using (var fs = System.IO.File.OpenWrite(fileName))
            {
                pngEncoder.Save(fs);
                fs.Close();
            }
        }

        private void AddImageToList()
        {
            string fileName = String.Format("incimage.{0}.{1:yyyy.MM.dd.hh.mm.ss}.png", textBlockName.Text, DateTime.Now);
            SaveNamedImage(fileName);
            Image targetImage = new Image();
            targetImage.Stretch = Stretch.Uniform;
            //targetImage.Source = new BitmapImage(new Uri(fileName, UriKind.Relative));
            //image1 = targetImage;
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(fileName, UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            targetImage.Source = src;
            //image1.Source = new BitmapImage(new Uri(fileName, UriKind.Relative));
            ListViewItem lvi = new ListViewItem();
            lvi.Height = 300;
            lvi.Content = targetImage;
            listViewImages.Items.Add(lvi);
        }
    }
}
