using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cambiar_Escena : MonoBehaviour
{
    public Button changeSceneButton;
    public float waitTime = 5.0f; // tiempo de espera entre escenas
    private int currentSceneIndex = 1; // índice de la escena actual
    private string[] sceneNames = new string[] { "Historia_1", "Historia_2", "Historia_3"}; // nombres de las escenas secundarias

    private void Start()
    {
        changeSceneButton.onClick.AddListener(ButtonChangeScene);
    }

    private void ButtonChangeScene()
    {
        currentSceneIndex = 1;
        SceneManager.LoadScene(sceneNames[currentSceneIndex - 1]);
        StartCoroutine(AutoChangeScene());
    }

    private IEnumerator AutoChangeScene()
    {
        while (currentSceneIndex < sceneNames.Length + 1)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(sceneNames[currentSceneIndex - 1]);
            currentSceneIndex++;
        }
    }
}
