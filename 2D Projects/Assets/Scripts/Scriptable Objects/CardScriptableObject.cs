using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Template")]
public class CardScriptableObject : ScriptableObject
{
    //Variables
    public new string name;
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int hp;
}
