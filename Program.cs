using System;
using System.Collections.Generic;

namespace BlackJack
{
  class Program
  {
    static void Main(string[] args)
    {
      //Welcome message
      Console.Clear();
      Console.WriteLine("Welcome to Blackjack! Let's play!");
      Console.WriteLine("");

      //Variable to loop gameplay
      var newGame = true;

      while (newGame)
      {
        //Reset playerBusted to false
        var playerBusted = false;

        //Generate and shuffle deck
        Console.WriteLine("Shuffling the deck...");
        Console.WriteLine("");

        var deckOfCards = GenerateDeckOfCards();

        //Create new player class for playerOne
        var playerOne = new Player();

        //Deal to playerOne
        Console.WriteLine("Dealing Players Hand...");
        Console.WriteLine("");

        //Assign playerOne a playerID of 1
        playerOne.playerID = 1;

        //TEMPORARY LOGIC TO TEST SPLIT
        // var tempCard = new Card();
        // var tempCard2 = new Card();

        // tempCard.CardRank = "Ace";
        // tempCard.CardSuit = "Spades";
        // tempCard.CardValue = 11;

        // tempCard2.CardRank = "Ace";
        // tempCard2.CardSuit = "Clovers";
        // tempCard2.CardValue = 11;

        // playerOne.playerHand.Add(tempCard);
        // playerOne.playerHand.Add(tempCard2);

        //Deal First Card to playerOne
        playerOne.playerHand.Add(deckOfCards[0]);
        deckOfCards.RemoveAt(0);

        //Deal Second Card to playerOne
        playerOne.playerHand.Add(deckOfCards[0]);
        deckOfCards.RemoveAt(0);

        //Display cards dealt to playerOne
        for (var i = 0; i < playerOne.playerHand.Count; i++)
        {
          Console.WriteLine("Player has a(n) " + playerOne.playerHand[i].DisplayCardName());
        }

        //Display total value of playerOne's hand
        Console.WriteLine("");
        Console.WriteLine($"Player has a total of: {playerOne.getValueOfHand(playerOne.playerHand)}");
        Console.WriteLine("");

        if (playerOne.playerHand[0].CardRank == playerOne.playerHand[1].CardRank)
        {
          Console.WriteLine($"You have 2 {playerOne.playerHand[0].CardRank}s. Would you like to Split your hand? (Y)es or (N)o.");

          var wantsToSplit = Console.ReadLine().ToLower();

          while (wantsToSplit != "y" && wantsToSplit != "n")
          {
            Console.WriteLine("Please enter a valid response. You can reply (Y)es or (N)o.");

            wantsToSplit = Console.ReadLine().ToLower();
          }

          if (wantsToSplit == "y")
          {
            //Split Hand
            Console.WriteLine("");
            Console.WriteLine("Splitting Players hand...");
            Console.WriteLine("");

            playerOne.playerHand = playerOne.ResetAllAces(playerOne.playerHand);

            playerOne.playerHandTwo.Add(playerOne.playerHand[0]);
            playerOne.playerHand.RemoveAt(0);

            playerOne.playerHand.Add(deckOfCards[0]);
            deckOfCards.RemoveAt(0);

            playerOne.playerHandTwo.Add(deckOfCards[0]);
            deckOfCards.RemoveAt(0);

            for (var i = 0; i < playerOne.playerHand.Count; i++)
            {
              Console.WriteLine("Players first hand has a(n) " + playerOne.playerHand[i].DisplayCardName());
            }

            //Display total value of playerOne's hand
            Console.WriteLine("");
            Console.WriteLine($"Players first hand has a total of: {playerOne.getValueOfHand(playerOne.playerHand)}");
            Console.WriteLine("");

            for (var i = 0; i < playerOne.playerHandTwo.Count; i++)
            {
              Console.WriteLine("Players second hand has a(n) " + playerOne.playerHandTwo[i].DisplayCardName());
            }

            //Display total value of playerOne's hand
            Console.WriteLine("");
            Console.WriteLine($"Players second hand has a total of: {playerOne.getValueOfHand(playerOne.playerHandTwo)}");
            Console.WriteLine("");
          }
        }

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

        //Create method to pass in a Player and Main Deck of cards
        //This method will loop the "Hit" and "Stand" logic for the player so they can build their hands
        ProcessPlayer(playerOne, deckOfCards, out playerOne, out deckOfCards);

        //Hand (or hands if player split) is updated, determine if playerOne busts
        if (playerOne.playerHandTwo.Count > 0)
        {
          if (playerOne.getValueOfHand(playerOne.playerHand) > 21 && playerOne.getValueOfHand(playerOne.playerHandTwo) > 21)
          {
            playerBusted = true;
          }

          if (playerOne.getValueOfHand(playerOne.playerHandTwo) > 21)
          {
            Console.WriteLine("Players second hand BUSTS!");
          }
        }
        else if (playerOne.getValueOfHand(playerOne.playerHand) > 21)
        {
          playerBusted = true;
        }

        //If at least one hand from player is valid
        if (!playerBusted)
        {
          //Begin dealer 'Hit' or 'Stand' logic
          var dealerValue = dealer.getValueOfHand(dealer.playerHand);

          Console.WriteLine("");

          while (dealerValue < 17)
          {
            Console.WriteLine("Dealer hits...");
            Console.WriteLine("");

            //Dealer hit logic
            dealer.playerHand.Add(deckOfCards[0]);
            deckOfCards.RemoveAt(0);

            dealerValue = dealer.getValueOfHand(dealer.playerHand);
          }

          for (var i = 0; i < dealer.playerHand.Count; i++)
          {
            Console.WriteLine($"Dealer has a(n) {dealer.playerHand[i].DisplayCardName()}");
          }

          Console.WriteLine("");
          Console.WriteLine($"The dealer has a total of: {dealer.getValueOfHand(dealer.playerHand)}");
          Console.WriteLine("");

          if (dealerValue > 21)
          {
            Console.WriteLine("Dealer BUSTS! Player wins!");
            Console.WriteLine("");
          }
          else
          {
            if (playerOne.playerHandTwo.Count > 0)
            {
              if (playerOne.getValueOfHand(playerOne.playerHand) <= 21)
              {
                if (playerOne.getValueOfHand(playerOne.playerHand) > dealer.getValueOfHand(dealer.playerHand))
                {
                  Console.WriteLine("The players first hand beats the dealer!");
                }
                else if (playerOne.getValueOfHand(playerOne.playerHand) < dealer.getValueOfHand(dealer.playerHand))
                {
                  Console.WriteLine("The dealer beats players first hand!");
                }
                else
                {
                  Console.WriteLine("The players first hand ties with the dealer!");
                }
              }
              else
              {
                Console.WriteLine("The players first hand BUSTS! The dealer beats the players first hand.");
              }

              if (playerOne.getValueOfHand(playerOne.playerHandTwo) <= 21)
              {
                if (playerOne.getValueOfHand(playerOne.playerHandTwo) > dealer.getValueOfHand(dealer.playerHand))
                {
                  Console.WriteLine("The players second hand beats the dealer!");
                }
                else if (playerOne.getValueOfHand(playerOne.playerHandTwo) < dealer.getValueOfHand(dealer.playerHand))
                {
                  Console.WriteLine("The dealer beats players second hand!");
                }
                else
                {
                  Console.WriteLine("The players second hand ties with the dealer!");
                }
              }
              else
              {
                Console.WriteLine("The players second hand BUSTS! The dealer beats the players second hand.");
              }
            }
            else
            {
              if (playerOne.getValueOfHand(playerOne.playerHand) <= 21)
              {
                if (playerOne.getValueOfHand(playerOne.playerHand) > dealer.getValueOfHand(dealer.playerHand))
                {
                  Console.WriteLine("Player wins!");
                }
                else if (playerOne.getValueOfHand(playerOne.playerHand) < dealer.getValueOfHand(dealer.playerHand))
                {
                  Console.WriteLine("The Dealer wins!");
                }
                else
                {
                  Console.WriteLine("The game is a tie (push)!");
                }
              }
              else
              {
                Console.WriteLine("Player BUSTS! The dealer wins!");
              }
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

      Console.WriteLine("");
      Console.WriteLine("Thank you for playing! Goodbye!");
      Console.WriteLine("");
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
          newCard.SetCardValue();

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

    static void ProcessPlayer(Player PlayerIn, List<Card> MainDeckIn, out Player PlayerOut, out List<Card> MainDeckOut)
    {
      var tempHand = new List<Card>();

      //Handle first hand
      if (PlayerIn.playerHand.Count > 0)
      {
        Console.WriteLine("Playing first hand...");
        Console.WriteLine("");

        HitOrStand(PlayerIn, PlayerIn.playerHand, MainDeckIn, out tempHand, out MainDeckIn);

        PlayerIn.playerHand = tempHand;
      }

      if (PlayerIn.playerHandTwo.Count > 0)
      {
        Console.WriteLine("");
        Console.WriteLine("Playing second hand...");
        Console.WriteLine("");

        HitOrStand(PlayerIn, PlayerIn.playerHandTwo, MainDeckIn, out tempHand, out MainDeckIn);

        PlayerIn.playerHandTwo = tempHand;
      }

      PlayerOut = PlayerIn;
      MainDeckOut = MainDeckIn;
    }

    static void HitOrStand(Player CurrentPlayer, List<Card> PlayerHandIn, List<Card> DeckIn, out List<Card> PlayerHandOut, out List<Card> DeckOut)
    {
      if (PlayerHandIn.Count > 0)
      {
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
            PlayerHandIn.Add(DeckIn[0]);
            DeckIn.RemoveAt(0);

            Console.WriteLine("");

            for (var i = 0; i < PlayerHandIn.Count; i++)
            {
              Console.WriteLine($"Player hand has a(n) {PlayerHandIn[i].DisplayCardName()}");
            }

            Console.WriteLine("");
            Console.WriteLine($"Player has a total of: {CurrentPlayer.getValueOfHand(PlayerHandIn)}");
            Console.WriteLine("");

            if (CurrentPlayer.getValueOfHand(PlayerHandIn) > 21)
            {
              wantsToHit = false;

              Console.WriteLine("Players hand BUSTS!");
              Console.WriteLine("");
            }
          }
          else if (userResponse == "stand")
          {
            wantsToHit = false;
          }
        }
      }

      PlayerHandOut = PlayerHandIn;
      DeckOut = DeckIn;
    }
  }
}
