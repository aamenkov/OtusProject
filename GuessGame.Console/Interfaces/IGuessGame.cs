namespace GuessGame.Console.Interfaces
{
    public interface IGuessGame : IGame
    {
        int NumberOfAttemts { get; }
        int MinRange { get; }
        int MaxRange { get; }

        void TryGuess(int number);
    }
}
