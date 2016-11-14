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
    /// Interaction logic for ImageBubble.xaml
    /// </summary>
    public partial class ImageBubble : UserControl
    {
        public enum Orientation { Outgoing, Incoming };

        private Path ImageBubbleLeftPortrait = new Path();
        private Path ImageBubbleLeftLandscape = new Path();
        private Path ImageBubbleRightPortrait = new Path();
        private Path ImageBubbleRightLandscape = new Path();

        public ImageBubble()
        {
            InitializeComponent();
            InitializeGeometries();
        }

        private void InitializeGeometries()
        {
            ImageBubbleLeftPortrait.Data = Geometry.Parse("M27.410999,0.5 L153.411,0.5 C163.90441,0.5 172.411,9.0065899 172.411,19.5 L172.411,200.5 C172.411,210.99341 163.90441,219.5 153.411,219.5 L27.410999,219.5 C23.148051,219.5 19.213007,218.09608 16.042997,215.72537 L15.924974,215.63263 15.791964,215.7357 C12.447578,218.10652 8.3615919,219.5 3.9501491,219.5 2.8887467,219.5 1.8461871,219.41933 0.82826424,219.26379 L0.5,219.20941 0.60664988,219.12764 C5.3825135,215.37439 8.4500552,209.54544 8.4500567,203 8.4500552,202.68489 8.4429448,202.37144 8.4288775,202.0598 L8.4213751,201.92683 8.4333255,201.92195 8.4685186,201.90901 8.4357241,201.47774 C8.4193065,201.1539 8.4109993,200.82791 8.4110009,200.5 L8.4110009,19.5 C8.4109993,9.0065899 16.917589,0.5 27.410999,0.5 z");
            ImageBubbleLeftLandscape.Data = Geometry.Parse("M27.366,0.5 L208.366,0.5 C218.85941,0.49999946 227.366,9.0065894 227.366,19.499999 L227.366,145.5 C227.366,155.99341 218.85941,164.5 208.366,164.5 L27.366,164.5 C23.103052,164.5 19.168008,163.09608 15.997995,160.72537 L15.902315,160.65018 15.791964,160.7357 C12.447579,163.10652 8.3615929,164.5 3.9501491,164.5 2.8887477,164.5 1.8461881,164.41933 0.82826328,164.26379 L0.5,164.20941 0.60665035,164.12764 C5.3825145,160.37439 8.4500561,154.54544 8.4500554,148 8.4500561,147.68489 8.4429457,147.37144 8.4288795,147.0598 L8.4213746,146.92683 8.4247672,146.92545 8.390723,146.47774 C8.3743074,146.1539 8.3660002,145.82791 8.366001,145.5 L8.366001,19.499999 C8.3660002,9.0065894 16.87259,0.49999946 27.366,0.5 z");
            ImageBubbleRightPortrait.Data = Geometry.Parse("M19.500001,0.5 L145.5,0.5 C155.99341,0.5 164.5,9.0065899 164.5,19.500002 L164.5,200.5 C164.5,201.15584 164.46677,201.80391 164.4019,202.44264 L164.39926,202.46349 164.38695,203.00999 C164.38695,209.55544 167.45448,215.3844 172.23035,219.13763 L172.33701,219.21941 172.00873,219.27379 C170.99082,219.42934 169.94826,219.50999 168.88686,219.50999 164.4754,219.50999 160.38942,218.11652 157.04505,215.7457 L156.94289,215.66653 156.868,215.72537 C153.698,218.09608 149.76294,219.5 145.5,219.5 L19.500001,219.5 C9.0065912,219.5 0.50000131,210.99341 0.5,200.5 L0.5,19.500002 C0.50000131,9.0065899 9.0065912,0.5 19.500001,0.5 z");
            ImageBubbleRightLandscape.Data = Geometry.Parse("M19.499998,0.5 L200.5,0.5 C210.99341,0.50000048 219.5,9.0065904 219.5,19.500002 L219.5,145.5 C219.5,145.82791 219.4917,146.1539 219.47528,146.47774 L219.42264,147.16989 219.40395,148 C219.40395,154.54544 222.47148,160.37439 227.24734,164.12764 L227.354,164.20941 227.02574,164.26379 C226.00781,164.41933 224.96526,164.5 223.90385,164.5 219.49242,164.5 215.40643,163.10652 212.06204,160.7357 L211.95772,160.65486 211.868,160.72537 C208.698,163.09609 204.76294,164.5 200.5,164.5 L19.499998,164.5 C9.0065899,164.5 0.5,155.99341 0.5,145.5 L0.5,19.500002 C0.5,9.0065904 9.0065899,0.50000048 19.499998,0.5 z");
            ImageBubbleLeftPortrait.Stretch = Stretch.UniformToFill;
            ImageBubbleLeftLandscape.Stretch = Stretch.UniformToFill;
            ImageBubbleRightPortrait.Stretch = Stretch.UniformToFill;
            ImageBubbleRightLandscape.Stretch = Stretch.UniformToFill;
        }

        public double SetImage(string fileName, Orientation orient)
        {
            double result = 0.0;

            Geometry clipData;

            targetImage.Source = new BitmapImage(new Uri(fileName));
            targetImage.Stretch = System.Windows.Media.Stretch.UniformToFill;

            ImageSource imgSrc = targetImage.Source;
            double w = imgSrc.Width;
            double h = imgSrc.Height;

            if (w > h)
            {
                targetImage.Width = 230.0;
                Width = 230.0;
                Height = 175.0;
                clipData = (orient == Orientation.Outgoing ? ImageBubbleLeftLandscape.Data : ImageBubbleRightLandscape.Data);
                result = 175.0;
            }
            else
            {
                targetImage.Height = 230.0;
                Width = 175.0;
                Height = 230.0;
                clipData = (orient == Orientation.Outgoing ? ImageBubbleLeftPortrait.Data : ImageBubbleRightPortrait.Data);
                result = 230.0;
            }

            targetImage.Clip = clipData;

            return result;
        }
    }
}
