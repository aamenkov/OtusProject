using GuessGame.Console.Interfaces;
using System;

namespace GuessGame.Console.Services
{
    public class GuessGameService : IGuessGame
    {
        public string Name => "Угадай число!";

        private ConfigService configService;

        public int NumberOfAttemts { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }

        private int _guessNumber { get; set; }

        public GuessGameService(ConfigService configService)
        {
            NumberOfAttemts = configService.NumberOfAttemts;
            MinRange = configService.MinRange;
            MaxRange = configService.MaxRange;
        }

        private int GenerateNumber()
        {
            Random random = new Random();
            return random.Next(MinRange, MaxRange);
        }

        public void TryGuess(int number)
        {
            var guessNumber = _guessNumber;

            if (guessNumber > number)
            {
                System.Console.WriteLine("Искомое число больше вашего.");
            }
            else if (guessNumber < number)
            {
                System.Console.WriteLine("Искомое число меньше вашего.");
            }
            else
            {
                System.Console.WriteLine("Вы угадали! Вы молодец!");
                Environment.Exit(0);
            }
        }

        public void Start()
        {
            System.Console.WriteLine("Добро пожаловать в игру 'Угадай число'.");
            _guessNumber = GenerateNumber();

            for (int i = 0; i < NumberOfAttemts; i++)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("Введите число:");
                var arg = System.Console.ReadLine();

                var exitResult = int.TryParse(arg, out int res);
                if (!exitResult)
                {
                    System.Console.WriteLine("Введите число. Минус попытка.");
                    continue;
                }

                TryGuess(res);
            }
            System.Console.WriteLine("Вы проиграли. Попробуйте еще раз :с ");
        }
    }
}
