using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCpntroller : MonoBehaviour
{
    private ItemDictionary itemDictionary;

    public GameObject inventoryPanel;
    public GameObject slotsPrefab;
    public int slotCount;
    public GameObject[] itemPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDictionary = FindAnyObjectByType<ItemDictionary>();

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotsPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefab.Length)
            {
                GameObject item = Instantiate(itemPrefab[i], slot.transform);
                //item.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                item.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 1);
                slot.currentItem = item;
            }
        }

    }

    public bool AddItem(GameObject itemPrefab)
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        return false;
    }
}

//    public List<InventorySaveData> GetInventoryItems()
//    {
//        List<InventorySaveData> invData = new List<InventorySaveData>();
//        foreach (Transform slotTransform in inventoryPanel.transform)
//        {
//            Slot slot = slotTransform.GetComponent<Slot>();
//            if (slot.currentItem != null)
//            {
//                Item item = slot.currentItem.GetComponent<Item>();
//                invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
//            }
//        }

//        return invData;
//    }

//    public void SetInventoryItems(List<InventorySaveData> inventorySaveDatas)
//    {
//        //clear all inventory to prevent duplicates
//        foreach (Transform child in inventoryPanel.transform)
//        {
//            Destroy(child.gameObject);
//        }

//        //create new slots
//        for (int i = 0; i < slotCount; i++)
//        {
//            Instantiate(slotsPrefab, inventoryPanel.transform);
//        }

//        //populate slots with saved items

//        foreach (InventorySaveData data in inventorySaveDatas)
//        {
//            if (data.slotIndex < slotCount)
//            {
//                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
//                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
//                if (itemPrefab != null)
//                {
//                    GameObject item = Instantiate(itemPrefab, slot.transform);
//                    //item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
//                    item.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 1);
//                }
//            }
//        }
//    }
//}
