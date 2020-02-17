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

      //Gameplay loop
      while (newGame)
      {
        //Reset playerBusted to false
        var playerBusted = false;

        //Generate and shuffle deck
        Console.WriteLine("Shuffling the deck...");
        Console.WriteLine("");

        var deckOfCards = GenerateDeckOfCards();

        //Create new "player" for playerOne
        var playerOne = new Player();

        //Deal to playerOne
        Console.WriteLine("Dealing Players Hand...");
        Console.WriteLine("");

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

        //If player has 2 cards of the same rank, allow player to "Split"
        if (playerOne.playerHand[0].CardRank == playerOne.playerHand[1].CardRank)
        {
          Console.WriteLine($"You have 2 {playerOne.playerHand[0].CardRank}s. Would you like to Split your hand? (YES) or (NO).");

          var wantsToSplit = Console.ReadLine().ToLower();

          while (wantsToSplit != "yes" && wantsToSplit != "no")
          {
            Console.WriteLine("Please enter a valid response. You can reply (YES) or (NO).");

            wantsToSplit = Console.ReadLine().ToLower();
          }

          //Split hand into two hands
          if (wantsToSplit == "yes")
          {
            Console.WriteLine("");
            Console.WriteLine("Splitting Players hand...");
            Console.WriteLine("");

            //If both Aces, one would have already been reduced to 1, so reset both to value 11 before "split"
            playerOne.playerHand = playerOne.ResetAllAces(playerOne.playerHand);

            //Move 1 card from "first" hand to "second" hand
            playerOne.playerHandTwo.Add(playerOne.playerHand[0]);
            playerOne.playerHand.RemoveAt(0);

            //Deal one additional card to players "first" hand
            playerOne.playerHand.Add(deckOfCards[0]);
            deckOfCards.RemoveAt(0);

            //Deal one additional card to players "second" hand
            playerOne.playerHandTwo.Add(deckOfCards[0]);
            deckOfCards.RemoveAt(0);

            //Display players updated "first" hand
            for (var i = 0; i < playerOne.playerHand.Count; i++)
            {
              Console.WriteLine("Players first hand has a(n) " + playerOne.playerHand[i].DisplayCardName());
            }

            //Display total value of players "first" hand
            Console.WriteLine("");
            Console.WriteLine($"Players first hand has a total of: {playerOne.getValueOfHand(playerOne.playerHand)}");
            Console.WriteLine("");

            //Display players updated "second" hand
            for (var i = 0; i < playerOne.playerHandTwo.Count; i++)
            {
              Console.WriteLine("Players second hand has a(n) " + playerOne.playerHandTwo[i].DisplayCardName());
            }

            //Display total value of players "second" hand
            Console.WriteLine("");
            Console.WriteLine($"Players second hand has a total of: {playerOne.getValueOfHand(playerOne.playerHandTwo)}");
            Console.WriteLine("");
          }
        }

        //Create new "player" for dealer
        var dealer = new Player();

        //Deal to dealer
        Console.WriteLine("Dealing Dealers Hand...");
        Console.WriteLine("");

        //Deal first card to dealer
        dealer.playerHand.Add(deckOfCards[0]);
        deckOfCards.RemoveAt(0);

        //Deal second card to dealer
        dealer.playerHand.Add(deckOfCards[0]);
        deckOfCards.RemoveAt(0);

        //ADVENTURE: Display top card for dealers hand
        Console.WriteLine($"Dealer has a(n) {dealer.playerHand[dealer.playerHand.Count - 1].DisplayCardName()} and 1 hidden card.");
        Console.WriteLine("");

        //Method to loop the "Hit" and "Stand" logic for the player so they can build their hands
        ProcessPlayer(playerOne, deckOfCards, out playerOne, out deckOfCards);

        //Analyze the players hand (or both hands if "split") to determine if any hands busted
        if (playerOne.playerHandTwo.Count > 0)
        {
          if (playerOne.getValueOfHand(playerOne.playerHand) > 21 && playerOne.getValueOfHand(playerOne.playerHandTwo) > 21)
          {
            playerBusted = true;
          }
          else if (playerOne.getValueOfHand(playerOne.playerHand) > 21)
          {
            Console.WriteLine("Players first hand BUSTS!");
          }
          else if (playerOne.getValueOfHand(playerOne.playerHandTwo) > 21)
          {
            Console.WriteLine("Players second hand BUSTS!");
          }
        }
        else if (playerOne.getValueOfHand(playerOne.playerHand) > 21)
        {
          playerBusted = true;

          Console.WriteLine("Players hand BUSTS!");
        }

        //If at least one hand from player is valid, process dealers hand
        if (!playerBusted)
        {
          //Begin dealer 'Hit' or 'Stand' logic
          var dealerValue = dealer.getValueOfHand(dealer.playerHand);

          Console.WriteLine("");

          //"Hit" on dealers hand until dealer has 17 or greater
          while (dealerValue < 17)
          {
            Console.WriteLine("Dealer hits...");
            Console.WriteLine("");

            //Dealer hit logic
            dealer.playerHand.Add(deckOfCards[0]);
            deckOfCards.RemoveAt(0);

            dealerValue = dealer.getValueOfHand(dealer.playerHand);
          }

          //Display dealers final hand
          for (var i = 0; i < dealer.playerHand.Count; i++)
          {
            Console.WriteLine($"Dealer has a(n) {dealer.playerHand[i].DisplayCardName()}");
          }

          //Display dealers hand total
          Console.WriteLine("");
          Console.WriteLine($"The dealer has a total of: {dealerValue}");
          Console.WriteLine("");

          //Determine the winner
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
                Console.WriteLine("The dealer beats the players first hand.");
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
                Console.WriteLine("The dealer beats the players second hand.");
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

        //Game over message. Ask the users if they want to play again
        Console.WriteLine("Game Over! Would you like to play again? Reply (YES) or (NO).");

        var playAgain = Console.ReadLine().ToLower();

        while (playAgain != "yes" && playAgain != "no")
        {
          Console.WriteLine("Please enter a valid response. You can reply (YES) or (NO).");

          playAgain = Console.ReadLine().ToLower();
        }

        //If "new game", then clear console for next hand, otherwise exit the game
        if (playAgain == "no")
        {
          newGame = false;
        }
        else
        {
          Console.Clear();
        }
      }

      //Goodbye message
      Console.WriteLine("");
      Console.WriteLine("Thank you for playing! Goodbye!");
      Console.WriteLine("");
    }

    static List<Card> GenerateDeckOfCards()
    {
      //Build deck of 52 unique playing cards
      var cardSuits = new string[] { "Spades", "Clubs", "Diamonds", "Hearts" };
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

      //Shuffle the deck of cards
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

      //Process Hit or Stand logic for first hand
      if (PlayerIn.playerHand.Count > 0 && PlayerIn.getValueOfHand(PlayerIn.playerHand) < 21)
      {
        if (PlayerIn.playerHandTwo.Count > 0)
        {
          Console.WriteLine("Playing first hand...");
        }
        else
        {
          Console.WriteLine("Playing hand...");
        }

        Console.WriteLine("");

        HitOrStand(PlayerIn, PlayerIn.playerHand, MainDeckIn, out tempHand, out MainDeckIn);

        PlayerIn.playerHand = tempHand;
      }
      else
      {
        if (PlayerIn.playerHandTwo.Count > 0)
        {
          Console.WriteLine("Your first hand has Blackjack!");
        }
        else
        {
          Console.WriteLine("You have Blackjack!");
        }
      }

      //Process Hit or Stand logic for second hand
      if (PlayerIn.playerHandTwo.Count > 0)
      {
        if (PlayerIn.getValueOfHand(PlayerIn.playerHandTwo) < 21)
        {
          Console.WriteLine("");
          Console.WriteLine("Playing second hand...");
          Console.WriteLine("");

          HitOrStand(PlayerIn, PlayerIn.playerHandTwo, MainDeckIn, out tempHand, out MainDeckIn);

          PlayerIn.playerHandTwo = tempHand;
        }
        else
        {
          Console.WriteLine("Your second hand has blackjack!");
        }
      }

      //Pass updated data to the outputs
      PlayerOut = PlayerIn;
      MainDeckOut = MainDeckIn;
    }

    static void HitOrStand(Player CurrentPlayer, List<Card> PlayerHandIn, List<Card> DeckIn, out List<Card> PlayerHandOut, out List<Card> DeckOut)
    {
      if (PlayerHandIn.Count > 0)
      {
        //Begin allowing player to "hit" or "stand"
        var wantsToHit = true;

        //Loop while player wants to hit
        while (wantsToHit)
        {
          Console.WriteLine("Would you like to (HIT) or (STAND)?");

          var userResponse = Console.ReadLine().ToLower();

          while (userResponse != "hit" && userResponse != "stand")
          {
            Console.WriteLine("Please enter a valid response. You can (HIT) or (STAND).");

            userResponse = Console.ReadLine().ToLower();
          }

          //"Hit" logic
          if (userResponse == "hit")
          {
            //Add top card in deck to players hand
            PlayerHandIn.Add(DeckIn[0]);
            DeckIn.RemoveAt(0);

            Console.WriteLine("");

            //Display updated hand
            for (var i = 0; i < PlayerHandIn.Count; i++)
            {
              Console.WriteLine($"Player hand has a(n) {PlayerHandIn[i].DisplayCardName()}");
            }

            //Display updated hand total
            Console.WriteLine("");
            Console.WriteLine($"Player has a total of: {CurrentPlayer.getValueOfHand(PlayerHandIn)}");
            Console.WriteLine("");

            //If hand busts, kick player out of wantsToHit loop
            if (CurrentPlayer.getValueOfHand(PlayerHandIn) > 21)
            {
              wantsToHit = false;
            }
          }
          else if (userResponse == "stand")
          {
            wantsToHit = false;
          }
        }
      }

      //Pass updated hand and deck back out of method
      PlayerHandOut = PlayerHandIn;
      DeckOut = DeckIn;
    }
  }
}
