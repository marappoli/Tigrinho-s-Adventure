using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorTransicaoCena3 : MonoBehaviour
{
    public string cenaEscolha1ou2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
                // Carrega a cena de Escolha 2
                SceneManager.LoadScene(cenaEscolha1ou2);
        }
    }
}
