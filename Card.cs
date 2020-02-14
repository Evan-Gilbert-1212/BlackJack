namespace BlackJack
{
  public class Card
  {
    //Rank of card
    public string CardRank { get; set; }

    //Suit of card
    public string CardSuit { get; set; }

    public int GetCardValue()
    {
      //Determine card Value
      //ADVENTURE: Consider Aces to be 1 or 11 (changes to 1 if it causes a bust with value of 11)
      if (CardRank == "Ace")
      {
        return 11;
      }
      else if (CardRank == "King" || CardRank == "Queen" || CardRank == "Jack")
      {
        return 10;
      }
      else
      {
        return ConvertNumberString(CardRank);
      }
    }

    public int ConvertNumberString(string NumberText)
    {
      if (NumberText == "Ten")
      {
        return 10;
      }
      else if (NumberText == "Nine")
      {
        return 9;
      }
      else if (NumberText == "Eight")
      {
        return 8;
      }
      else if (NumberText == "Seven")
      {
        return 7;
      }
      else if (NumberText == "Six")
      {
        return 6;
      }
      else if (NumberText == "Five")
      {
        return 5;
      }
      else if (NumberText == "Four")
      {
        return 4;
      }
      else if (NumberText == "Three")
      {
        return 3;
      }
      else if (NumberText == "Two")
      {
        return 2;
      }
      else
      {
        return 0;
      }
    }

    public string DisplayCardName()
    {
      return $"{CardRank} of {CardSuit}";
    }
  }
}
