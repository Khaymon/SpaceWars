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
    class GunLaser : Gun
    {
        static long GunLaserSpeed = 200;
        public GunLaser() : base(1)
        {
            this.Speed = GunLaserSpeed;
            Timer.Interval = TimeSpan.FromMilliseconds(Speed);
        }
    }
}
