using TMPro;
using UnityEngine;

/*

     Assignment: Mid
     Written by: Gavin Baltar created by following tutorial 1
     Filename: Unit.cs 

     Description: This script stores the stats and functionality of units in the game. Manages TakeDamage, Defend, and Heal.

*/
public class Unit : MonoBehaviour
{
    public enum WeaponType
    {
        Sword,
        Spear,
        Axe,
    }

    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;

    public bool isDefending;

    [Header("Weapon Type Tracker")]
    [SerializeField] public WeaponType weaponType;

    // Audio clips
    [Header("Unit Audio SFX")]
    [SerializeField] public AudioClip unitAttack;
    [SerializeField] public AudioClip playerBlock;
    [SerializeField] public AudioClip playerHeal;

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
        if (currentMP - manaCost < 0)
        {
            return false;
        }

        currentMP -= manaCost;

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

    public void SwapWeapon()
    {
        switch (weaponType)
        {
            case WeaponType.Sword:
                weaponType = WeaponType.Spear;
                break;
            case WeaponType.Spear:
                weaponType = WeaponType.Axe;
                break;
            case WeaponType.Axe:
                weaponType = WeaponType.Sword;
                break;
        }
    }

    public bool WeaponAttackSkill(int manaCost)
    {
        // Check if there is enough mana to use the WeaponSkill, if there is then change dialogue.
        if (currentMP - manaCost < 0)
        {
            return false;
        }

        currentMP -= manaCost;

        return true;
    }
}
