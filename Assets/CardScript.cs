using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public CardScriptableObject template;

    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI text;
    public Image image;
    public CardType type;

    public Renderer[] renderers;
    public DeckScript owner;

    private bool hovered = false;
    public float positionDamp;
    public float rotationDamp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, owner.GetCardPosition(this, hovered), 1 - Mathf.Pow(1 - positionDamp * 3.0f, Time.deltaTime * 60));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, owner.GetCardRotation(this, hovered), Time.deltaTime * rotationDamp);
    }

    internal void setTemplate(CardScriptableObject template)
    {
        this.template = template;

        title.text= template.title;
        text.text = template.text;
        image.sprite = template.sprite;
        image.gameObject.SetActive(image.sprite != null);
        SetCardColor(GameMaster.GetColor((int)template.type));
    }

    void SetCardColor(Color color)
    {
        foreach (var r in renderers)
        {
            r.material.SetColor("_Color", color);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked card: " + gameObject.name);
        if (owner != null)
        {
            owner.CardClicked(this);
        } else
        {
            Debug.LogWarning("No owner for card: " + gameObject.name);
        }

    }

    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false;
    }
}
