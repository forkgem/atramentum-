using UnityEngine;

public class InventoryCpntroller : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotsPrefab;
    public int slotCount;
    public GameObject[] itemPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotsPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefab.Length)
            {
                GameObject item = Instantiate(itemPrefab[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
