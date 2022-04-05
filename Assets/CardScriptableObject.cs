using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/Card")]
public class CardScriptableObject : ScriptableObject
{
    public string title { get { return this.name; } }
    [TextArea(15, 20)]
    public string text;
    public Sprite sprite;

    public CardType type;
    public int powerUsage = 0;
    public int powerGeneration = 0;
    public int happiness = 0;
    public int people = 0;
    public int housingNextToHappiness = 0;
    public int housingOnMapHappiness = 0;
    public int peopleOnMapHappiness = 0;
}

public enum CardType
{
    Industry = 1,
    Commercial = 3,
    Housing = 4
}