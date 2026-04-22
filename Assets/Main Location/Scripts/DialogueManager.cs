using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkFile;
    public TextMeshProUGUI textBox;
    public Button[] choiceButtons;

    private Story story;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        story = new Story(inkFile.text);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            ContinueStory();
        }
    }

    private void ContinueStory()
    {
        if (story.canContinue)
        {
            textBox.gameObject.SetActive(true);
            textBox.text = story.Continue();
            ShowChoices();
        }
        else
        {
            FinishDialogue();
        }
    }

    private void ShowChoices()
    {
        List<Choice> choices = story.currentChoices;
        int index = 0;
        foreach (Choice c in choices)
        {
            choiceButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = c.text;
            choiceButtons[index].gameObject.SetActive(true);
            index++;
        }

        for(int i  = index; i < choices.Count; i++)
        {
            choiceButtons[index].gameObject.SetActive(false);
        }
    }

    public void SetDecision(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
    private void FinishDialogue()
    {
        textBox.gameObject.SetActive(false);
    }
}
