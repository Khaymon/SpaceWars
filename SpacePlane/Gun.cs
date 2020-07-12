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
    public class Gun
    {
        public List<Shell> Shells;
        public int Type;
        public long Speed;
        public bool Reloaded = true;
        public DispatcherTimer Timer;

        public Gun(int Type)
        {
            Timer = new DispatcherTimer();
            Shells = new List<Shell>();
            this.Type = Type;
        }

        public bool Fire(double X, double Y, double Angle)
        {
            Shell NewShell = null;
            if (Type == 1)
                NewShell = new ShellLaser(X, Y, Angle);
            else if (Type == 2)
                NewShell = new ShellRocket(X, Y, Angle);
            if (NewShell != null)
            {
                Shells.Add(NewShell);
                return true;
            }
            return false;
        }
    }
}
