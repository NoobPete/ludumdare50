using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : DeckScript
{
    public TMPro.TextMeshProUGUI text;
    public static int locationCount = 0;
    public GameMaster gm;

    public Vector3 offset = new Vector3(0f, 2f, 0.3f);
    public Vector3 rotation = new Vector3(1f, 0f, 0f);

    private int[] _triggers;
    public int[] Triggers
    {
        set
        {
            _triggers = value;
            UpdateText(value);
            this.name = "Location " + locationCount++ + " - " + string.Join(", ", value);
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
        gm = GameMaster.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        gm.locationClicked(this);
    }

    public override Vector3 GetCardPosition(CardScript cs, bool hovered)
    {
        return offset;
    }

    public override Quaternion GetCardRotation(CardScript cs, bool hovered)
    {
        return this.transform.rotation * Quaternion.Euler(rotation);
    }
}
