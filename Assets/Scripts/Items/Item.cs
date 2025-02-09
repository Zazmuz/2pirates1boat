using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Item Information")]
    public Sprite itemSprite;
    public ItemType itemType;
    public ActionType ActionType;
}

public enum ItemType{
    Weapon, //swords, guns
    Tool, //hammers
    Resources,// planks and other stuff, more materials?
    Food //oranges, medicine?
}
public enum ActionType{
    Attack,
    Consume, // eat oranges to not get scurvy, medicine to regain hp.
    Interact //interacting with a zone
}