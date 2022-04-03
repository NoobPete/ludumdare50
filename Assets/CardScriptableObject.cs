using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/Card")]
public class CardScriptableObject : ScriptableObject
{
    public string title { get { return this.name; } }
    [TextArea(15, 20)]
    public string text;
    public Sprite sprite;

    public CardType type;
}

public enum CardType
{
    Industry = 1,
    Commercial = 3,
    Housing = 4
}