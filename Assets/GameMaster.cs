using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject cardBase;
    public GameObject locationTemplate;
    public Color[] colors;
    public static GameMaster main;

    public float locationSpaceHor;
    public float locationSpaceVer;
    public int[] locationLookup;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        CardScriptableObject[] allCards = Resources.LoadAll<CardScriptableObject>("Cards");

        Debug.Log("Found " + allCards.Length + " cards");
        float i = 0;
        foreach (var c in allCards)
        {
            var o = CreateCard(c);
            o.gameObject.transform.position = new Vector3(0, 0, i);

            i += 2.2f;
        }

        for (int hor = 0; hor < 4; hor++)
        {
            for (int ver = 0; ver < 3; ver++)
            {
                GameObject o = Instantiate(locationTemplate);
                LocationScript l = o.GetComponent<LocationScript>();
                int trigger = hor + (4 * (ver));
                trigger = locationLookup[trigger];

                if (trigger == 2 || trigger == 13)
                {
                    int[] numbers = { 2, 12 };
                    l.Triggers = numbers;
                }
                else
                {
                    int[] numbers = { trigger };
                    l.Triggers = numbers;
                }
                o.gameObject.transform.position = new Vector3(hor * locationSpaceHor, 0, ver * locationSpaceVer);
            }
        }
    }

    CardScript CreateCard(CardScriptableObject template)
    {
        GameObject o = Instantiate(cardBase);
        CardScript c = o.GetComponent<CardScript>();
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
