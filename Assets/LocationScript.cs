using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    private int[] _triggers;
    public int[] Triggers
    {
        set
        {
            _triggers = value;
            UpdateText(value);
        }
        get { return _triggers; }
    }

    private void UpdateText(int[] value)
    {
        if (value.Length == 0)
        {
            text.text = "";
        }
        else if (value.Length == 1)
        {
            text.text = "" + value[0];
        } else if (value.Length == 2)
        {
            text.text = "" + value[0] + "&" + value[1];
        } else
        {
            throw new NotImplementedException();
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
