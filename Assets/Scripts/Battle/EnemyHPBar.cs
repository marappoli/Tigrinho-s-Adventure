using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetEnemyHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);

        // Verificar se a vida do inimigo acabou
        if (hpNormalized <= 0)
        {
            // Se a vida do inimigo acabar, ocultar a barra de vida ou realizar outras ações necessárias
            health.SetActive(false);
        }
        else
        {
            // Se a vida do inimigo ainda não acabou, garantir que a barra de vida esteja visível
            health.SetActive(true);
        }
    }
}
