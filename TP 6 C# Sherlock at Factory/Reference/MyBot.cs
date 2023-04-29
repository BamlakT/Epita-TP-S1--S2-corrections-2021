namespace Reference
{
    public class MyBot : Bot
    {
        public override void Start(Game game)
        {
            game.Build(MachineType.Flask);
        }

        // TODO
        public override void Update(Game game)
        {
            game.UpdateMoneyAll();
            
            if (game.Round == 1)
                game.Produce(MachineType.Flask, 10);

            if (game.Round is 10 or 30)
                game.UpgradeAll();
            
            for (int i = 0; i < ((game.Round < 10) ? 6 : 20); i++)
                game.Build(MachineType.Flask);

            game.Produce(MachineType.Flask, (uint) (game.Round < 30 ? 500 : 1300));
            
            game.UpdateMoneyAll();
        }

        public override void End(Game game)
        {
            game.DestroyAll();
        }

    }
}