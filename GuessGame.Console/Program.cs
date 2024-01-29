// See https://aka.ms/new-console-template for more information
using GuessGame.Console.Services;
using Microsoft.Extensions.Configuration;

internal class Program
{
    /// <summary>
    /// Сервис для конфигурации 
    /// </summary>
    private static ConfigService _configService;

    private static GameStarterService _gameStarterService;

    private static GuessGameService _guessGameService;

    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

        if (configuration == null)
        {
            throw new Exception("Проверьте наличие файла 'appsettings.json' в проекте. ");
        }

        _configService = new ConfigService(configuration);
        _gameStarterService = new GameStarterService();
        _guessGameService = new GuessGameService(_configService);

        _gameStarterService.StartGame(_guessGameService);
    }
}