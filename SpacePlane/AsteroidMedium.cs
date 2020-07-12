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
    class AsteroidMedium : Asteroid
    {
        private static string AsteroidMediumPath = "Resources/rock3.png";
        public static int AsteroidMediumScore = 30;
        public AsteroidMedium(int W, int Level) : base(W, Level, AsteroidMediumPath)
        {
            Width = 50;
            Height = 50;
            Health = 300;
            Mass = 300;
            Score = AsteroidMediumScore;
            Rect.Width = Width;
            Rect.Height = Height;
        }
    }
}
