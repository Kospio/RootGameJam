using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class RestartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RestartGameRoutine()); 
    }

    IEnumerator RestartGameRoutine()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("Inicio"); 
    }
}
