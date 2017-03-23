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
                    //deal cards
                    //stay or hit
                    //computer stay or hit
                    //determine winner of hand
                    //adjust player money count
                    //ask if player would like to play another hand or quit
                    gameComplete = Logic.WouldYouLikeToQuit();


                    gameComplete = true;
                    Console.ReadLine();
            }
        }
    }
}
