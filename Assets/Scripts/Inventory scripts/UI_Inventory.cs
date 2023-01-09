using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    //Getting reference of invetory
    private Inventory inventory;
    //Set and reference UI transforms
    private Transform itemSlot_Container;
    private Transform itemSlot_Template;
    private player_movement player;
    //Display items within inventory
    private void Awake()
    {
        itemSlot_Container = transform.Find("ItemSlot_Container");
        itemSlot_Template = itemSlot_Container.Find("ItemSlot_Template");
    }
    public void SetPlayer(player_movement player)
    {
        this.player = player;
    }
    //Recieve and set inventory
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        UpdateInventoryItems();
    }
    //Update inventory list
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        UpdateInventoryItems();
    }
    //Update inventory items
    private void UpdateInventoryItems()
    {
        foreach (Transform child in itemSlot_Container)
        {
            if (child == itemSlot_Template)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 110f;
        //Cycle through item list and checks to see the limit listed below is reached and stop rendering
        for (int i = 0; i < Mathf.Min(inventory.GetItemList().Count, 500); i++)
        {
            Item item = inventory.GetItemList()[i];
            RectTransform itemSlotRectTransform = Instantiate(itemSlot_Template, itemSlot_Container).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            //This will allow the player to drop the item when playing by using right click
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                //This will remove the item from the inventory and then drop it into the game world
                inventory.RemoveItem(item);
                WorldItem.DropItem(player.GetPosition(), item);
            };

            //Used to locate and identify item slots
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("ItemGraphic").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x > 8)
            {
                x = 0;
                y--;
            }
        }
    }
}
