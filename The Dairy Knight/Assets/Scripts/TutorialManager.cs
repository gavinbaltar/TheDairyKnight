using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Assignment: Final
    Written by: Gavin Baltar
    Filename: TutorialManager.cs
    Description: This script enables and disables text boxes and player buttons to provide a tutorial guide.
*/
public class TutorialManager : MonoBehaviour
{
    public Button[] buttonList; // need to be able to disable buttons for tutorial.
    public Button attackButton;
    public Button skillButton;
    public Button healButton;
    public Button swapButton;

    public GameObject dialoguePrompt;
    public GameObject secondPrompt;

    public void SetUpPrompt()
    {
        dialoguePrompt.SetActive(true);

        foreach (var button in buttonList)
        {
            button.interactable = false;
        }
    }
    public void SetUpFirstPrompt()
    {
        dialoguePrompt.SetActive(true);

        foreach (var button in buttonList)
        {
            button.interactable = false;
        }

        attackButton.onClick.AddListener(OnButtonPress);
    }

    public void SetUpSecondPrompt()
    {
        if (secondPrompt)
        {
            dialoguePrompt = secondPrompt;

            dialoguePrompt.SetActive(true);

            foreach (var button in buttonList)
            {
                button.interactable = false;
            }

            healButton.onClick.AddListener(OnButtonPress);
        }
    }

    public void NextPrompt(GameObject nextPrompt)
    {
        dialoguePrompt.SetActive(false);
        dialoguePrompt = nextPrompt;
        dialoguePrompt.SetActive(true);
    }

    public void AttackPrompt(GameObject nextPrompt)
    {
        // Allow the attack button to be pressed
        attackButton.interactable = true;

        // Load attack dialogue tutorial
        NextPrompt(nextPrompt);
    }

    public void HealPrompt(GameObject nextPrompt)
    {
        skillButton.interactable = true;
        healButton.interactable = true;

        NextPrompt(nextPrompt);
    }

    // Kinda required for last prompts of player turn to reset the interactable value correctly.
    public void LastPrompt()
    {
        foreach (var button in buttonList)
        {
            button.interactable = false;
        }
    }

    public void SwapWeaponPrompt()
    {
        dialoguePrompt.SetActive(true);

        foreach (var button in buttonList)
        {
            button.interactable = false;
        }

        swapButton.interactable = true;

        swapButton.onClick.AddListener(OnButtonPress);
    }

    public void OnButtonPress()
    {
        dialoguePrompt.SetActive(false);

        LastPrompt();
    }

    public void OnDialogueEnd()
    {
        dialoguePrompt.SetActive(false);

        foreach (var button in buttonList)
        {
            button.interactable = true;
        }
    }
}