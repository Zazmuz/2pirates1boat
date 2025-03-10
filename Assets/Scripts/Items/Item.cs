using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Item Information")]
    public Sprite[] itemSprites;
    public string itemName;
    public ItemType itemType;
    public ActionType ActionType;
    public int durability;
}

public enum ItemType{
    Weapon, //swords, guns
    Tool, //hammers
    Resources,// planks and other stuff, more materials?
    Consumable //oranges, medicine?
}
public enum ActionType{
    Attack,
    Consume, // eat oranges to not get scurvy, medicine to regain hp.
    Interact //interacting with a zone
}