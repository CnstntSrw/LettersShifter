using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]

public class PlaceholderHider : MonoBehaviour
{

    [SerializeField]
    private bool _isPlaceholderHideOnSelect;
    private TMP_InputField _InputField;

    private void Awake()
    {
        _InputField = GetComponent<TMP_InputField>();
        _InputField.onSelect.AddListener(OnInputFieldSelect);
        _InputField.onDeselect.AddListener(OnInputFieldDeselect);
    }

    private void OnInputFieldSelect(string arg0)
    {
        if (_isPlaceholderHideOnSelect == true)
        {
            _InputField.placeholder.gameObject.SetActive(false);
        }
    }
    private void OnInputFieldDeselect(string arg0)
    {
        if (_isPlaceholderHideOnSelect == true)
        {
            _InputField.placeholder.gameObject.SetActive(true);
        }
    }
}
