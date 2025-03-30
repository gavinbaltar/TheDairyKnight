using UnityEngine;

/*

     Assignment: Mid
     Written by: Gavin Baltar created by following tutorial 1
     Filename: Unit.cs 

     Description: This script stores the stats and functionality of units in the game. Manages TakeDamage, Defend, and Heal.

*/
public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;

    public bool isDefending;
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        else
            return false;
    }

    public bool Heal(int healAmount, int manaCost)
    {
        currentMP -= manaCost;

        if (currentMP <= 0)
        {
            return false;
        }

        currentHP += healAmount;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        return true;
    }

    public void Defend()
    {
        // If a fully fledged game I would check to see if there's a status effect here preventing guard or something but there isn't
        currentMP += maxMP / 10; // Restore a tenth of mana, random arbitrary number.

        if (currentMP > maxMP)
            currentMP = maxMP;

        isDefending = true;
    }
}
