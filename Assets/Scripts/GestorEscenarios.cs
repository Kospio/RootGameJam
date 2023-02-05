using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscenarios : MonoBehaviour
{

    public void SceneChange(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
