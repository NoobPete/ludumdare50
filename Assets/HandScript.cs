using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : DeckScript
{
    public Vector3 offset = new Vector3(0f, 2f, 0.3f);
    public Vector3 offsetHover = new Vector3(1f, 0f, 0f);
    public CardScript selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selected = null;
        }
    }

    public override Vector3 GetCardPosition(CardScript cs, bool hovered)
    {
        int index = list.IndexOf(cs);
        return index * offset - offset * 0.5f * list.Count + (hovered || cs == selected ? offsetHover : Vector3.zero);
    }

    public override Quaternion GetCardRotation(CardScript cs, bool hovered)
    {
        float middleOffset = 0.5f * list.Count;
        int index = list.IndexOf(cs);
        return this.transform.rotation * Quaternion.Euler(0, 3 * (index-middleOffset) , 0); ;
    }

    public override void CardClicked(CardScript cs)
    {
        selected = cs;
    }
}
