using System;
using UnityEngine;

public class LettersGenerator
{
    public event Action<char[,]> OnLettersGenerated;

    internal LettersGenerator(GameView view)
    {
        view.OnGenerateClick += GenerateLetters;
    }

    internal static Color GetRandomColor()
    {
        return new Color(
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f));
    }

    private void GenerateLetters(int width, int height)
    {
        char[,] generatedLetters = new char[width, height];
        System.Random rnd = new System.Random();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                char randomChar = (char)rnd.Next('A', 'Z');
                generatedLetters[i, j] = randomChar;
            }
        }

        OnLettersGenerated?.Invoke(generatedLetters);
    }
}
