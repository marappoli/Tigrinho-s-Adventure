using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorTransicaoCena2 : MonoBehaviour
{
    public string cenaEscolha3;
    public string chaveNivel2Completo = "Nivel2Completo"; // Chave para armazenar o estado do nível 1 no PlayerPrefs
    public Collider2D meuCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Define o valor para 1 para indicar que o nível 1 foi completado
            PlayerPrefs.SetInt("Nivel2Completo", 1);

            // Verifica se o nível 1 foi concluído
            if (PlayerPrefs.GetInt(chaveNivel2Completo, 0) == 1)
            {
                // Carrega a cena de Escolha 2
                SceneManager.LoadScene(cenaEscolha3);
            }
            else
            {
                meuCollider.isTrigger = false;
                Debug.Log("Você precisa completar o nível 2 primeiro!");
                // Aqui você pode exibir uma mensagem na tela informando ao jogador que eles precisam completar o nível 1 primeiro.
            }
        }
    }
}
