using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    //Variables
    public CardScriptableObject card;

    public Text cardNameText;
    public Text cardDescriptionText;

    public Image cardArtworkSprite;

    public Text cardManaText;
    public Text cardHpText;

    // Start is called before the first frame update
    void Start()
    {
        cardNameText.text = card.name;
        cardDescriptionText.text = card.description;

        cardArtworkSprite.sprite = card.artwork;

        cardManaText.text = card.manaCost.ToString();
        cardHpText.text = card.hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
