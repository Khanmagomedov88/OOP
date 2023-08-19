using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            City[] city =
            {
                new City ("Махачкала", TimeOfDay.Evening, 47500),
                new City ("Кизляр", TimeOfDay.Morning, 11000),
                new City("Дербент", TimeOfDay.Night, 32020),
                new City("Мамедкала", TimeOfDay.Night, 17100),
                new Village("Сабнова", TimeOfDay.Afternoon, 8000, 20)
            };

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("0 - Добавить новый населенный пункт");
                Console.ForegroundColor = ConsoleColor.White;

                for (int i = 0; i < city.Length; i++)
                    Console.WriteLine($"{i + 1} - {city[i].nameCity}");

                Console.Write("\nВыберите номер населенного пункта: ");
                int numberCity = Convert.ToInt32(Console.ReadLine());

                // Если пользователь выбрал "Добавить населенный пункт"
                if (numberCity == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("--Добавление населенного пункта--\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("1. Добавить город \n2. Добавить село \n\nВыберите номер действия: ");

                    int numAction = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    if (numAction == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("--Добавление населенного пункта--\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Введите название города: ");
                        string userInputNameCity = Console.ReadLine();
                        Console.Write("Введите время суток города: ");
                        string userInputTimeOfCity = Console.ReadLine();
                        Console.Write("Введите бюджет города: ");
                        int userInputBalanceCity = Convert.ToInt32(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nГород успешно добавлен! ");
                        Console.ForegroundColor = ConsoleColor.White;
                        InsertCity(ref city, userInputNameCity, GetTimeOfDay(userInputTimeOfCity), userInputBalanceCity);

                        Console.ReadLine();
                    }
                    else if (numAction == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("--Добавление населенного пункта--\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Введите название села: ");
                        string userInputNameCity = Console.ReadLine();
                        Console.Write("Введите время суток села: ");
                        string userInputTimeOfCity = Console.ReadLine();
                        Console.Write("Введите бюджет села: ");
                        int userInputBalanceCity = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Введите возраст села: ");
                        int userInputAgeCity = Convert.ToInt32(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nСело успешно добавлено! ");
                        Console.ForegroundColor = ConsoleColor.White;
                        InsertVillage(ref city, userInputNameCity, GetTimeOfDay(userInputTimeOfCity), userInputBalanceCity, userInputAgeCity);

                        Console.ReadLine();
                    }
                }
                else //Если пользователь выбрал номер какого-либо города
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    city[numberCity - 1].Greeting();
                    Console.ForegroundColor = ConsoleColor.White;

                    int userChoice = city[numberCity - 1].ShowFunction();

                    if (userChoice == 1)
                        city[numberCity - 1].ShowStatus();
                    else if (userChoice == 2)
                    {
                        city[numberCity - 1].ShowBalance();
                        Console.Write("Какую сумму вы хотите списать: ");
                        city[numberCity - 1].TakingInBalance(Convert.ToInt32(Console.ReadLine()));

                    }
                    Console.ReadLine();
                }
            }

            TimeOfDay GetTimeOfDay(string timeOfDay)
            {
                switch (timeOfDay)
                {
                    case "Ночь":
                        return TimeOfDay.Night;
                    case "Вечер":
                        return TimeOfDay.Evening;
                    case "День":
                        return TimeOfDay.Afternoon;
                    case "Утро":
                        return TimeOfDay.Morning;
                    default: return TimeOfDay.Unknown;
                }
            }

            // Метод добавления города
            void InsertCity(ref City[] city, string NameCity, TimeOfDay Time, int CityBalance)
            {
                int cLenght = city.Length;
                City[] newCity = new City[cLenght + 1];

                for (int i = 0; i < newCity.Length; i++)
                {
                    if (i == newCity.Length - 1)
                        newCity[i] = new City(NameCity, Time, CityBalance);
                    else
                        newCity[i] = city[i];

                }

                city = newCity;
            }

            // Метод добавления села
            void InsertVillage(ref City[] city, string NameCity, TimeOfDay Time, int CityBalance, int Age)
            {
                int cLenght = city.Length;

                City[] newCity = new City[cLenght + 1];

                for (int i = 0; i < newCity.Length; i++)
                {
                    if (i == newCity.Length - 1)
                        newCity[i] = new Village(NameCity, Time, CityBalance, Age);
                    else
                        newCity[i] = city[i];

                }
                city = newCity;
            }
        }
    }

    enum TimeOfDay
    {
        Morning,
        Afternoon,
        Evening,
        Night,
        Unknown
    }
    interface ILocality
    {
        int ShowFunction();
        void ShowStatus();
        void ShowBalance();
        void Greeting();
    }

    class City : ILocality
    {
        public static int Identifications;
        public int Identification;
        public string nameCity { get; private set; }

        protected TimeOfDay _timeOfDay;
        public int cityBalance { get; protected set; }

        public City(string NameCity, TimeOfDay TimeOfDay, int CityBalance)
        {
            Identification = ++Identifications;
            nameCity = NameCity;
            _timeOfDay = TimeOfDay;
            cityBalance = CityBalance;
        }

        public virtual void Greeting()
        {
            Console.WriteLine($"Приветствуем в городе {nameCity}!");
        }

        public virtual int ShowFunction()
        {
            Console.Write("Доступные действия: \n\n1. Посмотреть статистику города \n2. Потратить деньги с баланса города \n\nВыберите действие: ");

            int numberFunction = Convert.ToInt32(Console.ReadLine());
            return numberFunction;
        }

        public virtual void ShowBalance()
        {
            Console.WriteLine($"Баланс города {nameCity}: {cityBalance}$");
        }

        public virtual void ShowStatus()
        {
            string isElectricity = AvailabilityOfElectricity();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n--Статистика города {nameCity}--");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Название города: {nameCity} \nID населенного пункта: {Identification}\nВремя суток в городе: {_timeOfDay} \nБаланс города: {cityBalance}$ " +
                $"\nНаличие электричества: {isElectricity}\n\n");
        }

        public string AvailabilityOfElectricity() // Метод, определяющий есть ли электричество в городе или селе
        {
            Random random = new Random();
            int a = random.Next(0, 100);

            if (a > 50)
                return "Отсутствует";
            else
                return "Присутствует";
        }

        public virtual void TakingInBalance(int Summa)
        {
            if (cityBalance >= Summa)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new String('-', 23));
                Console.WriteLine("Деньги успешно списаны с баланса города!");
                Console.ForegroundColor = ConsoleColor.White;
                cityBalance -= Summa;
                Console.WriteLine($"Баланс города {nameCity}: {cityBalance}$");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Недостаточно средств!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    class Village : City, ILocality
    {
        public int _age;
        public Village(string NameVillage, TimeOfDay TimeOfDay, int VillageBalance, int Age) : base(NameVillage, TimeOfDay, VillageBalance)
        {
            _age = Age;
        }

        public override void Greeting()
        {
            Console.WriteLine($"Приветствуем в селе {nameCity}!");
        }

        public override void ShowBalance()
        {
            Console.WriteLine($"Баланс села {nameCity}: {cityBalance}$");
        }

        public override int ShowFunction()
        {
            Console.Write("Доступные действия: \n\n1. Посмотреть статистику села \n2. Потратить деньги с баланса села \n\nВыберите действие: ");

            int numberFunction = Convert.ToInt32(Console.ReadLine());
            return numberFunction;
        }

        public override void ShowStatus()
        {
            string isElectricity = AvailabilityOfElectricity();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n--Статистика села {nameCity}--");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Название села: {nameCity} \nID населенного пункта: {Identification} \nВремя суток в селе: {_timeOfDay} \nБаланс села: {cityBalance}$ " +
                $"\nНаличие электричества: {isElectricity} \nВозраст села: {_age} лет \n\n");
        }

        public override void TakingInBalance(int Summa)
        {
            if (cityBalance >= Summa)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new String('-', 23));
                Console.WriteLine("Деньги успешно списаны с баланса села!");
                Console.ForegroundColor = ConsoleColor.White;
                cityBalance -= Summa;
                Console.WriteLine($"Баланс cела {nameCity}: {cityBalance}$");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Недостаточно средств!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
