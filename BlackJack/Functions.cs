using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public enum Suit
    {
        Hearts,
        Clubs,
        Diamonds,
        Spades
    }

    public enum Rank
    {
        Ace,
        Deuce,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public Card(Suit s, Rank r)
        {
            this.Suit = s;
            this.Rank = r;
        }

        public int GetCardValue()
        {
            var rv = 0;
            switch (this.Rank)
            {
                case Rank.Ace:
                    rv = 11;
                    break;
                case Rank.Deuce:
                    rv = 2;
                    break;
                case Rank.Three:
                    rv = 3;
                    break;
                case Rank.Four:
                    rv = 4;
                    break;
                case Rank.Five:
                    rv = 5;
                    break;
                case Rank.Six:
                    rv = 6;
                    break;
                case Rank.Seven:
                    rv = 7;
                    break;
                case Rank.Eight:
                    rv = 8;
                    break;
                case Rank.Nine:
                    rv = 9;
                    break;
                case Rank.Ten:
                    rv = 10;
                    break;
                case Rank.Jack:
                    rv = 10;
                    break;
                case Rank.Queen:
                    rv = 10;
                    break;
                case Rank.King:
                    rv = 10;
                    break;
                default:
                    break;
            }
            return rv;
        }

        public override string ToString()
        {
            return $"The {this.Rank} of {this.Suit}";
        }
    }

    /*
    var deck = new List<Card>();

    foreach (Rank r in Enum.GetValues(typeof(Rank)))
    {
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
                deck.Add(new Card(s, r));
        }
    }
    
    //sort the deck. NOTICE that the variable 'deck' is unchanged, but 'randomDeck' is the actual sorted deck.
    var randomDeck = deck.OrderBy(x => Guid.NewGuid()).ToList();
    */

    static class Logic
    {
        public static List<Card> InitializeBlackJack()
        {
            Console.WriteLine("Welcome to the BlackJack table. It's you versus the dealer.");
            var deck = ShuffleShoe(CreateDeck());
            return deck;
        }

        public static string AskForInitialPlayerMoney ()
        {
            Console.WriteLine("How much money are you coming to the table with?");
            var money = Console.ReadLine();
            return money;
        }

        public static List<Card> CreateDeck()
        {
            var deck = new List<Card>();

            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                foreach (Suit s in Enum.GetValues(typeof(Suit)))
                {
                    deck.Add(new Card(s, r));
                }
            }

            return deck;
        }

        public static List<Card> ShuffleShoe(List<Card> deck)
        {
            var randomDeck = deck.OrderBy(x => Guid.NewGuid()).ToList();
            return randomDeck;
        }

        public static string AskBetAmount ()
        {
            Console.WriteLine("How much would you like to bet for this hand?");
            var betAmount = Console.ReadLine();
            return betAmount;
        }

        public static int ParseMoney (string money, bool initialMoney)
        {
            var wasSuccessful = int.TryParse(money, out int parsedMoney);

            while (wasSuccessful != true)
            {
                Console.WriteLine("Oops, that is not number. Please try again.");
                if (initialMoney == true)
                {
                    money = AskForInitialPlayerMoney();
                }
                else
                {
                    money = AskBetAmount();
                }
                wasSuccessful = int.TryParse(money, out parsedMoney);
            }

            return parsedMoney;
        }

        public static bool WouldYouLikeToQuit ()
        {
            Console.WriteLine("Would you like to play another hand or leave the table? Please answer 'play' or 'leave'.");
            var decision = Console.ReadLine();
            var quit = false;

            if (decision == "leave")
            {
                quit = true;
            }

            return quit;
        }
    }
}
