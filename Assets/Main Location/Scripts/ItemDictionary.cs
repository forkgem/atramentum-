using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;


public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemPrefab;
    private Dictionary<int, GameObject> itemDictionary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
         itemDictionary = new Dictionary<int, GameObject>();

        //AutoIncrementIds
        for(int i = 0; i < itemPrefab.Count; i++)
        {
            if (itemPrefab[i] != null)
            {
                itemPrefab[i].ID = i +1;
            }
        }

        foreach (Item item in itemPrefab)
        {
            itemDictionary[item.ID] = item.gameObject;
        }
    }
   
    public GameObject GetItemPrefab(int itemID)
    {
        return itemDictionary[itemID]; //#9 tutorial, changed code it may crash here 
        
    }
        
    

}
