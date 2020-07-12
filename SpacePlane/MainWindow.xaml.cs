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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game G;
        private DispatcherTimer timer = new DispatcherTimer();//таймер для работы движка

        public MainWindow()
        {
            InitializeComponent();
            ImageBrush BackGround = new ImageBrush();
            BackGround.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/background.png"));
            GameArea.Background = BackGround;
            this.ResizeMode = ResizeMode.NoResize;
            timer.Tick += new EventHandler(Engine);//добавление делегата для работы движка
            timer.Interval = TimeSpan.FromMilliseconds(20);//частота обновления экрана
        }

        private void InitPlane()
        {
            GameArea.Children.Add(G.Person.Rect);
        }

        private void InitAsteroid()
        {
            int n = G.Asteroids.Count;
            GameArea.Children.Add(G.Asteroids[n - 1].Rect);
        }

        private void InitShell()
        {
            int n = G.Person.PlayerGun.Shells.Count;
            if (n == 0)
                return;
            Rectangle AddRect = G.Person.PlayerGun.Shells[n - 1].Rect;
            if(!GameArea.Children.Contains(AddRect))
                GameArea.Children.Add(G.Person.PlayerGun.Shells[n - 1].Rect);
        }

        private void InitExplosion()
        {
            GameArea.Children.Add(G.Explos.Rect);
        }

        private void InitStrike()
        {
            int n = G.Strikes.Count;
            if (n == 0)
                return;
            Strike S = G.Strikes[n - 1];
            for(int i = 0; i < S.Dusts.Count; ++i)
            {
                Dust D = S.Dusts[i];
                GameArea.Children.Add(D.Rect);
            }
        }

        private void DrawPlane()
        {
            Canvas.SetLeft(G.Person.Rect, G.Person.X);
            Canvas.SetTop(G.Person.Rect, G.Person.Y);
        }

        private void DrawAsteroids()
        {
            if(G.AsteroidGenerated)
            {
                G.AsteroidGenerated = false;
                InitAsteroid();
            }
            foreach(Asteroid A in G.Asteroids)
            {
                Canvas.SetLeft(A.Rect, A.X);
                Canvas.SetTop(A.Rect, A.Y);
            }
        }

        private void DrawShells()
        {
            if(G.Person.ShellGenerated)
            {
                G.Person.ShellGenerated = false;
                InitShell();
            }
            foreach(Shell S in G.Person.PlayerGun.Shells)
            {
                Canvas.SetLeft(S.Rect, S.X);
                Canvas.SetTop(S.Rect, S.Y);
            }
        }

        private void DrawExplosion()
        {
            Canvas.SetLeft(G.Explos.Rect, G.Explos.X);
            Canvas.SetTop(G.Explos.Rect, G.Explos.Y);
        }

        private void DrawStrikes()
        {
            if(G.StrikeGenerated)
            {
                G.StrikeGenerated = false;
                InitStrike();
                DrawStrikes();
            }
            else
            {
                for(int i = 0; i < G.Strikes.Count; ++i)
                {
                    Strike S = G.Strikes[i];
                    for(int j = 0; j < S.Dusts.Count; ++j)
                    {
                        Dust D = S.Dusts[j];
                        if (GameArea.Children.Contains(D.Rect))
                        {
                            Canvas.SetLeft(D.Rect, D.X);
                            Canvas.SetTop(D.Rect, D.Y);
                        }
                    }
                }
            }
        }

        private void DrawAsteroidExplodes()
        {
            for(int i = 0; i < G.AsteroidExplodes.Count; ++i)
            {
                AsteroidExplode AE = G.AsteroidExplodes[i];
                Canvas.SetLeft(AE.Rect, AE.X);
                Canvas.SetTop(AE.Rect, AE.Y);
                if (!AE.IsAlive)
                    GameArea.Children.Remove(AE.Rect);
            }
        }

        private void Engine(object sender, EventArgs e)
        {
            if (G.IsPlaying) {
                Score.Content = G.Score;
                DrawPlane();
                DrawAsteroids();
                DrawShells();
                DrawStrikes();
                DrawAsteroidExplodes();
                G.LevelUp(timer.Interval.TotalSeconds);
                if(G.Update((double)timer.Interval.Milliseconds / 1000f, GameArea))
                    InitExplosion();
            }
            else {
                DrawExplosion();
            }
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            G = new Game((int)this.Width, (int)this.Height, Weapon.Content);
            InitPlane();
            timer.Start();
        }

        private void GameArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (G != null)
            {
                if (e.Key == Key.W)
                {
                    G.Person.Accelerating = true;
                    G.Person.Stopping = false;
                }
                if (e.Key == Key.A)
                {
                    G.Person.TurningCounterClockwise = true;
                    G.Person.TurningClockwise = false;
                }
                if (e.Key == Key.D)
                {
                    G.Person.TurningCounterClockwise = false;
                    G.Person.TurningClockwise = true;
                }
                if (e.Key == Key.Space)
                {
                    G.Person.IsFire = true;
                }
            }
        }

        private void GameArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (G != null)
            {
                if (e.Key == Key.W)
                {
                    G.Person.Accelerating = false;
                    G.Person.Stopping = true;
                }
                if (e.Key == Key.A)
                {
                    G.Person.TurningCounterClockwise = false;
                }
                if (e.Key == Key.D)
                {
                    G.Person.TurningClockwise = false;
                }
                if (e.Key == Key.Space)
                {
                    G.Person.IsFire = false;
                }
            }
        }

        private void Laser_Click(object sender, RoutedEventArgs e)
        {
            Weapon.Content = "Laser";
        }

        private void Rockets_Click(object sender, RoutedEventArgs e)
        {
            Weapon.Content = "Rockets";
        }
    }
}
