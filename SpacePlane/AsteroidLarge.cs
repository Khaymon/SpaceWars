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
    class AsteroidLarge : Asteroid
    {
        private static string AsteroidLargePath = "Resources/rock4.png";
        public static int AsteroidLargeScore = 50;
        public AsteroidLarge(int W, int Level) : base(W, Level, AsteroidLargePath)
        {
            Width = 70;
            Height = 70;
            Health = 500;
            Mass = 500;
            Score = AsteroidLargeScore;
            Rect.Width = Width;
            Rect.Height = Height;
        }
    }
}
