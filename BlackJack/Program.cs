using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            //welcome player
            var deckForGame = Logic.InitializeBlackJack();
            var moneyAmount = Logic.ParseMoney(Logic.AskForInitialPlayerMoney(), true);
            var playerHand = new List<Card>();
            var dealerHand = new List<Card>();

            var gameComplete = false;

            while (gameComplete == false)
            {
            //hand loop
                //report current money amount
                Console.Write($"Your current funds are: ${moneyAmount}. ");
                //how much do you want to bet
                var betAmount = Logic.ParseMoney(Logic.AskBetAmount(), false);
                //shuffle deck
                Logic.ShuffleShoe(deckForGame);
                //deal opening hands
                Logic.DealOpeningHands(playerHand, dealerHand, deckForGame);
                //if player has Blackjack, end game immediately
                //player stay or hit
                //determine player bust
                //computer stay or hit
                //determine computer bust
                //determine winner of hand
                //adjust player money count
                //ask if player would like to play another hand or quit
                gameComplete = Logic.WouldYouLikeToQuit();

                   
                 Console.ReadLine();
            }
        }
    }
}
