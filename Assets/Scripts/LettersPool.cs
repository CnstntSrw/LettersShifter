using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LettersPool : MonoBehaviour
{
    internal static LettersPool SharedInstance;
    [SerializeField]
    private LetterView _Prefab;
    [SerializeField]
    private int _AmountToPool;

    private List<LetterView> pooledLetters;
    private RectTransform _RectTransform;
    private void Awake()
    {
        SharedInstance = this;
        _RectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        pooledLetters = new List<LetterView>();
        LetterView tmp;
        for (int i = 0; i < _AmountToPool; i++)
        {
            tmp = Instantiate(_Prefab, _RectTransform, false);
            tmp.SetActive(false);
            pooledLetters.Add(tmp);
        }
    }
    internal LetterView GetPooledLetterView()
    {
        for (int i = 0; i < _AmountToPool; i++)
        {
            if (!pooledLetters[i].IsActive())
            {
                return pooledLetters[i];
            }
        }
        return null;
    }
}
