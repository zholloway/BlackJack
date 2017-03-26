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

    static class Logic
    {
        public static string AskForInitialPlayerMoney()
        {
            Console.WriteLine("How much money are you coming to the table with?");
            var money = Console.ReadLine();
            return money;
        }

        public static List<Card> CreateDeck()
        {
            var deck = new List<Card>();

            for (int i = 0; i < 7; i++)
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    foreach (Suit s in Enum.GetValues(typeof(Suit)))
                    {
                        deck.Add(new Card(s, r));
                    }
                }
            }
            return deck;
        }

        public static List<Card> ShuffleShoe(List<Card> deck)
        {
            var randomDeck = deck.OrderBy(x => Guid.NewGuid()).ToList();
            return randomDeck;
        }

        public static string AskBetAmount()
        {
            Console.WriteLine("How much would you like to bet for this hand?");
            var betAmount = Console.ReadLine();
            return betAmount;
        }

        public static int ParseMoney(string money, bool initialMoney)
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

        public static bool WouldYouLikeToQuit()
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

        public static Card DealCardFaceUp(List<Card> deck, string playerOrDealer)
        {
            if (playerOrDealer == "player")
            {
                var playerSavedCard = deck.First();
                Console.WriteLine($"{playerSavedCard} was dealt to you.");
                return playerSavedCard;
            }
            else
            {
                var dealerSavedCard = deck.First();
                Console.WriteLine($"{dealerSavedCard} was dealt to the dealer.");
                return dealerSavedCard;
            }
        }

        public static Card DealCardFaceDown(List<Card> deck)
        {
            var dealerSavedCard = deck.First();
            Console.WriteLine("The dealer was dealt one card facedown.");
            return dealerSavedCard;
        }

        public static List<Card> ShrinkDeck(List<Card> deckForGame)
        {
            deckForGame.RemoveAt(0);
            var shrunkDeck = deckForGame;
            return shrunkDeck;
        }

        public static void DealOpeningHands(List<Card> playerHand, List<Card> dealerHand, List<Card> deckForGame)
        {
            playerHand.Add(Logic.DealCardFaceUp(deckForGame, "player"));
            deckForGame = Logic.ShrinkDeck(deckForGame);
            dealerHand.Add(Logic.DealCardFaceUp(deckForGame, "dealer"));
            deckForGame = Logic.ShrinkDeck(deckForGame);
            playerHand.Add(Logic.DealCardFaceUp(deckForGame, "player"));
            deckForGame = Logic.ShrinkDeck(deckForGame);
            dealerHand.Add(Logic.DealCardFaceDown(deckForGame));
            deckForGame = Logic.ShrinkDeck(deckForGame);
        }

        public static int CheckHandValue(List<Card> hand)
        {
            var handValue = 0;

            foreach (var individualCard in hand)
            {
                handValue += individualCard.GetCardValue();
            }

            return handValue;
        }

        public static string AskPlayerHitOrStay(int playerHandValue)
        {
            Console.WriteLine($"You currently have {playerHandValue}. Would you like to 'hit' or 'stay'?");
            var decision = Console.ReadLine();
            /*
            while (decision != "hit" || decision != "stay")
            {
                Console.WriteLine("Sorry, please type 'hit' or 'stay' as your choice.");
                decision = Console.ReadLine();
            }
            */

            Console.WriteLine($"You have chosen to {decision}.");

            return decision;
        }

        public static bool CheckForBlackjack(string playerOrDealer, int handValue)
        {
            var blackjackStatus = false;

            if (handValue == 21 && playerOrDealer == "player")
            {
                Console.WriteLine("You have 21! Congratulations!");
                blackjackStatus = true;
                return blackjackStatus;
            }
            else if (handValue == 21 && playerOrDealer == "dealer")
            {
                Console.WriteLine("The dealer has 21. You lose.");
                blackjackStatus = true;
                return blackjackStatus;
            }

            return blackjackStatus;
        }

        public static int AdjustForAceDecision()
        {
            var parsedDecision = 0;

            Console.WriteLine($"You hit an Ace. Would you like it to be worth '1' or '11'?");
            var aceDecision = Console.ReadLine();
            var wasSuccessful = int.TryParse(aceDecision, out parsedDecision);

            while (parsedDecision != 1 && parsedDecision != 11)
            {
                Console.WriteLine("Oops, that is not a valid choice. Please try again.");
                aceDecision = Console.ReadLine();
                wasSuccessful = int.TryParse(aceDecision, out parsedDecision);
            }

            return parsedDecision;
        }

        public static int CheckOpeningHandValueAce (List<Card> playerHand)
        {
            var totalValue = 0;

            foreach (var card in playerHand)
            {
                if (card.GetCardValue() == 11)
                {
                    totalValue += AdjustForAceDecision();
                }
                else
                {
                    totalValue += card.GetCardValue();
                }
            }

            return totalValue;
        }
    }
}
