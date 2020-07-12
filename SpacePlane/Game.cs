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
    public class Game
    {
        private const int MAX_LEVEL = 3;
        private double[] TicksForLevelUp = { 500, 500, 500, 500 };
        private double[] TInterval = { 700, 400, 200, 100 };
        private DispatcherTimer Timer;
        private int Ticks;
        public bool AsteroidGenerated = false;
        public bool StrikeGenerated = false;
        public bool IsPlaying = true;
        public List<Asteroid> Asteroids;
        public List<Strike> Strikes;
        public List<AsteroidExplode> AsteroidExplodes;
        public Player Person;
        public Explosion Explos;
        public int Height;
        public int Width;
        public int Level = 0;
        public int Score;

        public Game(int x, int y, object Weapon)
        {
            Width = x;
            Height = y;
            Level = 0;
            Person = new Player(x, y, Weapon);
            Asteroids = new List<Asteroid>();
            Strikes = new List<Strike>();
            AsteroidExplodes = new List<AsteroidExplode>();
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(GenerateAsteroid);
            Timer.Interval = TimeSpan.FromMilliseconds(TInterval[Level]);
        }

        public bool Update(double deltaT, Canvas GameField)
        {
            if (IsPlaying)
            {
                UpdatePerson(deltaT, GameField);
                UpdateAsteroids(deltaT, GameField);
                UpdateShells(deltaT, GameField);
                CheckShellStrike(deltaT, GameField);
                UpdateStrikes(deltaT, GameField);
                if (CheckCollision(deltaT, GameField))
                {
                    Timer.Stop();
                    Explos = new Explosion(Person.X, Person.Y);
                    IsPlaying = false;
                    return true;
                }
            }
            else
            {
                
            }
            return false;
        }

        public void UpdatePerson(double deltaT, Canvas GameField)
        {
            Person.Update(deltaT);
            if (Person.IsFire && Person.PlayerGun.Reloaded)
            {
                Person.PlayerGun.Timer.Start();
                Person.Fire(Person.PlayerGun, new EventArgs());
            }
        }

        public void UpdateAsteroids(double deltaT, Canvas GameField)
        {
            for (int i = 0; i < Asteroids.Count; ++i)
            {
                Asteroid A = Asteroids[i];
                A.Update(deltaT);
                if (A.Y > Height)
                {
                    GameField.Children.Remove(A.Rect);
                    Asteroids.Remove(A);
                }
            }
        }

        public void UpdateShells(double deltaT, Canvas GameField)
        {
            for (int i = 0; i < Person.PlayerGun.Shells.Count; ++i)
            {
                Shell S = Person.PlayerGun.Shells[i];
                S.Update(deltaT);
                if (S.Y < 0 || S.X > Width || S.X < -50)
                {
                    GameField.Children.Remove(S.Rect);
                    Person.PlayerGun.Shells.Remove(S);
                }
            }
        }

        public void UpdateStrikes(double deltaT, Canvas GameField)
        {
            for(int i = 0; i < Strikes.Count; ++i)
            {
                Strike Str = Strikes[i];
                for(int j = 0; j < Str.Dusts.Count; ++j)
                {
                    Dust D = Str.Dusts[j];
                    D.Update(deltaT);
                    if(!D.Allive)
                    {
                        Str.Dusts.Remove(D);
                        GameField.Children.Remove(D.Rect);
                    }
                }
                if (Str.Dusts.Count == 0)
                    Strikes.Remove(Str);
            }
        }

        public void CheckShellStrike(double deltaT, Canvas GameField)
        {
            for (int i = 0; i < Person.PlayerGun.Shells.Count; ++i)
            {
                Shell S = Person.PlayerGun.Shells[i];
                for (int j = 0; j < Asteroids.Count; ++j)
                {
                    Asteroid A = Asteroids[j];
                    double DistSquared = Math.Pow(S.X - A.X, 2) + Math.Pow(S.Y - A.Y, 2);
                    double AsterDiamSquared = Math.Pow(A.Width / 2, 2) + Math.Pow(A.Height / 2, 2);
                    if (Math.Sqrt(DistSquared) < Math.Sqrt(AsterDiamSquared))
                    {
                        Strike Str = new Strike(A.X, A.Y, S.Angle);
                        Strikes.Add(Str);
                        StrikeGenerated = true;
                        A.Health -= S.Damage;
                        GameField.Children.Remove(S.Rect);
                        Person.PlayerGun.Shells.RemoveAt(i);
                        break;
                    }
                    if (A.Health <= 0)
                    {
                        AsteroidExplode AE = new AsteroidExplode(A.X, A.Y, A.Width + 20);
                        AsteroidExplodes.Add(AE);
                        GameField.Children.Add(AE.Rect);
                        GameField.Children.Remove(A.Rect);
                        Asteroids.RemoveAt(j);
                        Score += A.Score;
                        break;
                    }
                }
            }
        }

        public bool CheckCollision(double deltaT, Canvas GameField)
        {
            for(int i = 0; i < Asteroids.Count; ++i)
            {
                Asteroid A = Asteroids[i];
                double DistSquared = Math.Pow(A.X - Person.X, 2) + Math.Pow(A.Y - Person.Y, 2);
                double AsteroidRadiusSquared = Math.Pow(A.Height / 2, 2) + Math.Pow(A.Width / 2, 2);
                if(Math.Sqrt(DistSquared) < Math.Sqrt(AsteroidRadiusSquared))
                {
                    GameField.Children.Remove(Person.Rect);
                    return true;
                }
            }
            return false;
        }
        public void LevelUp(double t)
        {
            if (Level >= MAX_LEVEL)
                return;
            if (!Timer.IsEnabled)
                Timer.Start();
            Ticks++;
            if(TicksForLevelUp[Level] <= Ticks)
            {
                Ticks = 0;
                Timer.Interval = TimeSpan.FromMilliseconds(TInterval[Level]);
                Level++;
            }
        }

        public void GenerateAsteroid(object sender, EventArgs e)
        {
            Random rand = new Random();
            int type = rand.Next(1, 4);
            Asteroid NewAsteroid = null;
            if(type == 1)
                NewAsteroid = new AsteroidSmall(Width, Level);
            else if(type == 2)
                NewAsteroid = new AsteroidMedium(Width, Level);
            else if(type == 3)
                NewAsteroid = new AsteroidLarge(Width, Level);

            Asteroids.Add(NewAsteroid);
            AsteroidGenerated = true;
        }
    }
}
