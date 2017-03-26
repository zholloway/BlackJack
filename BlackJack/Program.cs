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
            var handCounter = 1;

            var gameComplete = false;

            while (gameComplete == false)
            {
                //hand loop

                Console.WriteLine("----");
                Console.WriteLine($"Hand {handCounter}");
                Console.WriteLine("----");

                //report current money amount and ask for bet
                Console.Write($"Your current funds are: ${moneyAmount}. ");
                var betAmount = Logic.ParseMoney(Logic.AskBetAmount(), false);
                //make sure bet is possible
                while (betAmount > moneyAmount)
                {
                    Console.WriteLine("You don't have that much money.");
                    betAmount = Logic.ParseMoney(Logic.AskBetAmount(), false);
                }

                //shuffle deck
                Logic.ShuffleShoe(deckForGame);

                //deal opening hands
                Logic.DealOpeningHands(playerHand, dealerHand, deckForGame);

                //if player or dealer has Blackjack, end game immediately
                var playerHandValue = Logic.CheckHandValue(playerHand);
                var dealerHandValue = Logic.CheckHandValue(dealerHand);

                var playerBlackjackStatus = Logic.CheckForBlackjack("player", playerHandValue);
                var dealerBlackjackStatus = Logic.CheckForBlackjack("dealer", dealerHandValue);

                var playerWinHand = false;

                if (playerBlackjackStatus == true)
                {
                    playerWinHand = true;
                }
                else if (dealerBlackjackStatus == true)
                {
                    playerWinHand = false;
                }
                else
                {
                    //report playHandValue and ask hit or stay
                    var playerBust = false;
                    var playerDecision = Logic.AskPlayerHitOrStay(playerHandValue);

                    //player hit loop
                    while (playerDecision == "hit" && playerBust == false && playerBlackjackStatus == false)
                    {
                        playerHand.Add(Logic.DealCardFaceUp(deckForGame, "player"));
                        deckForGame = Logic.ShrinkDeck(deckForGame);
                        playerHandValue = Logic.CheckHandValue(playerHand);

                        var cardsInHand = 0;
                        foreach (var card in playerHand)
                        {
                            cardsInHand++;
                        }

                        if (playerHandValue < 21 && cardsInHand == 6)
                        {
                            Console.WriteLine("You have 6 cards in hand and are still under 21. You win!");
                            playerWinHand = true;
                            playerBlackjackStatus = true;
                        }
                        else if (playerHandValue < 21)
                        {
                            playerDecision = Logic.AskPlayerHitOrStay(playerHandValue);
                        }
                        else if (playerHandValue > 21)
                        {
                            Console.WriteLine($"You have {playerHandValue}. You lose.");
                            playerBust = true;
                            playerWinHand = false;
                        }
                        else
                        {
                            playerBlackjackStatus = Logic.CheckForBlackjack("player", playerHandValue);
                            playerWinHand = true;
                        }
                    }

                    //run dealerDeterminehitorstay logic

                    //dealer hit
                    if (dealerHandValue < 17 && playerBust == false && playerBlackjackStatus == false)
                    {
                        Console.WriteLine($"Dealer reveals {dealerHand[1]}. Dealer showing {dealerHand[0]} and {dealerHand[1]}.");
                        while (dealerHandValue < 17 && dealerHandValue < playerHandValue)
                        {
                            dealerHand.Add(Logic.DealCardFaceUp(deckForGame, "dealer"));
                            deckForGame = Logic.ShrinkDeck(deckForGame);
                            dealerHandValue = Logic.CheckHandValue(dealerHand);
                        }

                        if (dealerHandValue > 21)
                        {
                            Console.WriteLine("The dealer busts. You win!");
                            playerWinHand = true;
                        }
                        else if (dealerHandValue < 21 && dealerHandValue > playerHandValue)
                        {
                            Console.WriteLine($"The dealer has {dealerHandValue} and you have {playerHandValue}. You lose.");
                            playerWinHand = false;
                        }
                        else if (dealerHandValue < 22 && dealerHandValue < playerHandValue)
                        {
                            Console.WriteLine($"The dealer has {dealerHandValue} and you have {playerHandValue}. You win!");
                            playerWinHand = true;
                        }
                        else if (dealerHandValue == 21 && playerHandValue < 21)
                        {
                            Console.WriteLine("The dealer has 21. You lose.");
                            playerWinHand = false;
                        }
                    }
                    //dealer stay
                    else if (dealerHandValue >= 17 && dealerHandValue >= playerHandValue && playerBust == false && playerBlackjackStatus == false)
                    {
                        Console.WriteLine($"Dealer reveals {dealerHand[1]}. Dealer showing {dealerHand[0]} and {dealerHand[1]}.");
                        Console.WriteLine($"The dealer has {dealerHandValue} and you have {playerHandValue}. You lose.");
                        playerWinHand = false;
                    }
                    else if (dealerHandValue >= 17 && dealerHandValue < playerHandValue && playerBust == false && playerBlackjackStatus == false)
                    {
                        Console.WriteLine($"Dealer reveals {dealerHand[1]}. Dealer showing {dealerHand[0]} and {dealerHand[1]}.");
                        Console.WriteLine($"The dealer has {dealerHandValue} and you have {playerHandValue}. You win!");
                        playerWinHand = true;
                    }
                }
                
                //adjust moneyAmount based on player win or lose
                if (dealerHandValue == 21 && playerHandValue == 21)
                {
                    Console.WriteLine($"Push! The dealer has {dealerHandValue} and you have {playerHandValue}. Your bet is returned to you.");
                }
                else if (playerWinHand == true)
                {
                    moneyAmount += betAmount;
                }
                else
                {
                    moneyAmount -= betAmount;
                }

                //check to see if player is out of money. if not, check to see if they want to keep playing
                if (moneyAmount == 0)
                {
                    Console.WriteLine("You are out of money! Goodbye.");
                    gameComplete = true;
                }
                else
                {
                    playerHand.Clear();
                    dealerHand.Clear();
                    handCounter++;
                    gameComplete = Logic.WouldYouLikeToQuit();
                }
            }
            //inform player of winnings as they leave table
            Console.WriteLine("----");
            Console.WriteLine($"Thank you for playing. You are leaving with {moneyAmount:C}.");
            Console.ReadLine();
        }
    }
}
