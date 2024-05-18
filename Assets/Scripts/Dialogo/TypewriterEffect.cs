using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewrterSpeed = 40f;

    public bool IsRunning { get; private set;  }

    private Coroutine typingCoroutine;
    
    public void Run(string textToType, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
        
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    // reponsavel por digitar o texto
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        IsRunning = true;
        textLabel.text = string.Empty;

       

        float t = 0;
        int charIndex = 0;
        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime*typewrterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0,charIndex);

            yield return null;
        }
        IsRunning = false;
       
    }
}
