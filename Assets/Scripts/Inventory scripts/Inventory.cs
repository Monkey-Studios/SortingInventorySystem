using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    //Used to trigger pick up event and add to the current player inventory aswell as updating and sorting inventory orders
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;
    //Constructing Inventory
    public Inventory()
    {
        //Creating item list within inventory
        itemList = new List<Item>();
        //Adding item for testing purposes
        AddItem(new Item(Item.ItemType.Coin, "coin", 1f));
        AddItem(new Item(Item.ItemType.HealthPotion, "healthPotion", 0.5f));
        AddItem(new Item(Item.ItemType.ManaPotion, "manaPotion", 0.8f));
        Debug.Log(itemList.Count);
        
    }
    //Adding an item to the inventory
    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    //Updates Item list order based on button input by player
    public void UpdateItem()
    {
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    //Funcionality for removing item from inventory
    public void RemoveItem(Item item)
    {
        Item itemInInventory = null;
        foreach (Item inventoryItem in itemList)
        {
            if(inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount -= item.amount;
                itemInInventory = inventoryItem;
            }
        }
        if(itemInInventory != null && itemInInventory.amount <= 0)
        {
            itemList.Remove(itemInInventory);
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    //Make item list visible to the UI script
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
