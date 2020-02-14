using System;
using System.Collections.Generic;

namespace BlackJack
{
  class Program
  {
    static void Main(string[] args)
    {
      //Welcome message
      Console.WriteLine("Welcome to Blackjack! Let's play!");
      Console.WriteLine("");

      //Variable to loop gameplay
      var newGame = true;

      var playerBusted = false;

      while (newGame)
      {
        //Generate and shuffle deck
        Console.WriteLine("Shuffling the deck...");
        Console.WriteLine("");

        var deckOfCards = new List<Card>();

        deckOfCards = GenerateDeckOfCards();

        //Create new player class for playerOne
        var playerOne = new Player();

        //Deal to playerOne
        Console.WriteLine("Dealing Player 1s Hand...");
        Console.WriteLine("");

        //Assign playerOne a playerID of 1
        playerOne.playerID = 1;

        //Deal First Card to playerOne
        playerOne.playerHand.Add(deckOfCards[0]);
        deckOfCards.RemoveAt(0);

        //Deal Second Card to playerOne
        playerOne.playerHand.Add(deckOfCards[0]);
        deckOfCards.RemoveAt(0);

        //Display cards dealt to playerOne
        for (var j = 0; j < playerOne.playerHand.Count; j++)
        {
          Console.WriteLine("Player 1 has a(n) " + playerOne.playerHand[j].DisplayCardName());
        }

        //Display total value of playerOne's hand
        Console.WriteLine("");
        Console.WriteLine($"Player 1 has a total of: {playerOne.getValueOfHand()}");
        Console.WriteLine("");

        //Create new player class for dealer
        var dealer = new Player();

        //Deal to dealer
        Console.WriteLine("Dealing Dealers Hand...");
        Console.WriteLine("");

        //Assign dealer playerID of 0
        dealer.playerID = 0;

        //Deal first card to dealer
        dealer.playerHand.Add(deckOfCards[0]);
        deckOfCards.RemoveAt(0);

        //Deal second card to dealer
        dealer.playerHand.Add(deckOfCards[0]);
        deckOfCards.RemoveAt(0);

        //ADVENTURE: Display top card for dealers hand
        Console.WriteLine($"Dealer has a(n) {dealer.playerHand[dealer.playerHand.Count - 1].DisplayCardName()} and 1 hidden card.");
        Console.WriteLine("");

        //Begin allowing player to "hit" or "stand"
        var wantsToHit = true;

        while (wantsToHit)
        {
          Console.WriteLine("Would you like to (HIT) or (STAND)?");

          var userResponse = Console.ReadLine().ToLower();

          while (userResponse != "hit" && userResponse != "stand")
          {
            Console.WriteLine("Please enter a valid response. You can (HIT) or (STAND).");

            userResponse = Console.ReadLine().ToLower();
          }

          if (userResponse == "hit")
          {
            playerOne.playerHand.Add(deckOfCards[0]);
            deckOfCards.RemoveAt(0);

            for (var i = 0; i < playerOne.playerHand.Count; i++)
            {
              Console.WriteLine($"Player 1 has a(n) {playerOne.playerHand[i].DisplayCardName()}");
            }

            Console.WriteLine("");

            Console.WriteLine($"Player 1 has a total of: {playerOne.getValueOfHand()}");
            Console.WriteLine("");

            if (playerOne.getValueOfHand() > 21)
            {
              Console.WriteLine("BUST! Player 1 loses!");

              wantsToHit = false;
              playerBusted = true;
            }
          }
          else if (userResponse == "stand")
          {
            wantsToHit = false;
          }
        }

        if (!playerBusted)
        {
          //Begin dealer 'Hit' or 'Stand' logic
          var dealerValue = dealer.getValueOfHand();

          while (dealerValue < 17)
          {
            Console.WriteLine("Dealer hits...");
            Console.WriteLine("");

            //Dealer hit logic
            dealer.playerHand.Add(deckOfCards[0]);
            deckOfCards.RemoveAt(0);

            dealerValue = dealer.getValueOfHand();
          }

          for (var i = 0; i < dealer.playerHand.Count; i++)
          {
            Console.WriteLine($"Dealer has a(n) {dealer.playerHand[i].DisplayCardName()}");
          }

          Console.WriteLine("");
          Console.WriteLine($"The dealer has a total of: {dealer.getValueOfHand()}");
          Console.WriteLine("");

          if (dealerValue > 21)
          {
            Console.WriteLine("Dealer BUSTS! Player 1 wins!");
            Console.WriteLine("");
          }
          else
          {
            if (playerOne.getValueOfHand() > dealer.getValueOfHand())
            {
              Console.WriteLine("Player 1 wins!");
            }
            else if (playerOne.getValueOfHand() < dealer.getValueOfHand())
            {
              Console.WriteLine("The Dealer wins!");
            }
            else
            {
              Console.WriteLine("The game is a tie (push)!");
            }

            Console.WriteLine("");
          }
        }

        Console.WriteLine("Game Over! Would you like to play again? Reply (YES) or (NO).");

        var playAgain = Console.ReadLine().ToLower();

        while (playAgain != "yes" && playAgain != "no")
        {
          Console.WriteLine("Please enter a valid response. You can reply (YES) or (NO).");

          playAgain = Console.ReadLine().ToLower();
        }

        if (playAgain == "no")
        {
          newGame = false;
        }
        else
        {
          Console.Clear();
        }
      }
    }

    static List<Card> GenerateDeckOfCards()
    {
      var cardSuits = new string[] { "Spades", "Clovers", "Diamonds", "Hearts" };
      var cardRanks = new string[] {"Two", "Three", "Four", "Five", "Six", "Seven", "Eight",
                                    "Nine", "Ten", "Jack", "Queen", "King", "Ace"};

      var deckOfCards = new List<Card>();

      for (var i = 0; i < cardSuits.Length; i++)
      {
        for (var j = 0; j < cardRanks.Length; j++)
        {
          var newCard = new Card();

          newCard.CardRank = cardRanks[j];
          newCard.CardSuit = cardSuits[i];

          deckOfCards.Add(newCard);
        }
      }

      var tempSaveCard = new Card();

      for (var i = 0; i < deckOfCards.Count; i++)
      {
        Random rnd = new Random();

        int positionToSwap = rnd.Next(deckOfCards.Count);

        tempSaveCard = deckOfCards[i];
        deckOfCards[i] = deckOfCards[positionToSwap];
        deckOfCards[positionToSwap] = tempSaveCard;
      }

      return deckOfCards;
    }
  }
}
