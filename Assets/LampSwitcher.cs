using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LampSwitcher : MonoBehaviour, IClickable
{
   [Serializable]
   public struct MyStruct
   {
       public GameObject[] Objects;
   }

   public GameObject[]AllLamps;
   public Text _text;
    public MyStruct[] _lamps;
    private int _currentNum;

    private void Awake()
    {
        _currentNum = -1;
        gameObject.layer = LayerMask.NameToLayer("UI");
        foreach (var VARIABLE in AllLamps)
        {
            VARIABLE.SetActive(false);
        }

        _text.text = "";
    }

    public void Click()
    {
        Debug.Log("INTERACT");
        if (++_currentNum >= _lamps.Length) _currentNum = 0;
        foreach (var VARIABLE in AllLamps)
        {
            VARIABLE.SetActive(false);
        }

        _text.text = $"Variant {_currentNum+1}";
        for (int i = 0; i < _lamps.Length; i++)
        {
            if (i == _currentNum)
            for (int j = 0; j < _lamps[i].Objects.Length; j++)
            {
                _lamps[i].Objects[j].SetActive(true);
            }
        }
    }


#if UNITY_EDITOR
    //-------------------------------------------------------------------------
    [UnityEditor.CustomEditor(typeof(LampSwitcher))]
    public class UIElementEditor : UnityEditor.Editor
    {
        //-------------------------------------------------
        // Custom Inspector GUI allows us to click from within the UI
        //-------------------------------------------------
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LampSwitcher uiElement = (LampSwitcher)target;
            if (GUILayout.Button("Click"))
            {
                uiElement.Click();
            }
        }
    }
#endif
}
