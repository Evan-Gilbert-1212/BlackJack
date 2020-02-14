using System.Collections.Generic;

namespace BlackJack
{
  public class Player
  {
    public int playerID { get; set; }

    public List<Card> playerHand { get; set; } = new List<Card>();

    public int getValueOfHand()
    {
      var handTotal = 0;

      for (var i = 0; i < playerHand.Count; i++)
      {
        handTotal += playerHand[i].GetCardValue();
      }

      return handTotal;
    }
  }
}
