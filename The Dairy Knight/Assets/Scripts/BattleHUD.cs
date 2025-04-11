using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unit;
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
    public TextMeshProUGUI weaponType;

    public TextMeshProUGUI skillOne;
    public TextMeshProUGUI skillTwo;
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        healthText.text = "HP: " + unit.currentHP + "/" + unit.maxHP;
        manaText.text = "MP: " + unit.currentMP + "/" + unit.maxMP;
        weaponType.text = "Weapon: " + unit.weaponType;

        if(skillOne && skillTwo)
        {
            SetSkillText(unit);
        }
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

    public void SetWeaponType(string weapon, Unit unit)
    {
        weaponType.text = "Weapon: " + weapon;


        // Ensure valid text boxes given (Enemies don't have them)
        if(!skillOne && !skillTwo)
        {
            return;
        }

        SetSkillText(unit);
    }

    public void SetSkillText(Unit unit)
    {
        if (skillOne)
        {
            switch (unit.weaponType)
            {
                case WeaponType.Sword:
                    skillOne.text = "Cheddar Clash - 5 MP";
                    break;

                case WeaponType.Spear:
                    skillOne.text = "Swiss Skewer - 5 MP";
                    break;

                case WeaponType.Axe:
                    skillOne.text = "Colby Cleave - 5 MP";
                    break;
            }
        }

        if (skillTwo)
        {
            switch (unit.weaponType)
            {
                case WeaponType.Sword:
                    skillTwo.text = "Aged Cheese - 15 MP";
                    break;

                case WeaponType.Spear:
                    skillTwo.text = "Parmigiano Riposte - 15 MP";
                    break;

                case WeaponType.Axe:
                    skillTwo.text = "Pepper Hi-Jack - 15 MP";
                    break;
            }
        }
    }
}

