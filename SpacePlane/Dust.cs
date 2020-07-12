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
    public class Dust
    {
        public static readonly double MIN_WIDTH = 5;
        public static readonly double MAX_WIDTH = 10;
        private DispatcherTimer Timer;
        private static Random Rnd = new Random();
        public static readonly long LiveTime = 1000;
        public static readonly double Velocity = 200;
        public static readonly string Path = "Resources/dust_particle.png";
        public Rectangle Rect;
        public double Width;
        public double X;
        public double Y;
        public double Angle;
        public bool Allive = true;

        public Dust(double X, double Y, double Angle)
        {
            this.Angle = Rnd.Next((int)((Angle - Math.PI / 3) * 100), (int)((Angle + Math.PI / 3) * 100)) / 100f;
            Width = Rnd.Next((int)MIN_WIDTH, (int)MAX_WIDTH);
            this.X = X;
            this.Y = Y;
            Rect = new Rectangle();
            Rect.RenderTransformOrigin = new Point(0.5, 0.5);
            Rect.Width = Width;
            Rect.Height = Width;
            ImageBrush Skin = new ImageBrush();
            Skin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + Path));
            Rect.Fill = Skin;
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(LiveTime);
            Timer.Tick += new EventHandler(Delete);
        }

        public void Update(double deltaT)
        {
            if (!Timer.IsEnabled)
                Timer.Start();
            double XVelocity = -Velocity * Math.Sin(Angle);
            double YVelocity = Velocity * Math.Cos(Angle);
            X += XVelocity * deltaT;
            Y -= YVelocity * deltaT;
            if(Y <= 0)
            {
                Delete(Timer, new EventArgs());
            }
        }

        private void Delete(object sender, EventArgs e)
        {
            Rect.Fill = null;
            Allive = false;
        }
    }
}
