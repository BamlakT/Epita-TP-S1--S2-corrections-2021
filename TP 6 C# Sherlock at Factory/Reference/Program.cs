using System;

namespace Reference
{
    class Program
    {
        static void Main(string [] args)
        {
            Game game = new Game(100, 500);
            Bot bot = new MyBot();

            Console.WriteLine("Score: " + game.Launch(bot));
        }
    }
}