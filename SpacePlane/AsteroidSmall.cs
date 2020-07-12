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
    public class AsteroidSmall : Asteroid
    {
        private static string AsteroidSmallPath = "Resources/rock1.png";
        public static int AsteroidSmallScore = 10;
        public AsteroidSmall(int W, int Level) : base(W, Level, AsteroidSmallPath)
        {
            Width = 30;
            Height = 30;
            Health = 150;
            Mass = 100;
            Score = AsteroidSmallScore;
            Rect.Width = Width;
            Rect.Height = Height;
        }
    }
}
