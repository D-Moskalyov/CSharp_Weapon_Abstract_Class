using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    abstract class Weapon
    {
        public Weapon(int mx_dmg, int mx_dist)
        {
            MaxDamage = mx_dmg;
            MaxDistance = mx_dist;
        }
        int maxDamage;
        public int MaxDamage
        {
            get { return maxDamage; }
            set { maxDamage = value; }
        }
        int maxDistance;
        public int MaxDistance
        {
          get { return maxDistance; }
          set { maxDistance = value; }
        }
        
        public virtual void Shot(int distance)
        {
            double sht = (double)(MaxDistance - distance) / MaxDistance * MaxDamage;
            if (sht > 0)
            {
                Console.WriteLine("Урон: " + sht);
                return;
            }
            return;
        }
        public virtual void Reload(int count)
        {
            Console.WriteLine("Не подлежит заряду");
        }
    }

    abstract class Grenade : Weapon
    {
        static int maxGrenade = 3;

        public static int MaxGrenade
        {
            get { return Grenade.maxGrenade; }
        }
        static int countGrenade = 0;

        public static int CountGrenade
        {
            get { return countGrenade; }
            set { countGrenade = value; }
        }
        public Grenade(int mx_dam, int mx_dist)
            : base(mx_dam, mx_dist)
        {
            CountGrenade++;
        }
        public override void Shot(int distance)
        {
            CountGrenade--;
            base.Shot(distance);
        }
    }
    abstract class FirearmWeapon : Weapon
    {
        public FirearmWeapon(int mx_blt, int mx_dmg, int mx_dist)
            : base(mx_dmg, mx_dist)
        {
            MaxBullet = mx_blt;
            Bullet = 0;
        }
        int maxBullet;
        int bullet;

        public int Bullet
        {
          get { return bullet; }
          set { bullet = value; }
        }
        public int MaxBullet
        {
            get { return maxBullet; }
            set { maxBullet = value; }
        }

        public override void Shot(int distance)
        {
            Bullet--;
            base.Shot(distance);
        }

    }
    class Knife : Weapon
    {
        string title;
        private static Knife instance = null;
        private Knife(string name)
            : base(35, 1)
        {
            title = name;
        }

        public static Knife Instance(string str)
        {
            if (instance == null)
            {
                instance = new Knife(str);
            }
            else
            {
                Console.WriteLine("Нож уже в наличии");
                instance = null;
            }
            return instance;
        }

        public override void Shot(int distance)
        {
            Console.Write("Чик-чик. ");
            base.Shot(distance);
        }
    }
       
    class HandGun : FirearmWeapon
    {
        string title;
        private static HandGun instance;
        private HandGun(string name, int mx_blt, int mx_dam, int mx_dist)
            : base(mx_blt, mx_dam, mx_dist)
        {
            title = name;
        }

        public static HandGun Instance(string str, int max_blt, int max_dam, int max_dist)
        {
            if (instance == null)
            {
                instance = new HandGun(str, max_blt, max_dam, max_dist);
            }
            else
            {
                Console.WriteLine("Пистоль уже в наличии");
                instance = null;
            }
            return instance;
        }

        public override void Shot(int distance)
        {
            if (base.Bullet > 0)
            {
                Console.Write("Выстрел из пистолета. ");
                base.Shot(distance);
            }
        }
        public override void Reload(int count)
        {
            Bullet += count;
            if (Bullet > MaxBullet)
            {
                Bullet = MaxBullet;
            }
            Console.WriteLine("Пистоль заряжен");
        }
    }
    class Assault : FirearmWeapon
    {
        string title;
        private static Assault instance;
        private Assault(string name, int mx_blt, int mx_dam, int mx_dist)
            : base(mx_blt, mx_dam, mx_dist)
        {
            title = name;
        }

        public static Assault Instance(string str, int max_blt, int max_dam, int max_dist)
        {
            if (instance == null)
            {
                instance = new Assault(str, max_blt, max_dam, max_dist);
            }
            else
            {
                Console.WriteLine("Автомат уже в наличии");
                instance = null;
            }
            return instance;
        }

        public override void Shot(int distance)
        {
            if (base.Bullet > 0)
            {
                Console.Write("Вычтрел из автомата. ");
                base.Shot(distance);
            }
        }
        public override void Reload(int count)
        {
            Bullet += count;
            if (Bullet > MaxBullet)
            {
                Bullet = MaxBullet;
            }
            Console.WriteLine("Автомат заряжен");
        }
    }

    class Grenade_1 : Grenade
    {
        string title;
        private static Grenade_1 instance;

        private Grenade_1(string name, int mx_dam, int mx_dist)
            : base(mx_dam, mx_dist)
        {
            title = name;
        }

        public static Grenade_1 Instance(string name, int max_dam, int max_dist)
        {
            if (Grenade.CountGrenade < Grenade.MaxGrenade)
            {
                instance = new Grenade_1(name, max_dam, max_dist);
            }
            else
            {
                Console.WriteLine("Гранат уже три");
                instance = null;
            }
            return instance;
        }
        public override void Shot(int distance)
        {
            if (Grenade.CountGrenade > 0)
            {
                Console.Write("Ба-бах. ");
                base.Shot(distance);
            }
        }
    }

    class Grenade_2 : Grenade
    {
        string title;
        private static Grenade_2 instance = null;

        private Grenade_2(string name, int mx_dam, int mx_dist)
            : base(mx_dam, mx_dist)
        {
            title = name;
        }

        public static Grenade_2 Instance(string name, int max_dam, int max_dist)
        {
            if (Grenade.CountGrenade < Grenade.MaxGrenade)
            {
                instance = new Grenade_2(name, max_dam, max_dist);
            }
            else
            {
                Console.WriteLine("Гранат уже три");
                instance = null;
            }
            return instance;
        }
        public override void Shot(int distance)
        {
            if (Grenade.CountGrenade > 0)
            {
                Console.Write("Пшш. ");
                base.Shot(distance);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Weapon[] Tools = new Weapon[10];

            Tools[0] = Knife.Instance("knife");
            Tools[1] = HandGun.Instance("Пистоль", 7, 70, 100);
            Tools[2] = Assault.Instance("Автомат", 30, 100, 300);
            Tools[3] = Knife.Instance("knife");
            Tools[4] = HandGun.Instance("Пистоль", 10, 70, 100);
            Tools[5] = Assault.Instance("Автомат", 30, 100, 300);
            Tools[6] = Grenade_1.Instance("Граната 1", 80, 20);
            Tools[7] = Grenade_2.Instance("Граната 2", 40, 20);
            Tools[8] = Grenade_1.Instance("Граната 1", 80, 20);
            Tools[9] = Grenade_2.Instance("Граната 2", 40, 20);

            for (int i = 0; i < 10; i++)
            {
                if(Tools[i]!=null)
                    Tools[i].Reload(10);
            }

            for (int i = 0; i < 10; i++)
            {
                if (Tools[i] != null)
                    Tools[i].Shot(0);
            }
            Console.ReadLine();
            //Tools[7].Reload(10);
            //Tools[7].Shot(5);
        }
    }
}