using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject cardBase;
    public Color[] colors;
    public static GameMaster main;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        CardScriptableObject[] allCards = Resources.LoadAll<CardScriptableObject>("Cards");

        Debug.Log("Found " + allCards.Length + " cards");
        float i = 0;
        foreach (var c in allCards) {
            var o = CreateCard(c);
            o.gameObject.transform.position = new Vector3(0,0,i);

            i += 2.2f;
        }
    }

    Card CreateCard(CardScriptableObject template)
    {
        GameObject o = Instantiate(cardBase);
        Card c = o.GetComponent<Card>();
        c.setTemplate(template);
        return c;
    }

    internal static Color GetColor(CardType type)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Color GetColor(int index)
    {
        return main.colors[index];
    }
}
