using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    //Instansiation singleton item
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    //Creating a reference to all the item sprite and World item prefab
    public Transform pfWorldItem;
    public Sprite coinSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite lifeCrystalSprite;
    public Sprite jarSprite;
}
