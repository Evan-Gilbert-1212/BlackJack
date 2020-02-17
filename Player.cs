using System.Collections.Generic;

namespace BlackJack
{
  public class Player
  {
    public List<Card> playerHand { get; set; } = new List<Card>();

    public List<Card> playerHandTwo { get; set; } = new List<Card>();

    public int getValueOfHand(List<Card> HandToEvaluate)
    {
      var handTotal = 0;

      for (var i = 0; i < HandToEvaluate.Count; i++)
      {
        handTotal += HandToEvaluate[i].CardValue;
      }

      //ADVENTURE: Allow Aces to be 1 or 11
      //Reduce one at a time to try and get as close to 21 as possible
      while (handTotal > 21 && HasAcesToReduce(HandToEvaluate))
      {
        HandToEvaluate = ReduceNextAce(HandToEvaluate);

        handTotal = 0;

        for (var j = 0; j < HandToEvaluate.Count; j++)
        {
          handTotal += HandToEvaluate[j].CardValue;
        }
      }

      return handTotal;
    }

    public bool HasAcesToReduce(List<Card> HandToEvaluate)
    {
      var foundAceToReduce = false;

      for (var i = 0; i < HandToEvaluate.Count; i++)
      {
        if (HandToEvaluate[i].CardValue == 11)
        {
          foundAceToReduce = true;
        }
      }

      return foundAceToReduce;
    }

    public List<Card> ReduceNextAce(List<Card> HandToEvaluate)
    {
      for (var i = 0; i < HandToEvaluate.Count; i++)
      {
        if (HandToEvaluate[i].CardValue == 11)
        {
          HandToEvaluate[i].CardValue = 1;

          break;
        }
      }

      return HandToEvaluate;
    }

    public List<Card> ResetAllAces(List<Card> HandToEvaluate)
    {
      for (var i = 0; i < HandToEvaluate.Count; i++)
      {
        if (HandToEvaluate[i].CardValue == 1)
        {
          HandToEvaluate[i].CardValue = 11;
        }
      }

      return HandToEvaluate;
    }
  }
}
