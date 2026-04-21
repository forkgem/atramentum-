using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New NPC Dialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    //public Sprite npcPortrait;
    public string[] dialogueLine;
    public bool[] autoProgressLine;
    public bool[] endDialogueLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;

    public DialogueChoice[] choices; 
}

[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex; //where choices appear
    public string[] choices; // response options 
    public int[] nextDialogueIndexes; //where choice leads 
}