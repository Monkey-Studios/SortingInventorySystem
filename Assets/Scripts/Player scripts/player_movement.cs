using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class player_movement : MonoBehaviour
{
    //This is used to switch between the two different forms of search and sorting methods
    public bool Task1 = true;
    //Public reference to the text box for searching
    public Text search;
    //Calling upon search/sort method for button onClick functionality
    InventorySearchSort searchSort;
    //Setting up player ridgid body
    Rigidbody2D rb;
    //Setting player speed
    int speed = 4;
    //Reference to inventory
    private Inventory inventory;
    //Setting a serializable for Inventory UI
    [SerializeField] private UI_Inventory uiInventory;
    void Start()
    {
        //Call upon ridgid body
        rb = GetComponent<Rigidbody2D>();
        //Gives value to the new variable created for the onClick
        searchSort = GetComponent<InventorySearchSort>();
        //Initialise inventory
        inventory = new Inventory();
        //Setting Inventory UI
        uiInventory.SetInventory(inventory);
        //
        uiInventory.SetPlayer(this);
        //Testing if items spawn within the game world
        WorldItem.SpawnWorldItem(new Vector3(-3, -4), new Item(Item.ItemType.Coin, "coin", 1f));
        WorldItem.SpawnWorldItem(new Vector3(-4, -4), new Item(Item.ItemType.HealthPotion, "healthPotion", 0.5f));
        WorldItem.SpawnWorldItem(new Vector3(-5, -4), new Item(Item.ItemType.ManaPotion, "manaPotion", 0.8f));
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Checks for world items collider to see if the player is wanting to pick up the item
        WorldItem worldItem = collider.GetComponent<WorldItem>();
        if (worldItem != null)
        {
            //This adds the item to the player inventory and destroys the game world object
            inventory.AddItem(worldItem.GetItem());
            worldItem.DestroySelf();
        }
    }

    //Call upon the Move function
    void Update()
    {
        Move();
    }

    //New movement function that handels speed, position and look
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    //Gets the player X and Y position used to drop items from the inventory
    public Vector3 GetPosition()
    {
        return(this.transform.position);
    }
    //Used for the sorting by weight and name functions
    public void onClick(bool sortByType)
    {
        if(Task1 == true)
        {
            searchSort.Bubble(ref inventory.itemList, sortByType);
        }
        else
        {
            searchSort.Merge(ref inventory.itemList, sortByType);
        }
        //Starting timer to test efficency of the algorithm
        SearchSortTimer.EndTimer();
        inventory.UpdateItem();
    }
    //Used for the search bar function
    public void onClickSearch()
    {
        //Links the player search input to the text element
        string playerSearch = search.text;
        int itemPos = -1;
        if(Task1 == true)
        {
            //The system will then use the linear search to find the searched item in the inventory and if it doesnt equal -1 it returns that item
            itemPos = searchSort.Linear(ref inventory.itemList, playerSearch);
        }
        else
        {
            //If the system is set to Task 2 the system will then use the binary search to find the searched item in the inventory and if it doesnt equal -1 it returns that item
            itemPos = searchSort.Binary(ref inventory.itemList, playerSearch);
        }
        if(itemPos != -1)
        {
            inventory.itemList.RemoveAt(itemPos);
            inventory.UpdateItem();
            //Starting timer to test efficency of the algorithm
            SearchSortTimer.EndTimer();
        }
    }
    //10000 item for loop
    public void onClickAddThousand()
    {
        for (int i = 0; i < 100; i++)
        {
            int RandomItem = UnityEngine.Random.Range(0, 5);
            switch (RandomItem)
            {
                case 0:
                    inventory.AddItem(new Item(Item.ItemType.Coin, "coin", 1f));
                    break;
                case 1:
                    inventory.AddItem(new Item(Item.ItemType.HealthPotion, "healthPotion", 0.5f));
                    break;
                case 2:
                    inventory.AddItem(new Item(Item.ItemType.ManaPotion, "manaPotion", 0.8f));
                    break;
                case 3:
                    inventory.AddItem(new Item(Item.ItemType.LifeCrystal, "lifeCrystal", 1.5f));
                    break;
                case 4:
                    inventory.AddItem(new Item(Item.ItemType.Jar, "jar", 5f));
                    break;
            }
        }
        //Updating the Inventory UI when player mass adds items
        inventory.UpdateItem();
    }
}
