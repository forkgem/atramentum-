using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; } //Singleton instance
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    //public Image portraitImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); //Only one instance
    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show); //toggle ui visibility
    }

    public void SetNPCInfo(string npcName, Sprite portrait)
    {
        nameText.text = npcName;
        //portraitImage.sprite = portrait;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
}