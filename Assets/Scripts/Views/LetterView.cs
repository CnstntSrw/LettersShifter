using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterView : MonoBehaviour
{
    [SerializeField]
    internal TMP_Text _Text;

    internal float AnimationTime = 2f;

    [SerializeField]
    private RectTransform RectTransform;

    private MaterialPropertyBlock _PropertyBlock;

    internal void Init(Vector3 position, Vector2 size, char text)
    {
        SetPosition(position);
        SetSize(size);
        SetText(text);
        SetRandomColor();
        SetActive(true);
        TMP_UpdateManager.UnRegisterTextElementForRebuild(_Text);
    }

    private void SetRandomColor()
    {
        _Text.faceColor = LettersGenerator.GetRandomColor();
    }

    private void SetSize(Vector2 size)
    {
        RectTransform.sizeDelta = size;
    }

    private void SetText(char text)
    {
        _Text.text = text.ToString();
    }
    internal Vector3 GetPosition()
    {
        return RectTransform.position;
    }

    internal void SetActive(bool v)
    {
        _Text.enabled = v;
    }

    internal bool IsActive()
    {
        return _Text.enabled;
    }

    internal void SetPosition(Vector3 position)
    {
        RectTransform.localPosition = position;
    }

    internal void MoveTo(Vector3 end)
    {
        StartCoroutine(LerpPos(RectTransform.position, end));
    }

    private IEnumerator LerpPos(Vector3 start, Vector3 end)
    {
        float t = 0.0f;
        while (t < AnimationTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, t / AnimationTime);
            yield return null;
        }
    }
}
