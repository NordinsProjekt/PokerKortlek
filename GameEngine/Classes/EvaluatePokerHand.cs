using GameEngine.DTO;
using DataLayer.DTO;
using GameEngine.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class EvaluatePokerHand
    {
        private bool IsFlush(List<CardRecord> list)
        {
            var color = list.GroupBy(x => x.Color).Select(h => new { Key = h.Key, Count = h.Count() }).OrderByDescending(x => x.Count).ToList();
            if (color[0].Count == 5)
                return true;
            return false;
        }

        private bool IsStraight(List<CardRecord> list)
        {
            int counter = 0;
            int max = 4;
            var straight = list.OrderBy(x => x.Value).ToList();
            if (straight[4].Value == 14 && straight[1].Value == 2)
            {
                counter = 1;
                max = 3;
            }
            for (int i = counter; i < max; i++)
            {
                int card1 = straight[i].Value;
                int card2 = straight[i + 1].Value;
                if (card1 + 1 != card2)
                    return false;
            }
            return true;
        }

        private string ConvertCheckHandCount(int resultat)
        {
            switch (resultat)
            {
                case 1:
                    return "Highcard";
                case 2:
                    return "1 pair";
                case 3:
                    return "3 of a kind";
                case 4:
                    return "4 of a kind";
                default:
                    return "";
            }
        }

        public EvaluateCardResult EvaluateHand(List<CardRecord> list)
        {
            //Om man skickar in grupp index 0 i funktionen så kan man hitta HighCard eller par,triss,fyrtal
            //Om man skickar in grupp index 1 i samma funktion så kan man hitta 2 par och kåk
            if (list.Count < 5 || list.Count > 5)
                throw new GameException("Can only Evaluate Hand if it got 5 cards in it");
            var result = list.ToList().GroupBy(h => h.Value).Select(h => new { Key = h.Key, Count = h.Count() }).OrderByDescending(x => x.Count).ToList();
            var orderedList = list.OrderBy(x => x.Value).ToList();
            List<CardRecord> tiebreaker = new List<CardRecord>();
            //Sorteras på högsta counten
            switch (ConvertCheckHandCount(result[0].Count))
            {
                case "Highcard": //Kollar efter stege, färg eller färgstege
                    if (IsFlush(list))
                    {
                        if (IsStraight(list))
                        {
                            if (orderedList[0].Value == 10)
                                return new EvaluateCardResult("Royal Straight Flush",900,0);
                            else
                            {
                                if (orderedList[4].Value == 14)
                                    return new EvaluateCardResult("Straight Flush", 800, 1);
                                else
                                    return new EvaluateCardResult("Straight Flush", 800, orderedList[4].Value);
                            }
                        }
                        else
                            return new EvaluateCardResult("Flush", 500,orderedList[4].Value);
                    }
                    else
                    {
                        if (IsStraight(list))
                            return new EvaluateCardResult("Straight", 400,orderedList[4].Value);
                        else
                            return new EvaluateCardResult("High Card", 0 + orderedList[4].Value, orderedList[3].Value);
                    }
                case "1 pair":
                    //Kolla efter ett annat par
                    if (ConvertCheckHandCount(result[1].Count) == "1 pair")
                    {
                        int score = result[0].Key;
                        if (score <= result[1].Key)
                            score = result[1].Key;
                        tiebreaker = list.OrderByDescending(x => x.Value).Where(x => x.Value != result[0].Key || x.Value != result[1].Key).ToList();
                        return new EvaluateCardResult($"2 pairs of {result[0].Key} and {result[1].Key}", 200 + score, tiebreaker[0].Value);
                    }
                    else
                    {
                        tiebreaker = list.OrderByDescending(x => x.Value).Where(x => x.Value != result[0].Key).ToList();
                        return new EvaluateCardResult($"1 pair of {result[0].Key}", 100 + result[0].Key, tiebreaker[0].Value);
                    }
                case "3 of a kind":
                    if (ConvertCheckHandCount(result[1].Count) == "1 pair") //Kolla efter kåk
                        return new EvaluateCardResult("Full House", 600 + result[0].Key, result[1].Key);
                    tiebreaker = list.OrderByDescending(x => x.Value).Where(x => x.Value != result[0].Key).ToList();
                    return new EvaluateCardResult($"3 of a kind of {result[0].Key}",300+ result[0].Key, tiebreaker[0].Value);
                case "4 of a kind":
                    //Ingen mer kontroll behövs men skicka med Highcard
                    var highcard = list.Where(x => x.Value != result[0].Key).ToList();
                    return new EvaluateCardResult($"4 of a kind of {result[0].Key} and with {highcard.First()}",700+result[0].Key,highcard.First().Value);
                default:
                    return new EvaluateCardResult("Error", 0,0);
            }
        }
    }
}
