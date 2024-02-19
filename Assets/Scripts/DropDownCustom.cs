using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class DropDownCustom : MonoBehaviour
{
    public event UnityAction<string> OnItemChanged;
    private Dropdown _thisDropDown;
    private int _currentSelectionNum;

    private readonly Dictionary<int, string> _items = new Dictionary<int, string>();

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        _thisDropDown = GetComponent<Dropdown>();
        _currentSelectionNum = _thisDropDown.value;
        _thisDropDown.onValueChanged.AddListener(OnValueChanged);
    }
  
//    private void OnEnable()
//    {
//       
//    }

//    private void OnDisable()
//    {
//        _thisDropDown.onValueChanged.RemoveAllListeners();
//    }


    public void SetValueWithoutNotify(string newValue)
    {
        int key = _items.FirstOrDefault(x => x.Value == newValue).Key;
        _thisDropDown.SetValueWithoutNotify(key);
    }

    private void OnValueChanged(int optionID)
    {
        if (_currentSelectionNum != optionID)
        {
            _currentSelectionNum = optionID;

            if (_items.TryGetValue(optionID, out var itemID))
            {
              
                OnItemChanged?.Invoke(itemID);
            }
        }
    }

    public void SetOptions(List<string> items)
    {
        _thisDropDown.ClearOptions();
        _items.Clear();
        var index = 0;
        var options = new List<Dropdown.OptionData>();

        foreach (string id in items)
        {
            var item = new Dropdown.OptionData {text = id};

            options.Add(item);
            _items[index] = id;
            index++;
        }
        _thisDropDown.options = options;
    }

}