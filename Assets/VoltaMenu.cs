using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltaMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReiniciaMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
