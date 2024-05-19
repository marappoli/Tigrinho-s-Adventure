using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUILuta : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;


    public bool IsOpen { get; private set; }

    private ResponseHandlerLuta responseHandlerLuta;
    private TypewriterEffect typewriterEffect;

private void Start()
{
    typewriterEffect = GetComponent<TypewriterEffect>();
    responseHandlerLuta = GetComponent<ResponseHandlerLuta>();
    if (responseHandlerLuta == null)
    {
        Debug.LogError("ResponseHandler is null");
    }
    CloseDialogueBox();
    ShowDialogue(testDialogue);
}


 public void ShowDialogue(DialogueObject dialogueObject)
{
    if (dialogueObject == null)
    {
        Debug.LogError("DialogueObject is null");
        return;
    }
    IsOpen = true;
    dialogueBox.SetActive(true);
    StartCoroutine(StepThroughtDialogue(dialogueObject));
}


    /*public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }*/

private IEnumerator StepThroughtDialogue(DialogueObject dialogueObject)
{
    if (dialogueObject.Dialogue == null)
    {
        Debug.LogError("Dialogue array is null");
        yield break;
    }

    for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
    {
        string dialogue = dialogueObject.Dialogue[i];
        textLabel.text = dialogue;

        yield return RunTypingEffect(dialogue);

        if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }

    if (dialogueObject.HasResponses)
    {
        responseHandlerLuta.ShowResponses(dialogueObject.Responses);
    }
    else
    {
        CloseDialogueBox();
    }
}

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);
        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }


    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }}