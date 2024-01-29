using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessGame.Console.Interfaces
{
    public interface IGame
    {
        string Name { get; }

        void Start();
    }
}
