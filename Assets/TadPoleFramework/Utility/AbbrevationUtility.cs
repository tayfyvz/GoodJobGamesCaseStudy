using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TadPoleFramework.Utility
{
    public class AbbrevationUtility
    {
        private static readonly SortedDictionary<int, string> abbrevations = new SortedDictionary<int, string>
        {
            {1000, "K"}, {1000000, "M"}, {1000000000, "B"}
        };

        public static string AbbreviateNumber(float number)
        {
            
            for (int i = abbrevations.Count - 1; i >= 0; i--)
            {
                KeyValuePair<int, string> pair = abbrevations.ElementAt(i);
                if (Mathf.Abs(number) >= pair.Key)
                {
                    float rest = number % pair.Key;
                    float k = (number - rest) / pair.Key;
                    float f = Mathf.Round(rest / (pair.Key / 10));
                    // float f = (rest / (pair.Key / 10));
                    string roundedNumber;

                    // int d = (int) f;
                    
                    // if (f == 0)
                    // {
                    //     roundedNumber = k.ToString();
                    // }
                    // else
                    // {
                    //     if (f == 10)
                    //     {
                    //         f = 9;
                    //     }
                    //
                    //     roundedNumber = k.ToString() + "." + f.ToString();
                    // }
                    if (f == 10)
                    {
                        f = 9;
                    }
                    roundedNumber = k.ToString() + "." + f.ToString();
                    // Debug.Log($"MONEY COUNT : {number} | ABV : {roundedNumber + pair.Value}");
                    
                    return roundedNumber + pair.Value;
                }
            }
            // Debug.Log($"MONEY COUNT : {number} | ABV : {number.ToString()}");
            return number.ToString();
        }
    }
}