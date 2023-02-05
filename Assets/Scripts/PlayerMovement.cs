using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap groundTilemap;
    public LayerMask obstacleLayerMask;

    [SerializeField]
    Vector3 raycastCorrectionVector = new Vector3(0, 0.5f, 0);
    //Tile Position of the player. It is used to use adyacent tiles in future functions
    public Vector3Int playerTilePosition;
    private Vector3Int playerTileDestination;

    public Material tileActuationMaterial;
    public Material tileActualMaterial;

    public CardMovement cardMovement;

    public List<GameObject> interactableTiles;

    public GameObject playerMesh;

    public LevelManager levelManager;

    public bool canKill;

    public Animator potatoAnimato; 

    // Start is called before the first frame update
    void Start()
    {
        canKill = false; 

        levelManager = levelManager.GetComponent<LevelManager>();
        cardMovement = cardMovement.GetComponent<CardMovement>();

        playerTilePosition = groundTilemap.WorldToCell(transform.position);

        playerTileDestination = playerTilePosition;
        StartCoroutine(SetPlayerPosition(0.01f));
    }

    IEnumerator SetPlayerPosition(float duration)
    //Sets the playerTilePosition to the Tile it is in
    {
        //Arriba
        if (playerTilePosition.y < playerTileDestination.y)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        //Abajo
        else if (playerTilePosition.y > playerTileDestination.y)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        //Derecha
        else if (playerTilePosition.x < playerTileDestination.x)
        {
            transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        //Izquierda
        else if (playerTilePosition.x > playerTileDestination.x)
        {
            transform.localEulerAngles = new Vector3(0, 270, 0);
        }

        playerTilePosition = playerTileDestination;

        float time = 0;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(transform.position, groundTilemap.GetCellCenterWorld(playerTilePosition), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = groundTilemap.GetCellCenterWorld(playerTilePosition);

        canKill = false; 
    }

    public void DashAttack()
    {
        canKill = true;

        potatoAnimato.SetTrigger("corte");

        for (int i = 0; i < interactableTiles.Count; i++)
        {
            if (groundTilemap.WorldToCell(interactableTiles[i].transform.position) == groundTilemap.WorldToCell(cardMovement.groundhittingPoint))
            {
                playerTileDestination = groundTilemap.WorldToCell(interactableTiles[i].transform.position);
            }
        }

        Destroy(cardMovement.cardSelected.transform.GetChild(0).gameObject);

        StartCoroutine(SetPlayerPosition(0.5f));

    }

    public void MoveCard()
    {
        potatoAnimato.SetTrigger("saltar");

        for (int i = 0; i < interactableTiles.Count; i++)
        {
            if (groundTilemap.WorldToCell(interactableTiles[i].transform.position) == groundTilemap.WorldToCell(cardMovement.groundhittingPoint))
            {
                playerTileDestination = groundTilemap.WorldToCell(interactableTiles[i].transform.position);

                Destroy(cardMovement.cardSelected.transform.GetChild(0).gameObject);

                StartCoroutine(SetPlayerPosition(0.3f));
            }
        }
    }

    public void LateralAttack()
    {
        potatoAnimato.SetTrigger("corte_especial");

        StartCoroutine(SpawnBox(0.2f));

        Destroy(cardMovement.cardSelected.transform.GetChild(0).gameObject);

    }

    IEnumerator SpawnBox (float time)
    {
        canKill = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        
        yield return new WaitForSeconds(time);

        gameObject.GetComponent<BoxCollider>().enabled = false;
        canKill = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemie") && canKill == true)
        {
            levelManager.KillEnemy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemie") && canKill == false)
        {
            levelManager.GameOver();
        }

        if (other.gameObject.CompareTag("FinalBunny") && levelManager.canWin)
        {
            levelManager.Winning(); 
        }
    }

    public void CheckIfTiles(int size)
    {
        //Check in +X
        for (int i = 1; i < size + 1; i++)
        {
            playerTileDestination = playerTilePosition + new Vector3Int(i, 0, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length == 1 && sphereColliders[0].gameObject.CompareTag("Suelo"))
            {
                interactableTiles.Add(sphereColliders[0].gameObject);
            }
            else
            {
                break;
            }
        }

        //Check in -X
        for (int i = 1; i < size + 1; i++)
        {
            playerTileDestination = playerTilePosition + new Vector3Int(-i, 0, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length == 1 && sphereColliders[0].gameObject.CompareTag("Suelo"))
            {
                interactableTiles.Add(sphereColliders[0].gameObject);
            }
            else
            {
                break;
            }
        }

        //Check in +Y
        for (int i = 1; i < size + 1; i++)
        {
            playerTileDestination = playerTilePosition + new Vector3Int(0, i, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length == 1 && sphereColliders[0].gameObject.CompareTag("Suelo"))
            {
                interactableTiles.Add(sphereColliders[0].gameObject);
            }
            else
            {
                break;
            }
        }

        //Check in -Y
        for (int i = 1; i < size + 1; i++)
        {
            playerTileDestination = playerTilePosition + new Vector3Int(0, -i, 0);

            Collider[] sphereColliders = Physics.OverlapSphere(groundTilemap.GetCellCenterWorld(playerTileDestination), 0.1f);

            if (sphereColliders.Length == 1 && sphereColliders[0].gameObject.CompareTag("Suelo"))
            {
                interactableTiles.Add(sphereColliders[0].gameObject);
            }
            else
            {
                break;
            }
        }

        foreach (GameObject go in interactableTiles)
        {
            go.GetComponent<MeshRenderer>().material = tileActuationMaterial;
        }
    }
}
