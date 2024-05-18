using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    [SerializeField] private DialogueObject acertoDialogueObject;
    [SerializeField] private DialogueObject erroDialogueObject;
    [SerializeField] private string cenaCorreta;
    [SerializeField] private string cenaIncorreta;


    private DialogueUI dialogueUI;

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
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

        if (response.DialogueObject)
        {
            StartCoroutine(ShowDialogueAndWait(response.DialogueObject));
        }

        StartCoroutine(LoadSceneAfterDialogue(response.IsCorrect));
    }

    private IEnumerator ShowDialogueAndWait(DialogueObject dialogueObject)
    {
        dialogueUI.ShowDialogue(dialogueObject);
        yield return new WaitUntil(() => !dialogueUI.IsOpen);
    }

    private IEnumerator LoadSceneAfterDialogue(bool isCorrect)
    {
        yield return new WaitUntil(() => !dialogueUI.IsOpen);

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
        dialogueUI.ShowDialogue(acertoDialogueObject);
        SceneManager.LoadScene(cenaCorreta);
    }

    private void LoadIncorrectScene()
    {
        dialogueUI.ShowDialogue(erroDialogueObject);
        SceneManager.LoadScene(cenaIncorreta);
    }
}
