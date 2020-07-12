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
    public class Strike
    {
        public List<Dust> Dusts;
        public int Count;
        public long LiveTime = 2000;

        public Strike(double X, double Y, double Angle)
        {
            Dusts = new List<Dust>();
            Random rand = new Random();
            Count = rand.Next(3, 6);
            for(int i = 0; i < Count; ++i)
            {
                Dust D = new Dust(X, Y, Angle);
                Dusts.Add(D);
            }
        }
    }
}
