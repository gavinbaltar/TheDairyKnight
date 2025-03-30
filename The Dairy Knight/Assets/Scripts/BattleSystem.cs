using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

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

    // Audio clips
    [Header("Unit Audio SFX")]
    [SerializeField] private AudioClip unitAttack;
    [SerializeField] private AudioClip playerBlock;
    [SerializeField] private AudioClip playerHeal;

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

        while (elapsedTime < duration)
        {
            obj.position = Vector3.Lerp(startPosition, targetPosition,
            elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.position = targetPosition;
    }

    IEnumerator SetupBattle()
    {
        player = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = player.GetComponent<Unit>();

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

        dialogueText.text = "What will you do?";

        ToggleButtonInteraction();
    }
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        ToggleButtonInteraction();

        StartCoroutine(PlayerAttack());
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        ToggleButtonInteraction();

        StartCoroutine(PlayerHeal());
    }
    public void OnDefendButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        ToggleButtonInteraction();

        StartCoroutine(PlayerDefend());
    }
    IEnumerator PlayerAttack()
    {
        Vector3 attackPosition = enemyBattleStation.position + Vector3.left * 2f; // Move slightly in front of the enemy

        yield return StartCoroutine(MoveToPosition(player.transform, attackPosition, 0.5f));

        dialogueText.text = playerUnit.unitName + " strikes " + enemyUnit.unitName + " with their weapon!";

        yield return new WaitForSeconds(1.0f);

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHP);

        if (unitAttack)
        {
            SoundFXManager.instance.PlaySoundSFXClip(unitAttack, enemyBattleStation, 10f);
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
            if (playerHeal)
            {
                SoundFXManager.instance.PlaySoundSFXClip(playerHeal, playerBattleStation, 10f);
            }

            playerHUD.SetHP(playerUnit.currentHP);
            playerHUD.SetMP(playerUnit.currentMP);

            dialogueText.text = playerUnit.unitName + " restored health through the mystic arts!";

            yield return new WaitForSeconds(1.5f);

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
    IEnumerator EnemyTurn()
    {
        bool isDead = false;
        Vector3 attackPosition = playerBattleStation.position + Vector3.right * 2f;

        // Move slightly in front of the enemy
        yield return StartCoroutine(MoveToPosition(enemy.transform, attackPosition, 0.5f));

        dialogueText.text = enemyUnit.unitName + " swings their weapon at " + playerUnit.unitName + "!";

        yield return new WaitForSeconds(1.0f);

        if (unitAttack)
        {
            SoundFXManager.instance.PlaySoundSFXClip(unitAttack, playerBattleStation, 10f);
        }

        if (playerUnit.isDefending)
        {
            if (playerBlock)
            {
                SoundFXManager.instance.PlaySoundSFXClip(playerBlock, playerBattleStation, 10f);
            }

            dialogueText.text = playerUnit.unitName + " defended against the blow!";

            yield return new WaitForSeconds(1.5f);

            isDead = playerUnit.TakeDamage(enemyUnit.damage / 2);
        }
        else
        {
            isDead = playerUnit.TakeDamage(enemyUnit.damage);
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
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Bud triumphed! You win!";

            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene("Midterm_WinScreen");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Bun is the victor this time... You lose.";

            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene("Midterm_LossScreen");
        }
    }
}
