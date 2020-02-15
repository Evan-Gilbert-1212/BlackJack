namespace BlackJack
{
  public class Card
  {
    //Rank of card
    public string CardRank { get; set; }

    //Suit of card
    public string CardSuit { get; set; }

    //Value of card
    public int CardValue { get; set; }

    public string DisplayCardName()
    {
      return $"{CardRank} of {CardSuit}";
    }

    public void SetCardValue()
    {
      switch (CardRank)
      {
        case "Ace":
          CardValue = 11;
          break;
        case "King":
          CardValue = 10;
          break;
        case "Queen":
          CardValue = 10;
          break;
        case "Jack":
          CardValue = 10;
          break;
        case "Ten":
          CardValue = 10;
          break;
        case "Nine":
          CardValue = 9;
          break;
        case "Eight":
          CardValue = 8;
          break;
        case "Seven":
          CardValue = 7;
          break;
        case "Six":
          CardValue = 6;
          break;
        case "Five":
          CardValue = 5;
          break;
        case "Four":
          CardValue = 4;
          break;
        case "Three":
          CardValue = 3;
          break;
        case "Two":
          CardValue = 2;
          break;
        default:
          CardValue = 0;
          break;
      }
    }
  }
}
