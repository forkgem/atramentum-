//using Cinemachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryCpntroller inventoryCpntroller;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryCpntroller = FindAnyObjectByType<InventoryCpntroller>();

        LoadGame();


    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData()
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            //inventorySaveData = inventoryCpntroller.GetInventoryItems()

        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            //inventoryCpntroller.SetInventoryItems(saveData.inventorySaveData);
        }

        else
        {
            SaveGame();
        }
    }
}
