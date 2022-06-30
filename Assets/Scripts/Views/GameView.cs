using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameView : MonoBehaviour
{
    public event Action<int, int> OnGenerateClick;

    private LettersGenerator _Generator;
    [SerializeField]
    private Button _Generate;
    [SerializeField]
    private Button _Shuffle;
    [SerializeField]
    private TMP_InputField _WidthInput;
    [SerializeField]
    private TMP_InputField _HeightInput;
    [SerializeField]
    private Canvas _LetterCanvas;
    [SerializeField]
    private RectTransform _Parent;
    [SerializeField]
    private LetterView _LetterView;
    [SerializeField]
    private int _MaxSideSize = 50;

    private List<LetterView> _LettersViews = new List<LetterView>();

    private void Awake()
    {
        _Generator = new LettersGenerator(this);
        _Generate.onClick.AddListener(OnGenerate);
        _Shuffle.onClick.AddListener(() => 
        { 
            LettersShuffler.ShuffleLetters(_LettersViews);
            StartCoroutine(RevertControlsInteraction());
        });
        _Generator.OnLettersGenerated += ShowLetters;
        InitValidators();
    }

    private void InitValidators()
    {
        _WidthInput.onValueChanged.AddListener((string value) =>
        {
            if (int.TryParse(value, out int intValue) && intValue > _MaxSideSize)
            {
                _WidthInput.SetTextWithoutNotify(_MaxSideSize.ToString());
            }
        });
        _HeightInput.onValueChanged.AddListener((string value) =>
        {
            if (int.TryParse(value, out int intValue) && intValue > _MaxSideSize)
            {
                _HeightInput.SetTextWithoutNotify(_MaxSideSize.ToString());
            }
        });
    }

    private void ShowLetters(char[,] letters)
    {
        _LettersViews.Clear();

        int width = letters.GetLength(0);
        int height = letters.GetLength(1);

        for (int i = 0; i < width ; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var letterView = LettersPool.SharedInstance.GetPooledLetterView();
                letterView.Init(new Vector3(i * _Parent.sizeDelta.x / width, j * -_Parent.sizeDelta.y / height, 0), new Vector2(_Parent.sizeDelta.x / width, _Parent.sizeDelta.y / height), letters[i, j]);
                _LettersViews.Add(letterView);
            }
        }
        _LetterCanvas.enabled = true;
    }

    private void OnGenerate()
    {
        DisableObjects();

        if (int.TryParse(_WidthInput.text, out int width) && int.TryParse(_HeightInput.text, out int height))
        {
            OnGenerateClick?.Invoke(width, height);
        }
    }

    private void DisableObjects()
    {
        foreach (var view in _LettersViews)
        {
            view.SetActive(false);
        }
        _LetterCanvas.enabled = false;
    }
    private IEnumerator RevertControlsInteraction()
    {
        _WidthInput.interactable = !_WidthInput.interactable;
        _HeightInput.interactable = !_HeightInput.interactable;
        _Generate.interactable = !_Generate.interactable;
        _Shuffle.interactable = !_Shuffle.interactable;
        yield return new WaitForSeconds(_LetterView.AnimationTime);
        _WidthInput.interactable = !_WidthInput.interactable;
        _HeightInput.interactable = !_HeightInput.interactable;
        _Generate.interactable = !_Generate.interactable;
        _Shuffle.interactable = !_Shuffle.interactable;
    }
}
