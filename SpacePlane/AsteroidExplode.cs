using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpacePlane
{
    public class AsteroidExplode
    {
        public static readonly string Path = "Resources/asteroid_explosion.png";
        public static readonly long LiveTime = 200;
        public double X;
        public double Y;
        public DispatcherTimer Timer;
        public bool IsAlive = true;
        public Rectangle Rect;

        public AsteroidExplode(double X, double Y, double W)
        {
            this.X = X;
            this.Y = Y;
            Rect = new Rectangle();
            Rect.RenderTransformOrigin = new Point(0.5, 0.5);
            Rect.Width = W;
            Rect.Height = W;
            ImageBrush Skin = new ImageBrush();
            Skin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + Path));
            Rect.Fill = Skin;
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(LiveTime);
            Timer.Tick += new EventHandler(Delete);
            Timer.Start();
        }

        private void Delete(object sender, EventArgs e)
        {
            Rect.Fill = null;
            IsAlive = false;
        }
    }
}
