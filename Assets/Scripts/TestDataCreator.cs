using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDataCreator : MonoBehaviour
{

    private string[] arr = { "acetaldehyde","akroleyin","benzol" , "carbonyls", "chromium"};
    private int elementsCount=150;

    private void Start()
    {
        string s = "";

        for (int i = 0; i < elementsCount; i++)
        {
            int n = Random.Range(0, arr.Length);
            s += arr[n];
            s += "\n";
        }

        Debug.Log(s);
    }

}
