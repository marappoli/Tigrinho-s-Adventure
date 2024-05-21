using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Adicione esta linha

public class BattleSystem2 : MonoBehaviour
{
    public float playerHealth = 100f;
    public float enemyHealth = 80f; // Aumentando a vida do inimigo
    public float playerDefense = 0f; // A defesa inicial do jogador é 0
    public float enemyAttackDamage = 15f; // Aumentando o dano do ataque do inimigo

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
            Run();
        }

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(gameOverSceneName);
        }

        if (enemyHealth <= 0)
        {
            SceneManager.LoadScene(victorySceneName);
            PlayerPrefs.SetInt("Nivel2Completo", 1);
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
        enemyHealth -= damageDealt - playerDefense; // O dano do jogador é reduzido pela defesa
        playerHPBar.SetHP(playerHealth / 100f);
        enemyHPBar.SetEnemyHP(enemyHealth / 80f); // A vida do inimigo é normalizada de 0 a 1

        // Aqui você pode adicionar efeitos visuais ou sonoros de ataque
    }

    void Defend()
{
    // A defesa pode ser total ou falhar
    float defenseAmount = Random.Range(0f, 1f); // Defesa aleatória entre 0 e 1

    if (defenseAmount <= 0.6f) // 60% de chance de defesa total
    {
        playerDefense = 5f; // Aumenta a defesa do jogador
        defenseSuccessText.text = "Defesa total!"; // Adicione esta linha
        StartCoroutine(ClearTextAfterDelay(defenseSuccessText));
    }
    else // 30% de chance de falha na defesa
    {
        playerDefense = 0f; // A defesa do jogador é 0
        defenseFailText.text = "A defesa falhou"; // Adicione esta linha
        StartCoroutine(ClearTextAfterDelay(defenseFailText));
    }
}

IEnumerator ClearTextAfterDelay(TextMeshProUGUI text)
{
    yield return new WaitForSeconds(2f); // Ajuste este valor conforme necessário
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
        float damageDealt = Random.Range(10f, 15f); // Aumentando o dano do inimigo
        if (playerDefense > 0f) // Se a defesa do jogador for bem-sucedida
        {
            Debug.Log("O jogador se defendeu do ataque!");
        }
        else // Se a defesa do jogador falhar
        {
            playerHealth -= damageDealt; // O dano do inimigo é aplicado
            playerHPBar.SetHP(playerHealth / 100f);
        }
        playerDefense = 0f; // Redefine a defesa do jogador para o próximo turno

        // Aqui você pode adicionar efeitos visuais ou sonoros do ataque do inimigo
    }
}
