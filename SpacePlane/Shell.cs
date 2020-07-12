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
    public class Shell : UIElement
    {
        public static readonly int WIDTH = 10;
        public static readonly int HEIGHT = 30;
        public int Damage;
        public double XVelocity;
        public double YVelocity;
        public double Angle;
        public double X;
        public double Y;
        //public string Path;
        public Rectangle Rect;

        public Shell(string Path)//int Damage, double Velocity, int Width, int Height, double Angle)
        {
            Rect = new Rectangle();
            Rect.RenderTransformOrigin = new Point(0.5, 0.5);
            Rect.Width = WIDTH;
            Rect.Height = HEIGHT;
            ImageBrush Skin = new ImageBrush();
            Skin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + Path));
            Rect.Fill = Skin;
        }

        public void Update(double deltaT)
        {
            X += XVelocity * deltaT;
            Y -= YVelocity * deltaT;
        }
    }
}
