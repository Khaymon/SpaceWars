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
    class GunRocket : Gun
    {
        static long GunRocketSpeed = 700;
        public GunRocket() : base(2)
        {
            this.Speed = GunRocketSpeed;
            Timer.Interval = TimeSpan.FromMilliseconds(Speed);
        }
    }
}
