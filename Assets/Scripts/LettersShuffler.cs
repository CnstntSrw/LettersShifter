using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LettersShuffler
{
    internal static void ShuffleLetters(List<LetterView> letters)
    {
        Shuffle(letters);
    }
    private static void Shuffle(List<LetterView> list)
    {
        list = list.OrderBy(x => System.Guid.NewGuid()).ToList();
        IterateAndSwap(list);
    }

    private static void IterateAndSwap(List<LetterView> list)
    {
        if (list.Count % 2 == 0)
        {
            for (int i = 0; i < list.Count - 1; i += 2)
            {
                Swap(list[i], list[i + 1]);
            }
        }
        else
        {
            list.First().MoveTo(list.Last().GetPosition());
            for (int i = 1; i < list.Count; i++)
            {
                list[i].MoveTo(list[i - 1].GetPosition());
            }
        }
    }

    private static void Swap(LetterView x1, LetterView x2)
    {
        Vector3 tmp = x1.GetPosition();
        x1.MoveTo(x2.GetPosition());
        x2.MoveTo(tmp);
    }
}
