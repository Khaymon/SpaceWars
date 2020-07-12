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
    public class Asteroid : UIElement
    {
        private double[] LEVEL_VELOCITY = { 120, 180, 220, 300 };
        private static Random Rnd = new Random();
        const double MAX_VELOCITY = 100;
        const double MIN_VELOCITY = 50;
        const double MAX_ANGULAR_VELOCITY = 5;
        const double MIN_ANGLUAR_VELOCITY = -5;
        double AnglularVelocity;
        double Velocity = 80;
        public int Health;
        protected int Mass;
        public int Score;
        public int Height;
        public int Width;
        public double X;
        public double Y;
        public double Angle;
        public string Path;
        public Rectangle Rect;
        public int a = 13;
        public int c = 17;
        public static int seed = 53;

        // Случайная генерация астероида
        public Asteroid(int W, int Level, string Path)
        {
            Rect = new Rectangle();
            ImageBrush Skin = new ImageBrush();
            Skin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + Path));
            Rect.Fill = Skin;
            Rect.RenderTransformOrigin = new Point(0.5, 0.5);

            seed = (a * seed + c) % (W - 55);
            X = seed;
            Y = 0;
            Angle = 0;
            Velocity = LEVEL_VELOCITY[Level];
            AnglularVelocity = Rnd.Next((int)MIN_ANGLUAR_VELOCITY, (int)MAX_ANGULAR_VELOCITY);
            
        }

        public void Update(double deltaT)
        {
            Y += Velocity * deltaT;
            Angle += AnglularVelocity * deltaT;
            Rect.RenderTransform = new RotateTransform(Angle * 180 / Math.PI);
        }
    }
}
