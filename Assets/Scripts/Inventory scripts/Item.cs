using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    //Declare item type
    public ItemType itemType;
    //Declare total item amount
    public int amount;
    //Declaring Item weights
    public float weight = 0;
    //Declaring Item Names
    public string name;
    //Declare Item list
    public enum ItemType
    {
        HealthPotion,
        ManaPotion,
        Coin,
        Jar,
        LifeCrystal,
    }
    //Function used to get the sprites of all the objects
    public Sprite GetSprite()
    {
        //Switch statement matching instance to item type
        switch (itemType)
        {
            default:
            case ItemType.Coin:         return ItemAssets.Instance.coinSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion:   return ItemAssets.Instance.manaPotionSprite;
            case ItemType.LifeCrystal:  return ItemAssets.Instance.lifeCrystalSprite;
            case ItemType.Jar:          return ItemAssets.Instance.jarSprite;
        }
    }
    public Item(ItemType itemType, string name, float weight)
    {
        amount = 1;
        this.name = name;
        this.weight = weight;
        this.itemType = itemType;
    }
}
