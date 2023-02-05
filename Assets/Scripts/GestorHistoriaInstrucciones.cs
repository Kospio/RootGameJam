using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class GestorHistoriaInstrucciones : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite[] images;

    public GameObject pantallaImagenGO;
    public Image pantallaImagen; 

    public int conteo;

    public float duration;

    public string nextScene;

    public Canvas instrucciones;

    public bool canbutton;

    public AudioSource audioSource; 

    private void Start()
    {
        audioSource = audioSource.gameObject.GetComponent<AudioSource>(); 
        instrucciones.gameObject.SetActive(false);

        pantallaImagen = pantallaImagenGO.GetComponent<Image>(); 
    }

    public void preCambioHistoria()
    {
        audioSource.Play(); 
        StartCoroutine(CambiosHistoria());
        canbutton = false; 
    }

    public IEnumerator CambiosHistoria()
    {
        float elapsedTime = 0;
        float startValue = pantallaImagen.color.a;
        float endValue = 0; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            pantallaImagen.color = new Color(pantallaImagen.color.r, pantallaImagen.color.g, pantallaImagen.color.b, newAlpha);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        conteo++;

        if (conteo == images.Length)
        {
            SceneManager.LoadScene(nextScene); 
        }

        else if (conteo == images.Length - 1)
        {
            instrucciones.gameObject.SetActive(true);
        }

        else
        {
            pantallaImagen.sprite = images[conteo];

            elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(0, 1, elapsedTime / duration);
                pantallaImagen.color = new Color(pantallaImagen.color.r, pantallaImagen.color.g, pantallaImagen.color.b, newAlpha);
                yield return null;
            }
        }

        canbutton = true; 
    }
}
