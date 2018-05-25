using System.Collections.Generic;
using UnityEngine;

class GameUtils
{
    public static List<int> genDiffIntNumbers(int numStart, int numEnd, int numCount)
    {
        List<int> numberList = new List<int>();

        while (numberList.Count < numCount)
        {
            int random = (int)Random.Range(numStart, numEnd);

            bool hasSameNumber = false;
            for (int i = 0; i < numberList.Count; i++)
            {
                if (random == numberList[i]) //有相同的
                {
                    hasSameNumber = true;
                    break;
                }
            }
            if (hasSameNumber)
            {
                continue;
            }
            else
            {
                numberList.Add(random);
            }
        }

        return numberList;
    }
}
