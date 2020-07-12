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
    public class ShellLaser : Shell
    {
        public static double LaserVelocity = 500;
        public ShellLaser(double X, double Y, double Angle) : base("Resources/player_laser.png")
        {
            this.X = X;
            this.Y = Y;
            this.Angle = Angle;
            Damage = 50;
            XVelocity = -LaserVelocity * Math.Sin(Angle);
            YVelocity = LaserVelocity * Math.Cos(Angle);
            Rect.RenderTransform = new RotateTransform(-Angle * 180 / Math.PI);
        }
    }
}
