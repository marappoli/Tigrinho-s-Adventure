using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);

        // Verificar se a vida acabou
        if (hpNormalized <= 0)
        {
            // Se a vida acabar, destruir o objeto ao qual esta barra de vida está vinculada
            Destroy(transform.parent.gameObject);
        }
    }
}
