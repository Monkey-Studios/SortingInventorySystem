using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    //This creates and instance of the world item prefab
    public static WorldItem SpawnWorldItem(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfWorldItem, position, Quaternion.identity);
        //Sets world item transform
        WorldItem worldItem = transform.GetComponent<WorldItem>();
        worldItem.SetItem(item);

        return worldItem;
    }
    //This will drop the item and push it away from the player
    public static WorldItem DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        WorldItem worldItem = SpawnWorldItem(dropPosition + randomDir * 1f, item);
        worldItem.GetComponent<Rigidbody2D>();
        return worldItem;
    }
    //When the game starts the item within the game world looks for the corrosponding sprite renderer and sets it
    private Item item;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
