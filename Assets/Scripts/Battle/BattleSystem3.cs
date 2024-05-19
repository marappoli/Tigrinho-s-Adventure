using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleSystem3 : MonoBehaviour
{
    public float playerHealth = 100f;
    public float enemyHealth = 150f; 
    public float playerDefense = 0f; 
    public float enemyAttackDamage = 25f;

    public int healCount = 3;
    public TextMeshProUGUI healCountText;
    public HPBar playerHPBar;
    public EnemyHPBar enemyHPBar;

    public TextMeshProUGUI defenseSuccessText;
    public TextMeshProUGUI defenseFailText; 

    public string gameOverSceneName;
    public string victorySceneName;
    public string previousSceneName;

    public float attackInterval = 1.5f;
    private bool playerCanAttack = true;
    private bool isPlayerTurn = true;

    void Start()
    {
        playerHPBar.SetHP(1f);
        enemyHPBar.SetEnemyHP(1f);
        StartCoroutine(TurnRoutine());
        healCountText.text = "Curas restantes: " + healCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerCanAttack && isPlayerTurn)
        {
            Attack();
            isPlayerTurn = false;
            StartCoroutine(PlayerAttackCooldown());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && isPlayerTurn)
        {
            Defend();
            isPlayerTurn = false;
            StartCoroutine(PlayerAttackCooldown());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && isPlayerTurn)
        {
            Heal();
            isPlayerTurn = false;
            StartCoroutine(PlayerAttackCooldown());
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && isPlayerTurn)
        {
            Run();
        }

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(gameOverSceneName);
        }

        if (enemyHealth <= 0)
        {
            SceneManager.LoadScene(victorySceneName);
        }
    }

    IEnumerator PlayerAttackCooldown()
    {
        yield return new WaitForSeconds(attackInterval);
        playerCanAttack = true;
    }

    void Attack()
    {
        float damageDealt = 20f;
        enemyHealth -= damageDealt - playerDefense;
        playerHPBar.SetHP(playerHealth / 100f);
        enemyHPBar.SetEnemyHP(enemyHealth / 150f);

    }

    void Defend()
{

    float defenseAmount = Random.Range(0f, 1f); 

    if (defenseAmount <= 0.7f) 
    {
        playerDefense = 5f;
        defenseSuccessText.text = "Defesa total!"; 
        StartCoroutine(ClearTextAfterDelay(defenseSuccessText));
    }
    else 
    {
        playerDefense = 0f; 
        defenseFailText.text = "A defesa falhou"; 
        StartCoroutine(ClearTextAfterDelay(defenseFailText));
    }
}

    void Heal()
    {
        if (healCount > 0)
        {
            playerHealth += 30f; 
            playerHealth = Mathf.Min(playerHealth, 100f); 
            playerHPBar.SetHP(playerHealth / 100f);
            healCount--; 
            healCountText.text = "Curas restantes: " + healCount; 
        }
        else
        {
            Debug.Log("Você não pode se regenerar mais vezes.");
        }
    }

IEnumerator ClearTextAfterDelay(TextMeshProUGUI text)
{
    yield return new WaitForSeconds(2f); 
    text.text = "";
}


    void Run()
    {
        SceneManager.LoadScene(previousSceneName);
    }

   IEnumerator TurnRoutine()
{
    while (true)
    {
        yield return new WaitUntil(() => !isPlayerTurn);

        yield return new WaitForSeconds(attackInterval);

        EnemyAttack();

        isPlayerTurn = true;
        playerCanAttack = true;
    }
}


    void EnemyAttack()
    {
        float damageDealt = Random.Range(20f, 25f);
        if (playerDefense > 0f) 
        {
            Debug.Log("O jogador se defendeu do ataque!");
        }
        else 
        {
            playerHealth -= damageDealt; 
            playerHPBar.SetHP(playerHealth / 100f);
        }
        playerDefense = 0f; 

        // Aqui você pode adicionar efeitos visuais ou sonoros do ataque do inimigo
    }
}
