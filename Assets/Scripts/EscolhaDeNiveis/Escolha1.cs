using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Escolha1 : MonoBehaviour
{
    public string Level1Dialogo; // Nome da cena para carregar ao jogar o nível

    private bool jogadorPertoPorta = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPertoPorta = true;
            // Aqui você pode mostrar a opção para o jogador
            // por exemplo, exibindo um texto na tela
            Debug.Log("Pressione 'E' para jogar o nível.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPertoPorta = false;
            // Aqui você pode remover a opção da tela
        }
    }

    private void Update()
    {
        if (jogadorPertoPorta && Input.GetKeyDown(KeyCode.E))
        {
            // Carrega a cena de conversa correspondente
            SceneManager.LoadScene(Level1Dialogo);
        }
    }
}
