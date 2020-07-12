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
    public class Explosion
    {
        private DispatcherTimer Timer;
        private int SPEED = 100;
        private int State;
        public double X;
        public double Y;
        public Rectangle Rect;

        public Explosion(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(SPEED);
            Timer.Tick += new EventHandler(Update);
            Rect = new Rectangle();
            Rect.RenderTransformOrigin = new Point(0.5, 0.5);
            Rect.Width = Player.WIDTH;
            Rect.Height = Player.WIDTH;
            Timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            ImageBrush Skin = new ImageBrush();
            string Path = null;
            if (State == 0)
            {
                Path = "Resources/Explosion1.png";
            }
            else if (State == 1)
            {
                Path = "Resources/Explosion2.png";
            }
            else if (State == 2)
            {
                Path = "Resources/Explosion3.png";
            }
            else if (State == 3)
            {
                Path = "Resources/Explosion4.png";
            }
            else
            {
                Rect.Fill = null;
                return;
            }
            Skin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + Path));
            Rect.Fill = Skin;
            State++;
        }
    }
}
