using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public void IniciarJogo()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
