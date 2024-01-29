using GuessGame.Console.Interfaces;

namespace GuessGame.Console.Services
{
    public class GameStarterService
    {
        public GameStarterService() { }

        public void StartGame(IGame game)
        {
            game.Start();
        }
    }
}
