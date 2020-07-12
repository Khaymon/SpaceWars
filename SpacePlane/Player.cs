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
    public class Player : UIElement
    {
        private DispatcherTimer Timer;
        private const double MAX_VELOCITY = 800;
        private const double MIN_VELOCITY = 100;
        private const double MAX_ANGLE = Math.PI / 4;
        private const double MIN_ANGLE = -Math.PI / 4;
        private const double ACCELERATION = 600;
        private const double STOP = 1200;
        public static readonly int WIDTH = 60;
        public static readonly int HEIGHT = 40;
        public int Ticks;
        public double ANGULAR_VELOCITY = 3;
        public double AreaWidth;
        public double X;
        public double Y;
        public double Velocity = 20;
        public double Angle = 0;
        public bool Accelerating = false;
        public bool Stopping = false;
        public bool TurningClockwise = false;
        public bool TurningCounterClockwise = false;
        public bool IsFire = false;
        public bool ShellGenerated = false;
        public static string Path = "Resources/player_ship.png";
        public Gun PlayerGun;
        public Rectangle Rect;
        

        public Player(int x, int y, object Weapon)
        {
            Rect = new Rectangle();
            Rect.Width = 30;
            Rect.Height = 50;
            Rect.RenderTransformOrigin = new Point(0.5, 0.5);
            ImageBrush Skin = new ImageBrush();
            Skin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + Path));
            Rect.Fill = Skin;
            this.X = x / 2;
            this.Y = y / 1.5;
            AreaWidth = x;
            if ((Weapon as string) == "Laser")
                PlayerGun = new GunLaser();
            else
                PlayerGun = new GunRocket();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(PlayerGun.Speed);
            PlayerGun.Timer.Tick += new EventHandler(Fire);
        }

        public void Update(double deltaT)
        {
            if (!Timer.IsEnabled)
                Timer.Start();
            double acc_prod = 0;
            double turn_prod = 0;
            if (Accelerating)
                acc_prod = 1;
            if (Stopping)
                acc_prod = -1;
            if (TurningClockwise)
                turn_prod = -1;
            if (TurningCounterClockwise)
                 turn_prod = 1;
            Velocity += ACCELERATION * deltaT * acc_prod;
            Velocity = Math.Max(Velocity, MIN_VELOCITY);
            Velocity = Math.Min(Velocity, MAX_VELOCITY);
            Angle += ANGULAR_VELOCITY * deltaT * turn_prod;
            Angle = Math.Max(Angle, MIN_ANGLE);
            Angle = Math.Min(Angle, MAX_ANGLE);
            double vx = -Velocity * Math.Sin(Angle);
            X += vx * deltaT;
            X = Math.Max(X,0);
            X = Math.Min(X, AreaWidth - 55);
            Rect.RenderTransform = new RotateTransform(-Angle * 180 / Math.PI);
        }

        public void Fire(object sender, EventArgs e)
        {
            if (IsFire)
            {
                PlayerGun.Reloaded = false;
                double ShellX = X - (Player.WIDTH / 2 + WIDTH / 2) * Math.Sin(Angle) + 10;
                double ShellY = Y - (Player.HEIGHT / 2 + HEIGHT / 2) * Math.Cos(Angle) + 15;
                ShellGenerated = PlayerGun.Fire(ShellX, ShellY, Angle);
            }
            else
            {
                PlayerGun.Timer.Stop();
                PlayerGun.Reloaded = true;
            }
        }
    }
}
