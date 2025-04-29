using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unit;

/*
    Assignment: Mid
    Written by: Gavin Baltar created by following tutorial 1
    Filename: BattleSystem.cs

    Description: This script manages the game state of the minigame by keeping track of player and enemy turns. It handles managing the entire game state by tracking turns and actions. 
        It also utilizes the Unit.cs and BattleHUD.cs script from the same tutorial as well as a SoundFXManager from tutorial 2 to play sounds over the battle music.
*/

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public TutorialManager tutorialManager;
    public AudioSource musicManager;
    public AudioClip victoryTheme;
    public AudioClip lossTheme;
    [SerializeField] public bool isTutorial;
    bool firstPromptPlayed;
    bool secondPromptPlayed;

    // Prefabs
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // Placement components.
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    // Stat components.
    Unit playerUnit;
    Unit enemyUnit;

    // UI components.
    public TextMeshProUGUI dialogueText; // Dialogue box.
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public Button[] playerActions;
    public GameObject skillMenu;
    public TextMeshProUGUI weaponType;

    private GameObject player;
    private GameObject enemy;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator MoveToPosition(Transform obj, Vector3 targetPosition, float
    duration)
    {
        Vector3 startPosition = obj.position;
        float elapsedTime = 0f;

        float bounceHeight = 0.2f;
        float bounceFrequency = 6f;

        //while (elapsedTime < duration)
        //{
        //    obj.position = Vector3.Lerp(startPosition, targetPosition,
        //    elapsedTime / duration);
        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}

        //obj.position = targetPosition;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 flatPos = Vector3.Lerp(startPosition, targetPosition, t);

            // Bounce using sine wave (continuous oscillation)
            float bounce = Mathf.Sin(elapsedTime * bounceFrequency * Mathf.PI * 2f) * bounceHeight;
            flatPos.y += bounce;

            obj.position = flatPos;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
    }

    IEnumerator SetupBattle()
    {
        player = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = player.GetComponent<Unit>();
        playerUnit.animator.SetBool("isSword", true); // Always start on Sword

        enemy = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemy.GetComponent<Unit>();

        Vector3 playerStartPos = playerBattleStation.position + Vector3.left * 10f;
        Vector3 enemyStartPos = enemyBattleStation.position + Vector3.right * 10f;

        player.transform.position = playerStartPos;
        enemy.transform.position = enemyStartPos;

        StartCoroutine(MoveToPosition(player.transform,playerBattleStation.position, 1f));

        StartCoroutine(MoveToPosition(enemy.transform, enemyBattleStation.position, 1f));

        dialogueText.text = playerUnit.unitName + " has come across " + enemyUnit.unitName + ". Prepare to clash!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        ToggleButtonActivity();
        ToggleButtonInteraction();

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public void UpdateStatusDuration()
    {
        if (playerUnit.isCountering)
        {
            playerUnit.counteringDuration -= 1;

            if (playerUnit.counteringDuration < 0)
            {
                playerUnit.counteringDuration = 0;
                playerUnit.isCountering = false;

                playerUnit.playerParrySprite.SetActive(false);
            }
        }

        if (enemyUnit.isWeakened)
        {
            enemyUnit.weakenedDuration -= 1;

            if (enemyUnit.weakenedDuration < 0)
            {
                enemyUnit.weakenedDuration = 0;
                enemyUnit.isWeakened = false;

                enemyUnit.enemyWeakenedSprite.SetActive(false);
            }
        }
    }

    void ToggleButtonInteraction()
    {
        if (playerActions[1].interactable)
        {
            foreach (Button button in playerActions)
            {
                button.interactable = false;
            }
        }
        else
        {
            foreach (Button button in playerActions)
            {
                button.interactable = true;
            }
        }
    }

    void ToggleButtonActivity()
    {
        if (playerActions[1].IsActive())
        {
            foreach (Button button in playerActions)
            {
                button.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Button button in playerActions)
            {
                button.gameObject.SetActive(true);
            }
        }
    }

    void PlayerTurn()
    {
        playerUnit.isDefending = false;
        playerUnit.playerBlockSprite.SetActive(false);

        UpdateStatusDuration();

        dialogueText.text = "What will you do?";

        ToggleButtonInteraction();

        if(isTutorial && !firstPromptPlayed && PlayerData.level == 1)
        {
            tutorialManager.SetUpFirstPrompt();
            firstPromptPlayed = true;
        } else if(isTutorial && !secondPromptPlayed && PlayerData.level == 1)
        {
            tutorialManager.SetUpSecondPrompt();
            secondPromptPlayed = true;
        }
        else if (isTutorial && !firstPromptPlayed && PlayerData.level == 2)
        {
            tutorialManager.SwapWeaponPrompt();
            firstPromptPlayed = true;
        }
        else
        {
            if (!firstPromptPlayed) {
                tutorialManager.SetUpPrompt();
                firstPromptPlayed = true;
            }
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        ToggleButtonInteraction();

        if (skillMenu.activeSelf)
        {
            OnSkillMenuClick();
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnDefendButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        ToggleButtonInteraction();

        if (skillMenu.activeSelf)
        {
            OnSkillMenuClick();
        }

        StartCoroutine(PlayerDefend());
    }

    public void OnSwapButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        ToggleButtonInteraction();

        if (skillMenu.activeSelf) {
            OnSkillMenuClick();
        }

        StartCoroutine(PlayerWeaponSwap());
    }

    public void OnSkillMenuClick()
    {
        skillMenu.SetActive(!skillMenu.activeSelf);
    }

    public void OnSkillOneClick()
    {
        if(state != BattleState.PLAYERTURN) return;

        ToggleButtonInteraction();
        OnSkillMenuClick(); // hide skill menu

        StartCoroutine(WeaponSkillAttack());
    }

    public void OnSkillTwoClick()
    {
        if (state != BattleState.PLAYERTURN) return;

        ToggleButtonInteraction();
        OnSkillMenuClick(); // hide skill menu

        StartCoroutine(WeaponSkillUtility());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        ToggleButtonInteraction();
        OnSkillMenuClick();

        StartCoroutine(PlayerHeal());
    }

    public string CheckWeaponType(Unit unit)
    {
        //Debug.Log(unit.unitName + " current is of weapon type: " + unit.weaponType.ToString());
        return unit.weaponType.ToString();
    }

    IEnumerator PlayerAttack()
    {
        Vector3 attackPosition = enemyBattleStation.position + Vector3.left * 2f; // Move slightly in front of the enemy

        yield return StartCoroutine(MoveToPosition(player.transform, attackPosition, 0.5f));

        playerUnit.animator.SetTrigger("Attack");

        dialogueText.text = playerUnit.unitName + " strikes " + enemyUnit.unitName + " with their weapon!";

        yield return new WaitForSeconds(1.0f);

        // Determine damage bonus or reduction if using wrong weapon type.
        int damage = DetermineDamageBonus(playerUnit.damage, playerUnit, enemyUnit);

        if (damage < 0)
        {
            damage = 0;
        }

        bool isDead = enemyUnit.TakeDamage(damage);
        enemyHUD.SetHP(enemyUnit.currentHP);

        if (playerUnit.unitAttack)
        {
            SoundFXManager.instance.PlaySoundSFXClip(playerUnit.unitAttack, enemyBattleStation, 10f);
        }

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(MoveToPosition(player.transform, playerBattleStation.position, 0.5f));

        yield return new WaitForSeconds(0.5f);

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    // Same function, just doesn't go to enemy turn.
    IEnumerator PlayerCounter()
    {
        playerUnit.playerBlockSprite.SetActive(false);

        Vector3 attackPosition = enemyBattleStation.position + Vector3.left * 2f; // Move slightly in front of the enemy

        yield return StartCoroutine(MoveToPosition(player.transform, attackPosition, 0.5f));

        if (playerUnit.unitAttack)
        {
            SoundFXManager.instance.PlaySoundSFXClip(playerUnit.unitAttack, enemyBattleStation, 10f);
        }

        playerUnit.animator.SetTrigger("Attack");

        dialogueText.text = playerUnit.unitName + " counters " + enemyUnit.unitName + " with their weapon!";

        yield return new WaitForSeconds(1.0f);

        // Determine damage bonus or reduction if using wrong weapon type.
        int damage = DetermineDamageBonus(playerUnit.damage, playerUnit, enemyUnit);

        if (damage < 0)
        {
            damage = 0;
        }

        bool isDead = enemyUnit.TakeDamage(damage);
        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(MoveToPosition(player.transform, playerBattleStation.position, 0.5f));

        if(playerUnit.isDefending)
        {
            playerUnit.playerBlockSprite.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
    }

    IEnumerator PlayerHeal()
    {
        bool isHealed = playerUnit.Heal(5, 2);

        if (!isHealed)
        {
            dialogueText.text = "Not enough MP to heal!";
            yield return new WaitForSeconds(1.5f);
            PlayerTurn(); // Let player pick another option.
        }

        else
        {
            if (playerUnit.playerHeal)
            {
                SoundFXManager.instance.PlaySoundSFXClip(playerUnit.playerHeal, playerBattleStation, 10f);
            }

            playerHUD.SetHP(playerUnit.currentHP);
            playerHUD.SetMP(playerUnit.currentMP);

            dialogueText.text = playerUnit.unitName + " restored health through the mystic arts!";

            yield return new WaitForSeconds(1.5f);

            playerUnit.playerHealSprite.SetActive(false);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerDefend()
    {
        dialogueText.text = playerUnit.unitName + " braces for impact!";

        yield return new WaitForSeconds(1.5f);

        playerUnit.Defend();
        playerHUD.SetMP(playerUnit.currentMP);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }


    IEnumerator PlayerWeaponSwap()
    {
        if (playerUnit.SwapWeapon())
        {
            dialogueText.text = playerUnit.unitName + " swaps weapons!";

            yield return new WaitForSeconds(0.5f);

            playerHUD.SetWeaponType(playerUnit.weaponType.ToString(), playerUnit);
            playerHUD.SetSkillText(playerUnit);

            ToggleButtonInteraction();

            CheckWeaponType(playerUnit);
        }
        else
        {
            dialogueText.text = playerUnit.unitName + " only has one weapon!";

            yield return new WaitForSeconds(1.0f);

            PlayerTurn();
        }
    }

    IEnumerator WeaponSkillAttack()
    {
        if (playerUnit.WeaponSkillCheck(5)) // Mana check
        {
            playerHUD.SetMP(playerUnit.currentMP);

            // JUST COPIED THE PLAYERATTACK IN HERE, I FIGURE ITS BETTER TO JUST MAKE A SECOND FUNCTION SO WE CAN ADD SPECIFIC ANIMATIONS AND STUFF
            // I was thinking of finding a way to store weapon info separately with their skills, but that would require remaking the whole system
            // And since this is a smaller game with a hard amount of 2 skills we can just hard code it this way. First one can be an attack the second can be some support move?

            Vector3 attackPosition = enemyBattleStation.position + Vector3.left * 2f; // Move slightly in front of the enemy

            yield return StartCoroutine(MoveToPosition(player.transform, attackPosition, 0.5f));

            playerUnit.animator.SetTrigger("Attack");

            switch (playerUnit.weaponType)
            {
                case WeaponType.Sword:
                    dialogueText.text = playerUnit.unitName + " cuts the cheese!";

                    break;

                case WeaponType.Spear:
                    dialogueText.text = playerUnit.unitName + " makes a capsaicin kebab!";

                    break;

                case WeaponType.Axe:
                    dialogueText.text = playerUnit.unitName + " chops some onions!";

                    break;
            }

            yield return new WaitForSeconds(1.0f);

            // Determine damage bonus or reduction if using wrong weapon type.
            int damage = DetermineDamageBonus(playerUnit.damage + 2, playerUnit, enemyUnit); // Skill is gonna be increased damage with a new animation, so just add a damage modifier?

            if(damage < 0)
            {
                damage = 0;
            }

            bool isDead = enemyUnit.TakeDamage(damage);
            enemyHUD.SetHP(enemyUnit.currentHP);

            if (playerUnit.unitAttack)
            {
                SoundFXManager.instance.PlaySoundSFXClip(playerUnit.unitAttack, enemyBattleStation, 10f);
            }

            yield return new WaitForSeconds(0.5f);

            yield return StartCoroutine(MoveToPosition(player.transform, playerBattleStation.position, 0.5f));

            yield return new WaitForSeconds(0.5f);

            if (isDead)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        else
        {
            dialogueText.text = playerUnit.unitName + " doesn't have enough mana!";
            yield return new WaitForSeconds(1.5f);
            PlayerTurn(); // Let player pick another option.
        }
    }

    IEnumerator WeaponSkillUtility()
    {
        if(playerUnit.WeaponSkillCheck(15))
        {
            switch (playerUnit.weaponType)
            {
                case WeaponType.Sword:
                    dialogueText.text = playerUnit.unitName + " ages the enemy like a fine cheese! They're weakened for 3 turns!";

                    playerUnit.SetStatusEffect(enemyUnit);

                    break;

                case WeaponType.Spear:
                    dialogueText.text = playerUnit.unitName + " prepares to counter the enemy's attacks for the next 3 turns!";

                    playerUnit.SetStatusEffect(playerUnit);

                    break;

                case WeaponType.Axe:
                    dialogueText.text = playerUnit.unitName + " chops some onions! They're vulnerable for the next attack!";

                    playerUnit.SetStatusEffect(enemyUnit);

                    break;
            }

            playerHUD.SetMP(playerUnit.currentMP);

            yield return new WaitForSeconds(1.0f);

            StartCoroutine(EnemyTurn());

            // Maybe come up with icons here to turn on and off depending on the status that is applied?
        } 
        
        else
        {
            dialogueText.text = playerUnit.unitName + " doesn't have enough mana!";
            yield return new WaitForSeconds(1.5f);
            PlayerTurn(); // Let player pick another option.
        }
    }

    public int DetermineDamageBonus(int damage, Unit attacker, Unit defender)
    {
        switch (attacker.weaponType)
        {
            case WeaponType.Sword:

                if (defender.weaponType == WeaponType.Spear)
                {
                    dialogueText.text += " It bounces off their weapon!";
                    damage -= 2;
                }

                else if (defender.weaponType == WeaponType.Axe)
                {
                    dialogueText.text += " It strikes their weakpoint!";
                    damage += 2;
                }

                break;

            case WeaponType.Spear:

                if (defender.weaponType == WeaponType.Axe)
                {
                    dialogueText.text += " It bounces off their weapon!";
                    damage -= 2;
                }

                else if (defender.weaponType == WeaponType.Sword)
                {
                    dialogueText.text += " It strikes their weakpoint!";
                    damage += 2;
                }

                break;

            case WeaponType.Axe:

                if (defender.weaponType == WeaponType.Sword)
                {
                    dialogueText.text += " It bounces off their weapon!";
                    damage -= 2;
                }

                else if (defender.weaponType == WeaponType.Spear)
                {
                    dialogueText.text += " It strikes their weakpoint!";
                    damage += 2;
                }

                break;
        }

        if (defender.isVulnerable) { 
            damage *= 2; 
            defender.vulnerableDuration = 0;
            defender.isVulnerable = false;
            defender.enemyVulnerableSprite.SetActive(false);
        }

        if (attacker.isWeakened) { damage /= 2; }

        if(damage < 0)
        {
            damage = 0;
        }

        return damage;
    }

    IEnumerator EnemyTurn()
    {
        bool isDead = false;
        Vector3 attackPosition = playerBattleStation.position + Vector3.right * 2f;

        // Move slightly in front of the enemy
        yield return StartCoroutine(MoveToPosition(enemy.transform, attackPosition, 0.5f));

        enemyUnit.animator.SetTrigger("Attack");

        dialogueText.text = enemyUnit.unitName + " swings their weapon at " + playerUnit.unitName + "!";

        yield return new WaitForSeconds(.5f);

        if (enemyUnit.enemyAttack)
        {
            SoundFXManager.instance.PlaySoundSFXClip(enemyUnit.enemyAttack, playerBattleStation, 10f);
        }

        // Determine reduction & bonuses
        int damage = DetermineDamageBonus(enemyUnit.damage, enemyUnit, playerUnit);

        if (playerUnit.isDefending)
        {
            if (playerUnit.playerBlock)
            {
                SoundFXManager.instance.PlaySoundSFXClip(playerUnit.playerBlock, playerBattleStation, 10f);
            }

            dialogueText.text = playerUnit.unitName + " defended against the blow!";

            yield return new WaitForSeconds(1.5f);

            isDead = playerUnit.TakeDamage(damage / 2);
        }
        else
        {
            isDead = playerUnit.TakeDamage(damage);
        }

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(MoveToPosition(enemy.transform, enemyBattleStation.position, 0.5f));

        yield return new WaitForSeconds(0.5f);

        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            if(playerUnit.isCountering)
            {
                yield return StartCoroutine(PlayerCounter());
            }

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = playerUnit.unitName + " triumphed! You win!";

            musicManager.Stop();
            musicManager.PlayOneShot(victoryTheme);

            playerUnit.playerParrySprite.SetActive(false);

            yield return new WaitForSeconds(3.5f);

            Vector3 playerEndPos = playerBattleStation.position + Vector3.right * 17f;

            StartCoroutine(MoveToPosition(player.transform, playerEndPos, 2f));

            yield return new WaitForSeconds(2f);


            if (PlayerData.level != 3)
            {
                PlayerData.level++;
                PlayerData.canSelect = false;

                SceneManager.LoadScene("LevelSelect");
            }
            else
            {
                SceneManager.LoadScene("WinScreen");
            }

        }
        else if (state == BattleState.LOST)
        {
            PlayerData.canSelect = true;

            dialogueText.text = "The Spice Syndicate defeated the brave Dairy Knight... You lose.";

            musicManager.Stop();
            musicManager.PlayOneShot(lossTheme);

            yield return new WaitForSeconds(3.5f);

            SceneManager.LoadScene("LevelSelect");
        }
    }
}
