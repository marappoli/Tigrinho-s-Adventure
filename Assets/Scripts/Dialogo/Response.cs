using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private bool isCorrect;

    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
    public bool IsCorrect => isCorrect;
}
