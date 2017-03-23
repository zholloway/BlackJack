using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Requirements
    {
        //Level 1
            // Don't consider Aces as possible 1's...they are always 11 --- est. time = 0min
                //Ace value already set to 1 in provided code

            // This is a 2 Hand game(dealer and player) --- est. time = 30min

            // No splitting --- est. time = 0min
                //no coding required 

            // Game uses 1 - 52 card deck with no wilds --- est. time = 0min;
                // already provided

            // New shuffled deck every game - 30min;
                //create function ShuffleDeck that occurs every NewHand. NewHand performs Deal() to player, then computer, then player, then computer.

            // No betting at all --- 0min
                //no coding required

            // Dealer plays by set rules:
                // Dealer hits if they hold less than 16, otherwise Dealer stays --- est. time = 30min
                    //during ComputerTurn, use ComputerHitOrStay to Hit() if computerHandValue < 16, else Stay()
                // if you get 21 on the deal(blackjack!) then you automatically win --- est. time = 30min
                    //during PlayerTurn, check playerHandValue after Deal(). if playerHandValue == 21 during check, player automatically wins hand. 
                // you can see only the first card dealt to the dealer. All others are hidden. = 30min
                    //after Deal(), perform GetCardValue on computerCardOne. each computerCard beyond the first is reported to the player as "facedown"

            //total estimated time: 2 and 1/2 hours + 2 hours of other function setup (player HitOrStay loop, etc) and becoming comfortable with enum/class 

        //Level 2
            // You win if you have 6 cards and stay under 21 --- est. time = 20min
                //if playHandValue is < 21 && playerHandCount > 5, player wins. requires adding new condition to HitOrStay decision loop
            // Let the player choose if an Ace is a 1 or an 11 --- est. time = 30min
                //if player is dealt Ace, prompt for 1 or 11 value, then adjust playHandValue before checking playerHandValue for breaking HitOrStay loop
            //total estimated time: 50min

        //Level 3
            // Add simple betting, the user starts off with $100 and can bet/win/lose with each hand --- est. time = 30min
                // ask for bet amount before hand is dealt. if player wins, add that much to moneyAmount/lose, subtract that much. check moneyAmount after hand is over - if 0, game is over
            // Add 7 decks to the game in a "Shoe", shuffle them all together, deal from the "Shoe". --- est. time = 30 min
                //generate 6 more decks and splice them together to make the game's one Deck
            //total estimated time: 1hr

        //total time = ~7 hours
    }
}
