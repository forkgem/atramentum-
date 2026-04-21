using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerItemCollector : MonoBehaviour
{
    private InventoryCpntroller inventoryCpntroller;

    void Start()
    {
        inventoryCpntroller = FindAnyObjectByType<InventoryCpntroller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>(); 
            if(item != null)
            {
                //Add Item to inventory
                bool itemAdded = inventoryCpntroller.AddItem(other.gameObject);
                if(itemAdded)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
