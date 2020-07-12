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
    class ShellRocket : Shell
    {
        public static double RocketVelocity = 100;
        public ShellRocket(double X, double Y, double Angle) : base("Resources/rocket.png")
        {
            this.X = X;
            this.Y = Y;
            this.Angle = Angle;
            Damage = 250;
            XVelocity = -RocketVelocity * Math.Sin(Angle);
            YVelocity = RocketVelocity * Math.Cos(Angle);
            Rect.RenderTransform = new RotateTransform(-Angle * 180 / Math.PI);
        }
    }
}
