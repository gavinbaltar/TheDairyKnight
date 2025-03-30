using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

/*
    Assignment: Mid
    Written by: Gavin Baltar created by following tutorial 1
    Filename: BattleHUD.cs
    Description: This script updates the text in the UI display of the Gameplay
        scene and is called by BattleSystem.cs.
*/

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        healthText.text = "HP: " + unit.currentHP + "/" + unit.maxHP;
        manaText.text = "MP: " + unit.currentMP + "/" + unit.maxMP;
    }
    public void SetHP(int hp)
    {
        // Should just take the number after the '/' so I don't need to pass in the unit

        string health = healthText.text;
        string[] parts = health.Split('/');
        int maxHealth = int.Parse(parts[1]);
        healthText.text = "HP: " + hp + "/" + maxHealth;
    }
    public void SetMP(int mp)
    {
        string mana = manaText.text;
        string[] parts = mana.Split('/');
        int maxMana = int.Parse(parts[1]);
        manaText.text = "MP: " + mp + "/" + maxMana;
    }
}

