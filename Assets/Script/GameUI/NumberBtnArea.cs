using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberBtnArea : MonoBehaviour {
    
    public List<NumberBtn> btnAreas = new List<NumberBtn>();

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void enableAllBtns()
    {
        for (int i = 0; i < btnAreas.Count; i++)
        {
            btnAreas[i].setBtnEnable(true);
        }
    }

    public void refreshNumbers(List<int> numbers)
    {
        if (numbers == null || numbers.Count != btnAreas.Count)
        {
            Debug.LogError("NumberBtnArea.refreshNumbers error. error param");
            return;
        }
        for (int i = 0; i < numbers.Count; i++)
        {
            btnAreas[i].setNumber(numbers[i]);
        }
    }
}
