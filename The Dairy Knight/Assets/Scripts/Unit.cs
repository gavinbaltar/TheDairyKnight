using System.Collections;
using TMPro;
using Unity.VisualScripting;
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

    [Header("Player Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite playerSword;
    public Sprite playerAxe;
    public Sprite playerSpear;
    public Animator animator;
   
    [Header("Status Effects")]
    public bool isWeakened;
    public bool isVulnerable;
    public bool isCountering;
    public int weakenedDuration = 0;
    public int vulnerableDuration = 0;
    public int counteringDuration = 0;

    [Header("Weapon Type Tracker")]
    [SerializeField] public WeaponType weaponType;

    // Audio clips
    [Header("Unit Audio SFX")]
    [SerializeField] public AudioClip unitAttack;
    [SerializeField] public AudioClip playerBlock;
    [SerializeField] public AudioClip playerHeal;

    private void Start()
    {
        switch(weaponType)
        {
            case WeaponType.Sword:
                animator.SetBool("isSword", true);
                break;
            case WeaponType.Spear:
                animator.SetBool("isSpear", true);
                break;
            case WeaponType.Axe:
                animator.SetBool("isAxe", true);
                break;
        }
    }
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            currentHP = 0;
            gameObject.SetActive(false);
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
        currentMP += maxMP / 5; // Restore a tenth of mana, random arbitrary number.

        if (currentMP > maxMP)
            currentMP = maxMP;

        isDefending = true;
    }

    public bool SwapWeapon()
    {
        // Player has access to 1 weapon on level 1, 2 on level 2, and 3 on level 3
        animator.ResetTrigger("Attack");

        if (unitLevel == 1)
        {
            return false;
        }

        else if (unitLevel == 2)
        {
            if (weaponType == WeaponType.Sword)
            {
                animator.SetBool("isAxe", true);
                animator.SetBool("isSword", false);
                animator.SetBool("isSpear", false);

                weaponType = WeaponType.Axe;
            }
            else
            {
                animator.SetBool("isAxe", false);
                animator.SetBool("isSword", true);
                animator.SetBool("isSpear", false);

                weaponType = WeaponType.Sword;
            }
        }
        else
        {

            switch (weaponType)
            {
                case WeaponType.Sword:
                    animator.SetBool("isAxe", false);
                    animator.SetBool("isSword", false);
                    animator.SetBool("isSpear", true);

                    weaponType = WeaponType.Spear;
                    break;

                case WeaponType.Spear:
                    animator.SetBool("isAxe", true);
                    animator.SetBool("isSword", false);
                    animator.SetBool("isSpear", false);

                    weaponType = WeaponType.Axe;
                    break;

                case WeaponType.Axe:
                    animator.SetBool("isAxe", false);
                    animator.SetBool("isSword", true);
                    animator.SetBool("isSpear", false);

                    weaponType = WeaponType.Sword;
                    break;
            }
        }

        // Update sprite
        switch (weaponType)
        {
            case WeaponType.Sword:
                spriteRenderer.sprite = playerSword;
                break;

            case WeaponType.Axe:
                spriteRenderer.sprite = playerAxe;
                break;

            case WeaponType.Spear:
                spriteRenderer.sprite = playerSpear;
                break;
        }

        return true;
    }

    public bool WeaponSkillCheck(int manaCost)
    {
        // Check if there is enough mana to use the WeaponSkill, if there is then change dialogue.
        if (currentMP - manaCost < 0)
        {
            return false;
        }

        currentMP -= manaCost;

        return true;
    }

    public bool SetStatusEffect(Unit target)
    {
        switch (weaponType)
        {
            case WeaponType.Sword:

                target.isWeakened = true;
                target.weakenedDuration = 3;

                break;

            case WeaponType.Spear:

                //  This one applies to the player not the enemy.
                isCountering = true;
                counteringDuration = 3;

                break;

            case WeaponType.Axe:

                target.isVulnerable = true;
                target.vulnerableDuration = 1;

                break;
        }

        return true;
    }
}
