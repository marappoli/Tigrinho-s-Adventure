using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResponseHandlerLuta : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    [SerializeField] private DialogueObject acertoDialogueObject;
    [SerializeField] private DialogueObject erroDialogueObject;
    [SerializeField] private string cenaCorreta;
    [SerializeField] private string cenaIncorreta;


    private DialogueUILuta dialogueUI;

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUILuta>();
    }

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;
        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
{
    responseBox.gameObject.SetActive(false);
    
    Debug.Log("Picked response: " + response.ResponseText);
    Debug.Log("Dialogue Object: " + response.DialogueObject);

    if (response.DialogueObject)
    {
        dialogueUI.ShowDialogue(response.DialogueObject);
    }

    StartCoroutine(LoadSceneAfterDialogue(response.IsCorrect));
}

private IEnumerator LoadSceneAfterDialogue(bool isCorrect)
{
    // Aguarda um pequeno intervalo antes de verificar novamente se o diálogo está aberto
    yield return new WaitForSeconds(0.1f);

    Debug.Log("Dialogue finished. Is correct: " + isCorrect);

    if (isCorrect)
    {
        LoadCorrectScene();
    }
    else
    {
        LoadIncorrectScene();
    }
}


private void LoadCorrectScene()
{
    Debug.Log("Loading correct scene...");

    if (acertoDialogueObject == null)
    {
        Debug.Log("No correct dialogue. Loading scene directly: " + cenaCorreta);
        SceneManager.LoadScene(cenaCorreta);
    }
    else
    {
        Debug.Log("Showing correct dialogue first.");
        dialogueUI.ShowDialogue(acertoDialogueObject);
        SceneManager.LoadScene(cenaCorreta);
    }
}



    private void LoadIncorrectScene()
    {
        dialogueUI.ShowDialogue(erroDialogueObject);
        SceneManager.LoadScene(cenaIncorreta);
    }
}