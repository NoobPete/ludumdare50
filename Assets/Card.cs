using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardScriptableObject template;

    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI text;
    public Image image;
    public CardType type;

    public Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void setTemplate(CardScriptableObject template)
    {
        this.template = template;

        title.text= template.title;
        text.text = template.text;
        image.sprite = template.sprite;
        image.gameObject.SetActive(image.sprite != null);


        switch(template.type)
        {
            case CardType.Science:
                SetCardColor(Color.blue);
                break;
            case CardType.Tech:
                SetCardColor(Color.green);
                break;
        }
    }

    void SetCardColor(Color color)
    {
        foreach (var r in renderers)
        {
            r.material.SetColor("_Color", color);
        }
    }
}
