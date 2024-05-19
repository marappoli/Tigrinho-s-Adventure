using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    public float playerHealth = 100f;
    public float enemyHealth = 50f;

    public HPBar playerHPBar;
    public EnemyHPBar enemyHPBar; // Adicionando a barra de vida do inimigo

    public string gameOverSceneName; // Nome da cena para carregar quando o jogador perder
    public string victorySceneName; // Nome da cena para carregar quando o jogador vencer
    public string previousSceneName; // Nome da cena para carregar quando o jogador escolhe correr

    public float attackInterval = 1.5f; // Intervalo entre ataques do jogador e do inimigo
    private bool playerCanAttack = true; // Variável para controlar se o jogador pode atacar

    private bool isPlayerTurn = true; // Variável para controlar de quem é o turno

    void Start()
    {
        // Inicializar as barras de vida do jogador e do inimigo
        playerHPBar.SetHP(1f); // Vida completa no início
        enemyHPBar.SetEnemyHP(1f); // Vida completa no início

        // Iniciar a corrotina para o ataque do inimigo
        StartCoroutine(TurnRoutine());
    }

    void Update()
    {
        // Verificar se o jogador pressionou a tecla de ataque e é a vez do jogador
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerCanAttack && isPlayerTurn)
        {
            Attack();
            isPlayerTurn = false; // É a vez do inimigo atacar agora
            StartCoroutine(PlayerAttackCooldown()); // Iniciar a contagem regressiva para o próximo ataque do jogador
        }

        // Verificar se o jogador pressionou a tecla para correr e é a vez do jogador
        if (Input.GetKeyDown(KeyCode.Alpha2) && isPlayerTurn)
        {
            Run();
        }

        // Verificar se a vida do jogador chegou a zero
        if (playerHealth <= 0)
        {
            // Se a vida do jogador acabar, carregar a cena de Game Over
            SceneManager.LoadScene(gameOverSceneName);
            PlayerPrefs.SetInt("Nivel1Completo", 0);
        }

        // Verificar se a vida do inimigo chegou a zero
        if (enemyHealth <= 0)
        {
            // Se a vida do inimigo acabar, carregar a cena de vitória
            SceneManager.LoadScene(victorySceneName);
            PlayerPrefs.SetInt("Nivel1Completo", 1);
        }
    }

    IEnumerator PlayerAttackCooldown()
    {
        yield return new WaitForSeconds(attackInterval);
        playerCanAttack = true; // Após o intervalo, permitir que o jogador ataque novamente
    }

    void Attack()
    {
        // Reduzir a vida do inimigo ao atacar
        enemyHealth -= 10f; // Por exemplo, o ataque tira 10 de vida do inimigo
        // Atualizar a barra de vida do jogador
        playerHPBar.SetHP(playerHealth / 100f); // 100 é a vida máxima do jogador
        // Atualizar a barra de vida do inimigo
        enemyHPBar.SetEnemyHP(enemyHealth / 50f); // 50 é a vida máxima do inimigo

        // Aqui você pode adicionar efeitos visuais ou sonoros de ataque
    }

    IEnumerator TurnRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isPlayerTurn); // Aguardar até que seja a vez do inimigo

            yield return new WaitForSeconds(attackInterval);

            // Ataque do inimigo
            EnemyAttack();

            isPlayerTurn = true; // Após o ataque do inimigo, é a vez do jogador atacar novamente
            playerCanAttack = true; // Permitir que o jogador ataque
        }
    }

    void EnemyAttack()
    {
        // Reduzir a vida do jogador ao ataque do inimigo
        playerHealth -= 10f; // Por exemplo, o ataque do inimigo tira 5 de vida do jogador
        // Atualizar a barra de vida do jogador
        playerHPBar.SetHP(playerHealth / 100f); // 100 é a vida máxima do jogador
        // Atualizar a barra de vida do inimigo
        enemyHPBar.SetEnemyHP(enemyHealth / 50f); // 50 é a vida máxima do inimigo

        // Aqui você pode adicionar efeitos visuais ou sonoros do ataque do inimigo
    }

    void Run()
    {
        // Carregar a cena anterior quando o jogador escolhe correr
        SceneManager.LoadScene(previousSceneName);
    }
}
