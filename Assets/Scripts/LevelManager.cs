using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    GameObject[] Enemies;

    int deckRemain, enemiesRemain, turnNumber, deckInitial;

    public int[] cardNumberArray;

    public GameObject movePrefab, dashPrefab, lateralPrefab;
    public GameObject[] cardPool;

    public GameObject CardPosition1, CardPosition2, CardPosition3;
    public GameObject[] cardPositionArray;

    public int dashInitial, moveInitial, lateralInitia;

    public Text deckText;
    // Start is called before the first frame update
    void Start()
    {
        cardNumberArray = new int[3];
        cardPool = new GameObject[3];
        cardPositionArray = new GameObject[3];

        cardNumberArray[0] = moveInitial;
        cardNumberArray[1] = dashInitial;
        cardNumberArray[2] = lateralInitia;

        cardPool[0] = movePrefab;
        cardPool[1] = dashPrefab;
        cardPool[2] = lateralPrefab;

        cardPositionArray[0] = CardPosition1;
        cardPositionArray[1] = CardPosition2;
        cardPositionArray[2] = CardPosition3;


        Enemies = GameObject.FindGameObjectsWithTag("Enemie");
        enemiesRemain = Enemies.Length;

        SpawnRandomCards();
        UpdateGUI(); 
    }

    public void KillEnemy(GameObject enemyGO)
    {
        Destroy(enemyGO, 0.1f);

        enemiesRemain--;
        if (enemiesRemain == 0)
        {
            KilledThemAll();
        }
    }
    public void KilledThemAll()
    {

    }
    public void NextTurn()
    {
        CheckForCards(); 
        SpawnRandomCards();
        UpdateGUI();

        turnNumber++;
    }
    void CheckForCards()
    {
        for (int i = 0; i < cardPositionArray.Length; i++)
        {
            Debug.Log(cardPositionArray[i].transform.childCount);

            if (cardPositionArray[i].transform.childCount != 0)
            {
                Destroy(cardPositionArray[i].transform.GetChild(0).gameObject);
            }
        }
        
    }
    void SpawnRandomCards()
    {
        for (int i = 0; i < cardPool.Length; i++)
        {
            int randomCardPick = Random.Range(0, cardPool.Length);

            if (cardNumberArray[randomCardPick] == 0)
            {
                if (cardNumberArray[0] != 0)
                {
                    randomCardPick = 0;
                }
                else if (cardNumberArray[1] != 0)
                {
                    randomCardPick = 1;
                }
                else
                {
                    randomCardPick = 2;
                }
            }

            Instantiate(cardPool[randomCardPick], cardPositionArray[i].transform);

            //PARA EL MIKEL DEL FUTURO: Solo se restan del pool cuando spawnean asi que deja de ponerlo aquí melón
            cardNumberArray[randomCardPick]--;

            if (cardNumberArray[0] == 0 && cardNumberArray[1] == 0 && cardNumberArray[2] == 0)
            {
                cardNumberArray[0] = moveInitial;
                cardNumberArray[1] = dashInitial;
                cardNumberArray[2] = lateralInitia;
            }
        }
    }

    void UpdateGUI()
    {
        deckRemain = cardNumberArray[0] + cardNumberArray[1] + cardNumberArray[2];
        deckText.text = deckRemain.ToString();
    }
}
