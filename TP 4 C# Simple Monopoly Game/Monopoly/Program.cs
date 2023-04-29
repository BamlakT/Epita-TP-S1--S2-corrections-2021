namespace Monopoly
{
    class Program
    {
        static void Main()
        {
            Game game = Serializer.Load("../../../GameLayout/game");
            //Do not modify the line above
            Player player = new Player("Respo No Fun", 55, 0);
            game.DisplayBoard();
        }
    }
}