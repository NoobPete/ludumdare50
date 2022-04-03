using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public List<CardScript> list;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard(CardScript cs)
    {
        list.Add(cs);
        cs.transform.parent = this.transform;
        cs.owner = this;
    }

    public void RemoveCard(CardScript cs)
    {
        list.Remove(cs);
        cs.transform.parent = null;
        cs.owner = null;
    }

    public virtual void CardClicked(CardScript cs)
    {

    }

    public virtual Vector3 GetCardPosition(CardScript cs, bool hovered)
    {
        return Vector3.zero;
    }

    public virtual Quaternion GetCardRotation(CardScript cs, bool hovered)
    {
        return Quaternion.identity;
    }
}
