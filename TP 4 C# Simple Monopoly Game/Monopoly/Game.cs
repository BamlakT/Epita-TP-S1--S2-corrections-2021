using System;
using System.Collections.Generic;

namespace Monopoly
{
    public class Game
    {
        private const int BeginCell = 200;
        Random random = new Random();
        private List<Cell> board;
        private List<Player> players;
        private int playersnumber;
        private int boardSize;

        public List<Player> Players => players;
        public int BoardSize => boardSize;

        public int PlayersNumber=> playersnumber;
       
        public List<Cell> Board => board;
        public Game(int boardSize)
        {
            this.boardSize = boardSize;
            this.board = new List<Cell>();
            this.players = new List<Player>();
        }

        public int RollDice()
        {
            return random.Next(1, 13);
        }

        private int GetInput(Player player, Property property)
        {
            Console.WriteLine("No one owns the street you can buy it for {0}£, you have {1}£ left.\n" +
                              "Press 1 if you want to buy it, 0 otherwise.", property.Price,
                player.Balance);
            string input = Console.ReadLine();
            while (input == null || input != "1" && input != "0")
            {
                input = Console.ReadLine();
            }

            return Int32.Parse(input);
        }

        public void BuyProperty(Property property, Player player, int input)
        {
            if (input == 1)
            {
                if (player.Buy(property))
                    Console.WriteLine("You bought {0}, your balance is now: {1}£", property.Name, player.Balance);
                else
                    Console.WriteLine("Insufficient funds");
            }
        }

        public bool OnRegular(Cell cell, Player player)
        {
            Property property = (Property) cell;
            Console.WriteLine("You're now on the cell {0}", property.Name);
            if (property.Owner == null)
            {
                int input = GetInput(player, property);
                BuyProperty(property, player, input);
            }
            else if (property.Owner != player)
            {
                Console.WriteLine("This cell is owned by {0} and the rent cost is {1}£",
                    property.Owner.Name, property.RentCost);
                bool transfer = player.TransferTo(property.Owner, property.RentCost);
                if (transfer)
                    Console.WriteLine("You paid {0}£, your balance is now {1}£",
                        property.RentCost, player.Balance);
                else
                {
                    PlayerLost(player);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("You own this property !");
            }

            return true;
        }

        public bool OnTax(Tax tax, Player player)
        {
            Console.WriteLine("You are now on a tax cell: you have to pay: {0}£", tax.Amount);
            if (tax.TaxPlayer(player))
                Console.WriteLine("You paid {0}£, your balance is now {1}£", tax.Amount, player.Balance);
            else
            {
                PlayerLost(player);
                return false;
            }

            return true;
        }

        public bool PlayRound(Player player)
        {
            int rolled = RollDice();
            Console.WriteLine("{0}, this is your turn, you rolled {1}", player.Name, rolled);
            if (player.jailed)
            {
                Jail.AttemptDiceDouble(player);
                return true;
            }

            if (player.Position + rolled > boardSize)
                player.ReceiveMoney(BeginCell);
            player.Move(rolled, boardSize);
            Cell currentCell = board[player.Position];
            switch (currentCell)
            {
                case Street:
                case Station:
                case Company:
                    return OnRegular(currentCell, player);
                case Tax tax:
                    return OnTax(tax, player);
                case Luck luck:
                    return luck.GetEffect(player);
                case Jail:
                    player.SendToJail();
                    return true;
                default:
                    return true;
            }
        }

        public bool PlayerWon()
        {
            if (playersnumber == 1)
            {
                Console.WriteLine("Congratulations {0}, you won !", players[0].Name);
                return true;
            }

            return false;
        }

        public void PlayerLost(Player player)
        {
            Console.WriteLine("You don't have enough money, you lost !");
            foreach (Property p in player.Possessions)
                p.Owner = null;
            player.Possessions.Clear();
            players.Remove(player);
            playersnumber--;
        }

        public void AddBoard(Cell cell)
        {
            boardSize++;
            board.Add(cell);
        }

        public void AddPlayer(Player player)
        {
            this.playersnumber++;
            this.players.Add(player);
        }

        public void Play()
        {
            int index = 0;
            Player player = players[index];
            while (!PlayerWon())
            {
                Console.WriteLine("{0} this is your turn to play please press any key to roll the dice",
                    player.Name);
                Console.ReadLine();
                DisplayBoard();
                bool playerInGame = PlayRound(player);
                int increment = playerInGame ? 1 : 0;
                index = (index + increment) % playersnumber;
                player = players[index];
            }
        }

        public void DisplayBoard()
        {
            int position = 0;
            foreach (Cell c in board)
            {
                Console.WriteLine(c);
                foreach (Player player in players)
                {
                    if (player.Position == position)
                        Console.WriteLine($"\t* {player.Name} ({player.Balance}£)");
                }

                ++position;
            }
        }
    }
}