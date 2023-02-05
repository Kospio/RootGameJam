using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeVictoryScene : MonoBehaviour
{

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void QuitApp()
    {
        Application.Quit(); 
    }
}
