using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/Card")]
public class CardScriptableObject : ScriptableObject
{
    public string title;
    public string text;
    public Sprite sprite;

    public CardType type;

}

public enum CardType
{
    Tech,
    Science
}