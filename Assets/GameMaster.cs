using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameMaster : MonoBehaviour
{
    public GameObject cardBase;
    public GameObject locationTemplate;
    public GameObject dice;
    public Transform diceSpawnLocation;
    public Transform diceIdleLocation;
    public Vector3 diceIdleSpace;
    public HandScript hand;
    public Color[] colors;
    public static GameMaster main;
    public int pollution = 0;
    public int basePollution = 5;
    public TMPro.TextMeshProUGUI pollutiontext;
    public int happiness = 0;
    public TMPro.TextMeshProUGUI happinesstext;
    public Transform endgame;

    public float locationSpaceHor;
    public float locationSpaceVer;
    public float diceSpread;
    public int[] locationLookup;
    public List<DiceScript> liveDices = new List<DiceScript>();
    public List<LocationScript> locations = new List<LocationScript>();

    public DiceState rollState = DiceState.WAITING;
    public CardScriptableObject[] allCards;

    private int cardNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        allCards = Resources.LoadAll<CardScriptableObject>("Cards");
        Debug.Log("Found " + allCards.Length + " cards");
        createNewHand();

        // Create table
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
                locations.Add(l);
            }
        }
    }

    private void createNewHand()
    {
        clearHand();

        for (int i = 0; i < 4; i++)
        {
            var o = CreateCard(allCards[Random.Range(0, allCards.Length - 1)]);
            o.gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }

    private void clearHand()
    {
        hand.selected = null;
        foreach (var c in hand.list)
        {
            Destroy(c.gameObject);
        }
        hand.list.Clear();
    }

    public void locationClicked(LocationScript locationScript)
    {
        if (locationScript.list.Count > 0)
        {
            var c = locationScript.list[0];
            locationScript.RemoveCard(c);
            Destroy(c.gameObject);
        }

        CardScript card = hand.selected;
        if (card != null && rollState == DiceState.WAITING)
        {
            hand.RemoveCard(card);
            hand.selected = null;
            locationScript.AddCard(card);
        }
    }

    CardScript CreateCard(CardScriptableObject template)
    {
        GameObject o = Instantiate(cardBase);
        CardScript c = o.GetComponent<CardScript>();
        o.name = cardBase.name + "(" + cardNumber++ + ")";
        c.setTemplate(template);
        hand.AddCard(c);
        return c;
    }

    // Update is called once per frame
    void Update()
    {
        if (pollution > 250)
        {
            rollState = DiceState.ENDED;
        }

        if (rollState == DiceState.ENDED)
        {
            endgame.gameObject.SetActive(true);
        }

        if (rollState == DiceState.ROLLING)
        {
            bool allDiceStopped = true;

            foreach (var d in liveDices)
            {
                if (!d.Stopped())
                {
                    allDiceStopped = false;
                    break;
                }
            }

            if (allDiceStopped)
            {
                rollState = DiceState.WAITING;
                createNewHand();
                foreach (var d in liveDices)
                {
                    var rb = d.GetComponent<Rigidbody>();
                    rb.freezeRotation = true;
                    rb.isKinematic = true;
                }

                for (int i = 0; i < liveDices.Count; i += 2)
                {
                    int sum = liveDices[i].side + liveDices[i + 1].side;
                    Debug.Log("Dice pair " + i / 2 + " rolled a " + sum);
                    foreach (var l in locations)
                    {
                        if (Array.Exists(l.Triggers, element => element == sum) && l.list.Count > 0)
                        {
                            var c = l.list[0];
                            l.RemoveCard(c);
                            Destroy(c.gameObject);
                        }
                    }
                }
            }
        }
        else if (rollState == DiceState.WAITING)
        {
            for (var i = 0; i < liveDices.Count; i++)
            {
                var d = liveDices[i];
                d.transform.position = Vector3.Lerp(d.transform.position, diceIdleLocation.position + diceIdleSpace * (i % 2) + new Vector3(0, 0, -1) * (i / 2), 1 - Mathf.Pow(1 - 0.2f * 3.0f, Time.deltaTime * 60));
            }
        }
        pollutiontext.text = "Polution: " + pollution;
        happinesstext.text = "Happiness: " + happiness;
    }

    public void roll()
    {
        if (rollState != DiceState.WAITING)
        {
            return;
        }

        foreach (var d in liveDices)
        {
            Destroy(d.gameObject);
        }
        liveDices.Clear();

        clearHand();

        pollution += basePollution;


        int powerConsumed = 0;
        int powerProduced = 0;
        int peopleCount = 0;
        int housecount = 0;

        int housemultipliercount = 0;
        int peoplemultipliercount = 0;
        foreach (var locationScript in locations)
        {
            if (locationScript.list.Count > 0)
            {
                var c = locationScript.list[0];
                powerConsumed += c.template.people + c.template.powerUsage;
                powerProduced += c.template.powerGeneration;
                happiness += c.template.happiness;
                peopleCount += c.template.people;
                if (c.type == CardType.Housing)
                {
                    housecount += 1;
                }
                housemultipliercount += c.template.housingNextToHappiness;
                peoplemultipliercount += c.template.peopleOnMapHappiness;
            }
        }

        happiness += housecount * housemultipliercount + peoplemultipliercount + peopleCount;

        if (powerConsumed > powerProduced)
        {
            pollution += (powerConsumed - powerProduced) * 4;
        }

        pollution += basePollution;

        rollState = DiceState.ROLLING;

        for (int i = 0; i < 1 + Mathf.Min(4, pollution / 50); i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject o = Instantiate(dice);
                o.transform.position = diceSpawnLocation.position + new Vector3(Random.Range(-diceSpread, diceSpread), Random.Range(-diceSpread, diceSpread), Random.Range(-diceSpread, diceSpread));
                DiceScript ds = o.GetComponent<DiceScript>();
                liveDices.Add(ds);
                Renderer r = o.GetComponent<Renderer>();
                r.material.SetColor("_Color", colors[i]);
            }
        }
    }

    public static Color GetColor(int index)
    {
        return main.colors[index];
    }

    public enum DiceState
    {
        WAITING,
        ROLLING,
        ENDED
    }
}
